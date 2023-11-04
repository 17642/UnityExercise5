using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Wave currentWave;//현재 웨이브 정보
    //아래 변수 두개 대신 웨이브 사용
    //[SerializeField]
    //private GameObject enemPrefab;//적 프리팹 설정
    //[SerializeField]
    //private float spawnTIme;//스폰 시간
    [SerializeField]
    private Transform[] wayPoints;//현재 스테이지 이동 경로
    [SerializeField]
    private GameObject enemyHPSliderPrefab;//적 체력 표시용 슬라이더 UI 프리팹
    [SerializeField]
    private Transform canvasTransform;//Canvas 오브젝트의 Transform
    [SerializeField]
    private playerHP playerHP;//플레이어 체력
    [SerializeField]
    private PlayerGold playerGold;//플레이어 골드
    private List<EnemyScript> enemyList;//현재 맵에 존재하는 적 리스트

    private int currentEnemyCount;//현재 웨이브에 남아있는 적(웨이브 시작시 max/ 적 사망시마다 -1)

    public List<EnemyScript> EnemyList => enemyList;//적의 생성과 삭제를 EnemySpawner에서 실행하므로 Set는 필요없음

    public int CurrentEnemyCount => currentEnemyCount;//현재 웨이브에 남아있는적 
    public int MaxEnemyCount => currentWave.maxEnemyCount;//현재 웨이브의 최대 적
    // Start is called before the first frame update
    void Awake()
    {
        //적 생성 코루틴 함수 호출
        enemyList = new List<EnemyScript>();//적 리스트 메모리 할당
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        currentWave= wave;//웨이브 정보 저장
        currentEnemyCount = currentWave.maxEnemyCount;//현재 적의 수를 현재 웨이브의 최대 적 수로 지정
        StartCoroutine("SpawnEnemy");//적 생성 코루틴 함수 호출(웨이브 시작할 때)
    }

    private IEnumerator SpawnEnemy()
    {

        int spawnEnemyCount = 0; //생성한 적의 수
        //while (true)
        
        while(spawnEnemyCount<currentWave.maxEnemyCount)//웨이브에 할당된 적 숫자만큼만  생성
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);//임의의 적 지정
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);//프리팹을 이용해 적 생성
            EnemyScript enemy = clone.GetComponent<EnemyScript>();//방금 생성한 적의 컴포넌트
            enemy.Setup(this,wayPoints);//waypoint로 Setup 호출
            enemyList.Add(enemy);//enemyList에 적 추가

            SpawnEnemyHPSlider(clone);//해당 적의 HP Slider 추가

            spawnEnemyCount++;//생성한 적 수 증가
            //Wave마다 스폰 시간이 다를 수 있음
            yield return new WaitForSeconds(currentWave.spawnTime);//SpawnTime동안 대기
        }
    }
    // Update is called once per frame
    public void DestroyEnemy(EnemyDestroyType type, EnemyScript enemy,int gold)
    {
        if (type == EnemyDestroyType.Arrive)//적이 도착했을 때
        {
            playerHP.TakeDamage(1);//플레이어 체력 -1
        }else if (type == EnemyDestroyType.kill)//적이 발사체에 의해 사망했을 때
        {
            playerGold.CurrentGold += gold;//골드 추가
        }
        currentEnemyCount--;//적 수 -1
        enemyList.Remove(enemy);//리스트에 사망하는 적 삭제
        Destroy(enemy.gameObject);//적 오브젝트 삭제
    }

    public void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);//슬라이더 UI 생성
        sliderClone.transform.SetParent(canvasTransform);//슬라이더 UI를 캔버스의 자식으로 설정(그래야 화면에 보임)
        sliderClone.transform.localScale = Vector3.one;//자식으로 변경되어 바뀐 크기를 1,1,1로 설정(vector3.one)

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);//쫓아다닐 대상을 enemy로 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());//Slider UI에 enemy의 체력 정보를 표시
    }
}
