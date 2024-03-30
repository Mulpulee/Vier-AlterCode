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
		private float m_damageRate;
		
		public Skill_22011() {
			m_damage = SkillDataSystem.GetValue(22011, "Damage");
            m_damageRate = SkillDataSystem.GetValue(22011, "DamageRate");
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
				float damage = (target.Caster.Status.Attack * m_damageRate) + m_damage;
                entity.OnHit(playerObject, damage, HitType.Skill);
			}

			target.ChangeState(SkillState.Cooldown);
		}

		public override void Update(Skill target) {

		}

		public override void FixedUpdate(Skill target) { 
		
		}
	}
}