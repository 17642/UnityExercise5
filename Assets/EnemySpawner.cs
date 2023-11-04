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
    [SerializeField]
    private GameObject enemyHPSliderPrefab;//적 체력 표시용 슬라이더 UI 프리팹
    [SerializeField]
    private Transform canvasTransform;//Canvas 오브젝트의 Transform
    [SerializeField]
    private playerHP playerHP;//플레이어 체력
    [SerializeField]
    private PlayerGold playerGold;//플레이어 골드
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

            SpawnEnemyHPSlider(clone);//해당 적의 HP Slider 추가

            yield return new WaitForSeconds(spawnTIme);//SpawnTime동안 대기
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
