using Entity.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility.DataStructure;
using Utility.Extension;
using Utility.Management;

namespace GameSystemManager {
	public enum InputType {
		Skill1 = SkillSlot.Slot1,
		Skill2 = SkillSlot.Slot2,
		Skill3 = SkillSlot.Slot3,
		Skill4 = SkillSlot.Slot4,
		SkillSlot1,
		SkillSlot2,
		SkillSlot3,
		SkillSlot4,
		MoveUp,
		MoveDown,
		MoveLeft,
		MoveRight,
		Jump,
		Attack,
		Interact,
	}

	public class GameInputManager : IndestructibleSingleton<GameInputManager> {
		private static readonly string SaveKey = "KeySettings";
		[SerializeField] private SerializedDictionary<InputType, KeyCode> m_keyCodeByInputTypes;
		[SerializeField] private bool m_isLocked;
		HashSet<object> m_locks;

		public event Action OnKeyChanged;

		public IEnumerable<object> Locks => m_locks;
		public bool IsLocked => m_isLocked;

		[ContextMenu("Save")]
		private void SaveSettings() {
			string json = EncryptionUtility.ToEncrytedString(m_keyCodeByInputTypes);
			PlayerPrefs.SetString(SaveKey, json);
		}
		[ContextMenu("Load")]
		private void LoadSettings() {
			if (PlayerPrefs.HasKey(SaveKey)) {
				string json = PlayerPrefs.GetString(SaveKey);
				m_keyCodeByInputTypes = EncryptionUtility.FromEncrytedString<SerializedDictionary<InputType, KeyCode>>(json);
			} else {
				ResetSettings();
				SaveSettings();
			}
		}

		[ContextMenu("Reset")]
		private void ResetSettings() {
			m_keyCodeByInputTypes = new SerializedDictionary<InputType, KeyCode>() {
				{ InputType.Skill1, KeyCode.Q },
				{ InputType.Skill2, KeyCode.W },
				{ InputType.Skill3, KeyCode.E },
				{ InputType.Skill4, KeyCode.R },
				{ InputType.SkillSlot1, KeyCode.Alpha1 },
				{ InputType.SkillSlot2, KeyCode.Alpha2 },
				{ InputType.SkillSlot3, KeyCode.Alpha3 },
				{ InputType.SkillSlot4, KeyCode.Alpha4 },
				{ InputType.MoveUp		, KeyCode.UpArrow },
				{ InputType.MoveDown	, KeyCode.DownArrow },
				{ InputType.MoveLeft	, KeyCode.LeftArrow },
				{ InputType.MoveRight	, KeyCode.RightArrow },
				{ InputType.Jump	, KeyCode.Space },
				{ InputType.Attack	, KeyCode.A },
				{ InputType.Interact, KeyCode.D },
			};
		}

		public void Lock(object key) {
			m_locks.Add(key);
			m_isLocked = true;
		}
		public void UnLock(object key) {
			m_locks.Remove(key);
			m_isLocked = m_locks.Count > 0;
		}

		public KeyCode GetKeyCode(InputType type) => m_keyCodeByInputTypes[type];
		public bool ChangeKey(InputType type, KeyCode code) {
			if (code == KeyCode.Escape) {
				return false;
			}

			foreach (KeyCode keyCode in m_keyCodeByInputTypes.Values) {
				if (code == keyCode) {
					return false;
				}
			}

			m_keyCodeByInputTypes[type] = code;

			OnKeyChanged?.Invoke();

			return true;
		}

		public static InputType SkillSlotToInputType(SkillSlot slot) {
			int temp = (int)slot;
			InputType type = (InputType)temp;
			return type;
		}

		public static bool GetKeyDown(SkillSlot slot) => GetKeyDown(SkillSlotToInputType(slot));
		public static bool GetKey(SkillSlot slot) => GetKey(SkillSlotToInputType(slot));
		public static bool GetKeyUp(SkillSlot slot) => GetKeyUp(SkillSlotToInputType(slot));

		public static bool GetKeyDown(InputType type) {
			if (Instance.m_isLocked) {
				return false;
			}

			if (Instance.m_keyCodeByInputTypes.TryGetValue(type, out KeyCode code)) {
				if (code == KeyCode.Mouse0) {
					return Input.GetKeyDown(code) && !EventSystem.current.IsPointerOverGameObject();
				} else {
					return Input.GetKeyDown(code);
				}
			}

			return false;
		}
		public static bool GetKey(InputType type) {
			if (Instance.m_isLocked) {
				return false;
			}

			if (Instance.m_keyCodeByInputTypes.TryGetValue(type, out KeyCode code)) {
				return Input.GetKey(code);
			}

			return false;
		}
		public static bool GetKeyUp(InputType type) {
			if (Instance.m_isLocked) {
				return false;
			}

			if (Instance.m_keyCodeByInputTypes.TryGetValue(type, out KeyCode code)) {
				return Input.GetKeyUp(code);
			}

			return false;
		}

		protected override void OnInstantiated() {
			m_locks = new HashSet<object>();
			LoadSettings();
		}
	}
}