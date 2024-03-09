//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.Playables;

//public enum NearEnemyState
//{
//    Idle = 0,       //대기
//    TailAttack,     //꼬리평타
//    ClawsAttack,    //발톱평타
//    FireCircle,     //원형불
//    EattingMob,     //먹이 끌어당기기
//    CircleShoot,    //원형화염구
//    FanShoot        //부채꼴화염구
//}
//public class NearEnemy : MonoBehaviour
//{
//    public GameObject prfFire;
//    public Transform target;
//    Vector3 whereToAtk;
//    Vector3 playerPos;
//    Enemy enemy;

//    private NearEnemyState dragonState;

//    // Start is called before the first frame update
//    private void Awake()
//    {
//        enemy = GetComponent<Enemy>();
//        ChangeState(NearEnemyState.Idle);
//    }

//    public void SetState(int num)
//    {
//        switch (num)
//        {
//            case 0: ChangeState(NearEnemyState.Idle); break;
//            case 1: ChangeState(NearEnemyState.TailAttack); break;
//            case 2: ChangeState(NearEnemyState.ClawsAttack); break;
//            case 3: ChangeState(NearEnemyState.FireCircle); break;
//            case 4: ChangeState(NearEnemyState.EattingMob); break;
//            case 5: ChangeState(NearEnemyState.CircleShoot); break;
//            case 6: ChangeState(NearEnemyState.FanShoot); break;
//            default: break;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//    }

//    private void ChangeState(NearEnemyState newState)
//    {
//        StopCoroutine(dragonState.ToString());
//        dragonState = newState;
//        StartCoroutine(dragonState.ToString());
//    }

//    private IEnumerator Idle()
//    {
//        Debug.Log("비전투모드");

//        while (true)
//        {
//            yield return null;
//        }
//    }
//    private IEnumerator TailAttack()
//    {
//        int i = 0;
//        Debug.Log("꼬리평타");

//        while (i < 0)
//        {
//            Debug.Log("쌔액");
//        }

//        enemy.isAttaking = false;

//        ChangeState(NearEnemyState.Idle);
//        yield return null;
//    }

//    private IEnumerator ClawsAttack()
//    {
//        int i = 0;
//        Debug.Log("발톱평타");

//        while (i < 0)
//        {
//            Debug.Log("콩");
//        }

//        enemy.isAttaking = false;

//        yield return null;
//        ChangeState(NearEnemyState.Idle);
//    }

//    private IEnumerator FireCircle()
//    {
//        int i = 0;
//        Debug.Log("화염");

//        while (i < 3)
//        {
//            i++;
//            Debug.Log("뚜시뚜시");
//            yield return new WaitForSeconds(1);
//        }
//        enemy.isAttaking = false;
//        ChangeState(NearEnemyState.Idle);
//    }

//    private IEnumerator EattingMob()
//    {
//        int i = 0;
//        Debug.Log("먹이끌어당기기");

//        while (i < 3)
//        {
//            i++;
//            Debug.Log("쿠와아앙");
//            yield return new WaitForSeconds(1);
//        }
//        enemy.isAttaking = false;
//        ChangeState(NearEnemyState.Idle);
//    }

//    private IEnumerator CircleShoot()
//    {
//        int i = 0;
//        Debug.Log("원형화염구");

//        while (i < 3)
//        {
//            i++;
//            Circleshoot();
//            Debug.Log("토도도돗");
//            yield return new WaitForSeconds(1);
//        }
//        enemy.isAttaking = false;
//        ChangeState(NearEnemyState.Idle);
//    }

//    private IEnumerator FanShoot()
//    {
//        int i = 0;
//        Debug.Log("부채꼴화염구");

//        while (i < 3)
//        {
//            i++;
//            Fanshoot();
//            Debug.Log("슝슝슝슝");
//            yield return new WaitForSeconds(1);
//        }
//        enemy.isAttaking = false;
//        ChangeState(NearEnemyState.Idle);
//    }

//    public Transform Target;

//    private void Circleshoot()
//    {
//        //360번 반복
//        for (int i = 0; i < 360; i += 13)
//        {
//            GameObject temp = Instantiate(prfFire);
//            Destroy(temp, 2f);
//            temp.transform.position = transform.position;
//            temp.transform.rotation = Quaternion.Euler(0, 0, i);
//        }
//    }

//    private void Fanshoot()
//    {
//        Vector3 vec = transform.rotation.eulerAngles;
//        for (int i = 0; i < 60; i += 10)
//        {
//            Debug.Log(transform.rotation.z);
//            GameObject temp = Instantiate(prfFire);
//            Destroy(temp, 2f);
//            temp.transform.position = transform.position;
//            temp.transform.Rotate(new Vector3(vec.x, vec.y, vec.z - 30 + i));
//        }
//    }

//}
