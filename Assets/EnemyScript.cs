using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyDestroyType { kill=0,Arrive}
public class EnemyScript : MonoBehaviour
{
    private int wayPointCount;      //이동 경로 수
    private Transform[] wayPoints;  //이동 경로 정보
    private int currentIndex = 0;   //웨이포인트 인덱스
    private Movement2D movement2D;  //이동 제어
    private EnemySpawner spawner;//적의 삭제를 EnemySpawner에게 부여
    // Start is called before the first frame update
    [SerializeField]
    private int gold = 10;
    public void Setup(EnemySpawner spawner,Transform[] wayPoints)//웨이포인트 설정 함수
    {
        movement2D = GetComponent<Movement2D>();
        this.spawner = spawner;

        wayPointCount=wayPoints.Length;//wayPoint 정보 설정
        this.wayPoints = new Transform[wayPointCount];//wayPoint 지정
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;//첫 웨이포인트로 이동

        StartCoroutine("OnMove");//OnMove 코루틴으로 시작
    }

    private IEnumerator OnMove()
    {
        NextMoveto();//다음 이동 방향 설정

        while (true)//계속 지정
        {
            transform.Rotate(Vector3.forward * 10);//오브젝트 회전

            //현재 위치와 목표지의 거리가 이동속도*0.02보다 가까울 경우
            //moveSpeed를 곱하지 않으면 물체가 0.02를 넘어 경로를 탈주할 수 있음.
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed){
                NextMoveto();//다음 이동방향 설정
            }

            yield return null;
        }
    }

    private void NextMoveto()
    {
        if(currentIndex < wayPointCount-1) {//이동할 waypoint가 남아있을 경우

            transform.position = wayPoints[currentIndex].position;//현재 위치를 목표지점으로 설정
            currentIndex++;//인덱스 증가(목표지점을 다음 목표지점으로 설정
            //방향 설정
            Vector3 direction = (wayPoints[currentIndex].position-transform.position).normalized;
            movement2D.MoveTo(direction);

        }
        else//마지막 waypoint에 도달했다면
        {
            gold = 0;//gold를 얻을 수 없게 설정
            //Destroy(gameObject);//오브젝트 삭제
            OnDie(EnemyDestroyType.Arrive);//오브젝트 사망
        }
    }

    // Update is called once per frame
    public void OnDie(EnemyDestroyType type)//오브젝트가 사망할 때
    {
        //EnemySpawner가 처리를 하도록 함수 호출(List를 스포너에서 관리하므로)
        spawner.DestroyEnemy(type,this,gold);
    }
}
