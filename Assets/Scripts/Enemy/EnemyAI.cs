using Entity;
using Entity.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    Enemy enemy;

    Animator enemyAnimator;

    [SerializeField]
    float attackDelay;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;

        // Add Code..
        EntityBehaviour player = PlayerEvent.Instance.Player;
        if (player != null) {
            target = player.transform;
        }
    }

    void DecreaseDelay() //딜레이감소
    {
        attackDelay -= Time.deltaTime;

        if (attackDelay < 0)
        {
            attackDelay = 0;
        }
    }

    void Update()
    {
        Invoke("DecreaseDelay", 1f);
        // 타겟과 자신의 거리를 확인
        float distance = Vector3.Distance(transform.position, target.position);

        //시야 범위안에 들어올 때
        if (distance <= enemy.fieldOfVision)
        {
            FaceTarget(); // 타겟 바라보기
            //쿨타임이 0이고 공격 범위 안에 들어왔으면
            if (attackDelay == 0 && distance <= enemy.atkRange)
            {
                AttackTarget();
            }
            else
            {
                enemyAnimator.SetBool("Attacking", false);
                if ( distance >=2 && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else
        {
            enemyAnimator.SetBool("LeftMoving", false);
            enemyAnimator.SetBool("RightMoving", false);
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * enemy.Status.Speed * Time.deltaTime);
        if (dir == -1)
        {
            enemyAnimator.SetBool("LeftMoving", true);
            enemyAnimator.SetBool("RightMoving", false);
        }
        else
        {
            enemyAnimator.SetBool("LeftMoving", true);
            enemyAnimator.SetBool("RightMoving", false);
            //enemyAnimator.SetBool("LeftMoving", false);
            //enemyAnimator.SetBool("RightMoving", true);
        }
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    void AttackTarget()
    {
        enemyAnimator.SetBool("LeftMoving", false);
        enemyAnimator.SetBool("RightMoving", false);
        enemyAnimator.SetBool("Attacking", true); // 공격 애니메이션 실행
        Debug.Log(enemyAnimator.GetBool("Attacking"));
        attackDelay = enemy.Status.AttackSpeed; // 딜레이 충전
        //target.GetComponent<Player>().nowHp -= enemy.atkDmg;
    }
}
