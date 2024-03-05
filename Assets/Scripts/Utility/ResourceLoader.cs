using UnityEngine;

namespace Utility {
	public static class ResourceLoader {
		public static T SkillObjectLoad<T>(string name) where T : MonoBehaviour => Resources.Load<T>($"SkillObjects/{name}");
		public static Sprite SkillImageLoad(int id) => Resources.Load<Sprite>($"SkillImages/{id}");
	}
}