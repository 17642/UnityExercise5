using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemPrefab;//적 프리팹 설정
    [SerializeField]
    private float spawnTIme;//스폰 시간
    [SerializeField]
    private Transform[] wayPoints;//현재 스테이지 이동 경로
    private List<EnemyScript> enemyList;//현재 맵에 존재하는 적 리스트

    public List<EnemyScript> EnemyList => enemyList;//적의 생성과 삭제를 EnemySpawner에서 실행하므로 Set는 필요없음
    // Start is called before the first frame update
    void Awake()
    {
        //적 생성 코루틴 함수 호출
        enemyList = new List<EnemyScript>();//적 리스트 메모리 할당
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemPrefab);//프리팹을 이용해 적 생성
            EnemyScript enemy = clone.GetComponent<EnemyScript>();//방금 생성한 적의 컴포넌트
            enemy.Setup(this,wayPoints);//waypoint로 Setup 호출
            enemyList.Add(enemy);//enemyList에 적 추가

            yield return new WaitForSeconds(spawnTIme);//SpawnTime동안 대기
        }
    }
    // Update is called once per frame
    public void DestroyEnemy(EnemyScript enemy)
    {
        enemyList.Remove(enemy);//리스트에 사망하는 적 삭제
        Destroy(enemy.gameObject);//적 오브젝트 삭제
    }
}
