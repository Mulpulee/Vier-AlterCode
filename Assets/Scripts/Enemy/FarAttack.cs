using System.Collections;
using UnityEngine;

public class FarAttack : MonoBehaviour
{
    public GameObject bulletPrf;
    Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<Enemy>().enemyAnimator;
    }

    private void Update()
    {
        if (enemyAnimator.GetBool("Attacking")) //Attacking(Animation)으로 분석
        {
            Invoke("Attack", 0.4f); //0.4초뒤 총알 발사
        }
    }

    private void Attack()
    {
        GameObject temp = Instantiate(bulletPrf);
        temp.transform.position = transform.position;
        Destroy(temp, 2f);
    }
}