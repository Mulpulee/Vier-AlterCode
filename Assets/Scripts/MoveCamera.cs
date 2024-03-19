using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public GameObject Target;             // ����ٴ� Ÿ�� ������Ʈ
    public float follow_speed = 4.0f;    // ���󰡴� �ӵ�
    public float z = -10.0f;            // ������ų ī�޶��� z���� ��
    public float minX = -10f;                //x�� �ּ�
    public float maxX = 10f;                //x�� �ִ�
    public float minY = -10f;                //y�� �ּ�
    public float maxY = 10f;                //y�� �ִ�

    Transform this_transform;            // ī�޶��� ��ǥ
    Transform Target_transform;         // Ÿ���� ��ǥ

    void Start()
    {
        this_transform = GetComponent<Transform>();
        Target_transform = Target.GetComponent<Transform>();
    }

    void OnDrawGizmos() //ī�޶� ���ѿ��� �׸���
    {
            Vector3 p1 = new Vector3(minX, maxY, z); 
            Vector3 p2 = new Vector3(maxX, maxY, z);
            Vector3 p3 = new Vector3(maxX, minY, z);
            Vector3 p4 = new Vector3(minX, minY, z);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
    }

    void Update()
    {
        float x = Mathf.Clamp(Target_transform.position.x, minX, maxX);
        float y = Mathf.Clamp(Target_transform.position.y, minY, maxY);

        this_transform.position = Vector2.Lerp(this_transform.position, new Vector3(x, y, z), follow_speed * Time.deltaTime);
        this_transform.Translate(0, 0, z); //ī�޶� ���� z������ �̵�
    }
}