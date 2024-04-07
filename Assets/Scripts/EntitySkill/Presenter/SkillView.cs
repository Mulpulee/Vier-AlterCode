using Entity.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.DesignPattern.MVP;

namespace EntitySkill.Presenter {
	public class SkillView : ViewBase {
		[Header("Elements")]
		public GameObject SkillElementParent;
		public List<SkillViewElement> SkillElement;

		private void Awake() {
			if (SkillElementParent == null) {
				SkillElementParent = gameObject;
			}
			SkillElement = SkillElementParent.transform.GetComponentsInChildren<SkillViewElement>().ToList();
			SkillElement.Reverse();

			while (SkillElement.Count > (int)SkillSlot.Count) {
				int index = SkillElement.Count - 1;
				Destroy(SkillElement[index].gameObject);
				SkillElement.RemoveAt(index);
			}
		}
	}
}