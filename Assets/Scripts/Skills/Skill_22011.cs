using Entity;
using Entity.Components;
using Entity.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_22011 : FSMState<Skill> {
		private float m_damage;
		
		public Skill_22011() {
			m_damage = SkillDataSystem.GetValue(22011, "Damage");
		}

		public override void Enter(Skill target) {
			List<EntityBehaviour> entitys = GameObject.FindObjectsOfType<EntityBehaviour>().ToList();
			EntityBehaviour player = entitys.Find((EntityBehaviour entity) => entity.TryGetComponent(out PlayerInputComponent input));
			entitys.Remove(player);

			GameObject playerObject = null;
			if (player != null) {
				playerObject = player.gameObject;
			}
			foreach (EntityBehaviour entity in entitys) {
                entity.OnHit(playerObject, m_damage, HitType.Skill);
			}

			target.ChangeState(SkillState.Cooldown);
		}

		public override void Update(Skill target) {

		}

		public override void FixedUpdate(Skill target) { 
		
		}
	}
}