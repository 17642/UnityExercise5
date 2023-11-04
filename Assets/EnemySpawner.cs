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
    // Start is called before the first frame update
    void Awake()
    {
        //적 생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemPrefab);//프리팹을 이용해 적 생성
            EnemyScript enemy = clone.GetComponent<EnemyScript>();//방금 생성한 적의 컴포넌트
            enemy.Setup(wayPoints);//waypoint로 Setup 호출

            yield return new WaitForSeconds(spawnTIme);//SpawnTime동안 대기
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
