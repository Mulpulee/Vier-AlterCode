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
	[SerializeField] private EntityBehaviour m_entity;
	[SerializeField] private EntityBehaviour m_result;
	[SerializeField] private CanvasGroup m_group;

	private void Start() {
		
	}

	private void Update() {

	}

	[ContextMenu("Make")]
	public void Make() {
		PlayerEvent.Instance.Spawn();
	}
}