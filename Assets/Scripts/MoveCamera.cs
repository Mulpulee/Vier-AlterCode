using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public GameObject Target;             // 따라다닐 타겟 오브젝트
    public float follow_speed = 4.0f;    // 따라가는 속도
    public float z = -10.0f;            // 고정시킬 카메라의 z축의 값
    public float minX = -10f;                //x축 최소
    public float maxX = 10f;                //x축 최대
    public float minY = -10f;                //y축 최소
    public float maxY = 10f;                //y축 최대

    Transform this_transform;            // 카메라의 좌표
    Transform Target_transform;         // 타겟의 좌표

    void Start()
    {
        this_transform = GetComponent<Transform>();
        Target_transform = Target.GetComponent<Transform>();
    }

    void OnDrawGizmos() //카메라 제한영역 그리기
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
        this_transform.Translate(0, 0, z); //카메라를 원래 z축으로 이동
    }
}