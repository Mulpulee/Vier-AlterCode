using Entity.Interface;
using Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity.Player;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;
    public float damage = 1.0f;
    public Transform target;
    private Vector3 pos;
    private void Start()
    {
        // old code
        // target = GameObject.Find("Player").transform;

        EntityBehaviour player = PlayerEvent.Instance.Player;
        if (player != null) {
            target = player.transform;
        }
        
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        pos = new Vector2(dir, 0);
        Destroy(gameObject, 2f);            //생성으로부터 2초 후 삭제
    }

    private void Update()
    {
        transform.Translate(pos * Speed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			Destroy(gameObject);
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
				return;
		}

		if (other.TryGetComponent(out IHittable hit)) {
			hit.OnHit(gameObject, damage, HitType.Normal);
		    Destroy(gameObject);
        }
	}
}
