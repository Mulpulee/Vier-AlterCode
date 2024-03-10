using Entity;
using Entity.Basic;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : EntityBehaviour {
	public float atkRange;
	public float fieldOfVision;

	public Image nowHpbar;
	public GameObject prfHpBar;
	public GameObject canvas;

	public RectTransform hpBar;
	public float height = 1.7f;

	public EntityBehaviour Player;
	public Animator enemyAnimator;


	private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision) {
		Status.Name = _enemyName;
		Status.OriginHealth.SetMaxValue(_maxHp);
		Status.Health = _maxHp;
		Status.Attack = _atkDmg;
		Status.AttackSpeed = _atkSpeed;
		Status.Speed = _moveSpeed;
		atkRange = _atkRange;
		fieldOfVision = _fieldOfVision;
	}

	void Start() {
		base.ReInitHandler(new EntityHit(), new EnemyDeath());
		base.ReInitStatus(new EntityStatus());

		if (prfHpBar != null) {
			if (canvas != null) {
				GameObject hpBarObject = Instantiate(prfHpBar, canvas.transform);
				this.hpBar = hpBarObject.GetComponent<RectTransform>();

			}
		}

		switch (name) {
			case "nearEnemy":
				SetEnemyStatus("nearEnemy", 100, 10, 2f, 3, 4f, 14f);  //이름 최대체력 대미지 공격딜레이 이동속도 공격범위 시야범위
				break;
			case "hardEnemy":
				SetEnemyStatus("hardEnemy", 100, 10, 3f, 2, 8f, 12f);
				break;
			case "farEnemy":
				SetEnemyStatus("farEnemy", 100, 10, 5f, 5, 15f, 20f);
				break;
		}

		if (hpBar != null) {
            Transform child = hpBar.transform.GetChild(0);

            if (child != null) {
			    nowHpbar = child.GetComponent<Image>();
            }
        } else {
            Transform parent = transform.parent;
            string fullName = gameObject.name;
            while (parent != null) {
                fullName = $"{parent.name}/{fullName}";

                parent = parent.parent;
            }

            Debug.Log($"HP Bar is Null! (in {fullName})");
        }
	}

	void Update() {
		Vector3 _hpBarPos = Camera.main.WorldToScreenPoint
			(new Vector3(transform.position.x, transform.position.y + height, 0));
        if (hpBar != null) { 
		    hpBar.position = _hpBarPos;
        }

        float nowHp = Status.Health;
        float maxHp = Status.OriginHealth.MaxValue;

		if (nowHpbar != null) {
			nowHpbar.fillAmount = nowHp / maxHp;
		}
	}

	// void Die() -> public void OnDeath(EntityBehaviour) in Assets/Scripts/Enemy/EnemyDeath.cs; 
}

// Original Code
//public class Enemy : MonoBehaviour
//{
//    public string enemyName;
//    public int maxHp;
//    public int nowHp;
//    public int atkDmg;
//    public float atkSpeed;
//    public float moveSpeed;
//    public float atkRange;
//    public float fieldOfVision;
//    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
//    {
//        enemyName = _enemyName;
//        maxHp = _maxHp;
//        nowHp = _maxHp;
//        atkDmg = _atkDmg;
//        atkSpeed = _atkSpeed;
//        moveSpeed = _moveSpeed;
//        atkRange = _atkRange;
//        fieldOfVision = _fieldOfVision;
//    }

//    public Image nowHpbar;
//    public GameObject prfHpBar;
//    public GameObject canvas;

//    RectTransform hpBar;
//    public float height = 1.7f;

//    public MoveScript Player;
//    public Animator enemyAnimator;


//    void Start()
//    {
//        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();

//        switch (name)
//        {
//            case "nearEnemy":
//                SetEnemyStatus("nearEnemy", 100, 10, 2f, 3, 4f, 14f);  //이름 최대체력 대미지 공격딜레이 이동속도 공격범위 시야범위
//                break;
//            case "hardEnemy":
//                SetEnemyStatus("hardEnemy", 100, 10, 3f, 2, 8f, 12f);
//                break;
//            case "farEnemy":
//                SetEnemyStatus("farEnemy", 100, 10, 5f, 5, 15f, 20f);
//                break;
//        }
//        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
//    }

//    void Update()
//    {
//        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint
//            (new Vector3(transform.position.x, transform.position.y + height, 0));
//        hpBar.position = _hpBarPos;
//        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
//    }

//    private void OnTriggerEnter2D(Collider2D col)
//    {
//        if (col.CompareTag("Player"))
//        {
//            if (Player.attacked)
//            {
//                nowHp -= Player.atkDmg;
//                Player.attacked = false;

//                if (nowHp <= 0) Die(); //사망처리
//            }
//        }
//    }

//    void Die() //사망처리
//    {
//        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
//        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
//        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
//        Destroy(gameObject, 3);                     // 3초후 제거
//        Destroy(hpBar.gameObject, 3);               // 3초후 체력바 제거
//    }
//}