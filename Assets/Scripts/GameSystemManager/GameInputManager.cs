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
		Skill5 = SkillSlot.Slot5,
		FormSlot1 = SkillSlot.Count,
		FormSlot2,
		FormSlot3,
		FormSlot4,
		FormSlot5,
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
		private static readonly string Version = "KeyVersion";
		private static readonly float KeysVersion = 0.1f;
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
			PlayerPrefs.SetFloat(Version, KeysVersion);
		}
		[ContextMenu("Load")]
		private void LoadSettings() {
			if (PlayerPrefs.HasKey(SaveKey)) {
				if (PlayerPrefs.HasKey(Version)) {
					float version = PlayerPrefs.GetFloat(Version);
					if (GameInputManager.KeysVersion > version) {
						Debug.Log($"Key Version Change! {version} -> {GameInputManager.KeysVersion}");
						ResetSettings();
                        SaveSettings();
                        return;
                    }
				} else {
                    Debug.Log($"Key Version Change! None -> {GameInputManager.KeysVersion}");
                    ResetSettings();
					SaveSettings();
					return;
				}
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
				{ InputType.Skill5, KeyCode.T },
				{ InputType.FormSlot1, KeyCode.BackQuote },
				{ InputType.FormSlot2, KeyCode.Alpha1 },
				{ InputType.FormSlot3, KeyCode.Alpha2 },
				{ InputType.FormSlot4, KeyCode.Alpha3 },
				{ InputType.FormSlot5, KeyCode.Alpha4 },
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