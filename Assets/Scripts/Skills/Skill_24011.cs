using Entity.Components;
using UnityEngine;
using Utility;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_24011 : FSMState<Skill> {
		private static Skill_24011_Drone m_drone;

		private float m_droneSenseRange = 5.0f;
		private float m_droneDamage = 10.0f;
		private float m_droneMovePower = 15.0f;
		private float m_droneRemoveMinSpeed = 0.5f;


		public Skill_24011() {
			m_droneSenseRange = SkillDataSystem.GetValue(24011, "DroneSenseRange");
			m_droneDamage = SkillDataSystem.GetValue(24011, "DroneDamage");
			m_droneMovePower = SkillDataSystem.GetValue(24011, "DroneMovePower");
			m_droneRemoveMinSpeed = SkillDataSystem.GetValue(24011, "DroneRemoveMinSpeed");
		}

		public override void Enter(Skill target) {
			if (Skill_24011.m_drone == null) {
				Skill_24011.m_drone = ResourceLoader.SkillObjectLoad<Skill_24011_Drone>("Skill_24011_Drone");

				Skill_24011.m_drone.SetDamage(m_droneDamage);
				Skill_24011.m_drone.SetSenseRange(m_droneSenseRange);
				Skill_24011.m_drone.SetMovePower(m_droneMovePower);
				Skill_24011.m_drone.SetRemoveMinSpeed(m_droneRemoveMinSpeed);
			}

			Skill_24011_Drone drone = GameObject.Instantiate(Skill_24011.m_drone, target.Caster.gameObject.transform.position, Quaternion.identity);
			drone.SetCaster(target.Caster);

			target.ChangeState(SkillState.Cooldown);
		}

		public override void Update(Skill target) {

		}
		public override void FixedUpdate(Skill target) { 
		
		}
	}
}