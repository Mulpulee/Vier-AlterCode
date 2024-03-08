using Entity.Basic;
using Entity.Components;
using Entity.Interface;
using System;
using System.Linq;
using System.Xml;
using UnityEngine;
using Utility.DesignPattern;

namespace Entity.Player {
	public class PlayerEvent : SingletionBehaviour<PlayerEvent> {
		[SerializeField] private EntityBehaviour m_playerPrefab;

		public IDeath Death => m_playerDeath;
		public IHit Hit => m_playerHit;

		private IDeath m_playerDeath = new EntityDeath();
		private IHit m_playerHit = new EntityHit();

		private EntityBehaviour m_playerInstance;
		public event Action OnPlayerSpawned;

		[ContextMenu("Spawn")]
		public void Spawn() {
			EntityBehaviour player = EntityBehaviour.GetBuilder(m_playerPrefab, Vector2.zero)
			.AddEntityComponent<PlayerMoveComponent>()
			.AddEntityComponent<PlayerSkillComponent>()
			.AddEntityComponent<PlayerInteractComponent>()
			.AddEntityComponent<PlayerInputComponent>()
			.AddEntityComponent<PlayerSkillChangeComponent>()
			.SetHit(new EntityHit())
			.SetDeath(new EntityDeath())
			.Build();

			m_playerInstance = player;

			OnPlayerSpawned?.Invoke();

			var skillCaster = player.GetComponent<PlayerSkillComponent>();
			skillCaster.SetSkill(SkillSlot.Slot1, 20001);
			skillCaster.SetSkill(SkillSlot.Slot2, 21011);
			skillCaster.SetSkill(SkillSlot.Slot3, 20002);
			skillCaster.SetSkill(SkillSlot.Slot4, 21001);
		}
	}
}