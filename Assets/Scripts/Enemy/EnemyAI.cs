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

    void DecreaseDelay() //�����̰���
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
        // Ÿ�ٰ� �ڽ��� �Ÿ��� Ȯ��
        float distance = Vector3.Distance(transform.position, target.position);

        //�þ� �����ȿ� ���� ��
        if (distance <= enemy.fieldOfVision)
        {
            FaceTarget(); // Ÿ�� �ٶ󺸱�
            //��Ÿ���� 0�̰� ���� ���� �ȿ� ��������
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
        if (target.position.x - transform.position.x < 0) // Ÿ���� ���ʿ� ���� ��
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else // Ÿ���� �����ʿ� ���� ��
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    void AttackTarget()
    {
        enemyAnimator.SetBool("LeftMoving", false);
        enemyAnimator.SetBool("RightMoving", false);
        enemyAnimator.SetBool("Attacking", true); // ���� �ִϸ��̼� ����
        Debug.Log(enemyAnimator.GetBool("Attacking"));
        attackDelay = enemy.Status.AttackSpeed; // ������ ����
        //target.GetComponent<Player>().nowHp -= enemy.atkDmg;
    }
}
