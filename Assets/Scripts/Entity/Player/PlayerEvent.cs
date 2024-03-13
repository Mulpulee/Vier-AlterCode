using Entity.Basic;
using Entity.Components;
using Entity.Interface;
using EntitySkill;
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

		public EntityBehaviour Player => m_playerInstance;
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
			skillCaster.SetSkill(SkillSlot.Slot1, 10001);
			skillCaster.SetSkill(SkillSlot.Slot2, 10002);
			skillCaster.SetSkill(SkillSlot.Slot3, 0);
			skillCaster.SetSkill(SkillSlot.Slot4, 0);
		}
	}
}