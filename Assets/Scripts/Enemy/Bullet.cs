using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;
    public Transform target;
    private Vector3 pos;
    private void Start()
    {
        target = GameObject.Find("Player").transform;
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        pos = new Vector2(dir, 0);
        Destroy(gameObject, 2f);            //�������κ��� 2�� �� ����
    }

    private void Update()
    {
        transform.Translate(pos * Speed * Time.deltaTime);
    }
}
