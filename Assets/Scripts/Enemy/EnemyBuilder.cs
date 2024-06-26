using Entity;
using Entity.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.DataStructure;

public enum EnemyType {
    Far,
    Hard,
    Near,
    TutorialBot
}

public class EnemyBuilder : MonoBehaviour {
    [System.Serializable]
    public class EnemyInfo {
        [Header("Gizmos")]
        public Color DrawColor = Color.black;
        public float DrawSphereRadius = 0.5f;

        [Header("Enemy Info")]
        public EntityBehaviour Enemy = null;
	}

    public int EnemyCount => m_spawnedEnemy.childCount;
    public bool IsEnemyZero => m_spawnedEnemy.childCount <= 0;

    [Header("Required Info")]
    [SerializeField] private Transform m_spawnedEnemy;
    [SerializeField] private Canvas m_enemyUICanvas;
    [SerializeField] private GameObject m_hpPrefab;

	[Header("Enemys")]
    [SerializeField] private List<CustomKeyValuePair<EnemyType, Vector3>> m_buildEnemys;

	[Header("Debugs")]
    [SerializeField] private SerializedDictionary<EnemyType, EnemyInfo> m_drawInfo;
    [SerializeField] private int m_enemyCount;
    [SerializeField] private bool m_isEnemyZero;

    [ContextMenu("Initialize Debuging")]
    private void IntializeDebuging() {
        for (int i = 0; i <= (int)EnemyType.TutorialBot; i++) {
            EnemyType type = (EnemyType)i;
            Debug.Log(type.ToString());
            if (!m_drawInfo.ContainsKey(type)) {
                m_drawInfo[type] = new EnemyInfo();
            }
        }
    }

	private void Reset() {
        m_drawInfo[EnemyType.Far]  = new EnemyInfo();
        m_drawInfo[EnemyType.Hard] = new EnemyInfo();
        m_drawInfo[EnemyType.Near] = new EnemyInfo();
        m_drawInfo[EnemyType.TutorialBot] = new EnemyInfo();
	}

    public void SpawnEnemys() {
        Type enemyTypeEnum = typeof(EnemyType);

        foreach (CustomKeyValuePair<EnemyType, Vector3> enemy in m_buildEnemys) {
            if (!m_drawInfo.ContainsKey(enemy.Key)) {
                continue;
            }
            EnemyInfo info = m_drawInfo[enemy.Key];

            var builder = EntityBehaviour.GetBuilder(info.Enemy, enemy.Value);
            builder.SetHit(new EntityHit());
            builder.SetDeath(new EnemyDeath());

            string typeName = Enum.GetName(enemyTypeEnum, enemy.Key);
            string enemyName = $"{typeName} Enemy";

            builder.SetName(enemyName);

            var enemyStatus = new EntityStatus();
            switch (enemy.Key) {
                case EnemyType.Far:
                    enemyStatus.OriginHealth.SetMaxValue(100);
                    enemyStatus.Health = 100;
                    enemyStatus.Attack = 10.0f;
                    enemyStatus.AttackSpeed = 5.0f;
                    enemyStatus.Speed = 5.0f;
                    enemyStatus.AttackRange = 12.0f;
                    enemyStatus.Vision = 20.0f;

                    enemyStatus.Defense = 0.0f;
                    enemyStatus.DamageReductionRate = 0.0f;
                    break;
                case EnemyType.Hard:
                    enemyStatus.OriginHealth.SetMaxValue(100);
                    enemyStatus.Health = 100;
                    enemyStatus.Attack = 10.0f;
                    enemyStatus.AttackSpeed = 3.0f;
                    enemyStatus.Speed = 2.0f;
                    enemyStatus.AttackRange = 4.0f;
                    enemyStatus.Vision = 12.0f;

                    enemyStatus.Defense = 0.0f;
                    enemyStatus.DamageReductionRate = 0.0f;
                    break;
                case EnemyType.Near:
                    enemyStatus.OriginHealth.SetMaxValue(100);
                    enemyStatus.Health = 100;
                    enemyStatus.Attack = 10.0f;
                    enemyStatus.AttackSpeed = 2.0f;
                    enemyStatus.Speed = 3.0f;
                    enemyStatus.AttackRange = 1.5f;
                    enemyStatus.Vision = 14.0f;

                    enemyStatus.Defense = 0.0f;
                    enemyStatus.DamageReductionRate = 0.0f;
                    break;
                case EnemyType.TutorialBot:
                    enemyStatus.OriginHealth.SetMaxValue(99999);
                    enemyStatus.Health = 99999;

                    enemyStatus.Defense = 99999f;
                    enemyStatus.DamageReductionRate = 99999f;

                    builder.SetDeath(new TutorialBotDeath());
                    break;
                default:
                    continue;
            }

            builder.SetStatus(enemyStatus);

            EntityBehaviour behavior = builder.Build();
            if (enemy.Key != EnemyType.TutorialBot) behavior.transform.parent = m_spawnedEnemy;

            if (m_hpPrefab != null && m_enemyUICanvas != null) {
                if (behavior.TryGetComponent(out Enemy enemyInfo)) {
                    GameObject hpBarObject = Instantiate(m_hpPrefab, m_enemyUICanvas.transform);
                    if (hpBarObject.TryGetComponent(out RectTransform rectTransform)) {
                        enemyInfo.hpBar = rectTransform;

                        Transform child = rectTransform.GetChild(0);

                        if (child != null) {
                            enemyInfo.nowHpbar = child.GetComponent<Image>();
                        }
                    }
                    else {
                        Destroy(hpBarObject);
                    }
                }
            }
            else {
                continue;
            }

            behavior.name = $"{enemyName} (Spawn From: {enemy.Value})";
        }
    }

	private void Start() {
        SpawnEnemys();
    }

    private void Update() {
#if UNITY_EDITOR
        m_enemyCount = EnemyCount;
        m_isEnemyZero = IsEnemyZero;

        // IDE0052 Error Removed.
        if (m_enemyCount == 0) { }
        if (m_isEnemyZero) { }
#endif
    }

    private void OnDrawGizmos() {
        Color before = Gizmos.color;
	    
        foreach(CustomKeyValuePair<EnemyType, Vector3> enemy in m_buildEnemys) {
            if (!m_drawInfo.ContainsKey(enemy.Key)) {
                continue;
            }
            EnemyInfo info = m_drawInfo[enemy.Key];

			Gizmos.color = info.DrawColor;

            Gizmos.DrawWireSphere(enemy.Value, info.DrawSphereRadius);
        }

        Gizmos.color = before;
    }
}
