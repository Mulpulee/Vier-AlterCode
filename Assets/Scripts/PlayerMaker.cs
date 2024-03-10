using Entity;
using Entity.Basic;
using Entity.Components;
using Entity.Player;
using EntitySkill;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utility.Extension;
using Utility.Management;

public class PlayerMaker : MonoBehaviour {
	private void Awake() {
		Make();
	}

	private void Update() {

	}

	[ContextMenu("Make")]
	public void Make() {
		PlayerEvent.Instance.Spawn();
	}
}