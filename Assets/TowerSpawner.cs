using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate;//타워 정보
    //[SerializeField]
   // private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;//현재 맵에 존재하는 적 리스트 가져오기
    [SerializeField]
    private PlayerGold playerGold;//플레이어 골드 가져오기
    [SerializeField]
    private SystemTextViewer systemTextViewer;//시스템 텍스트 뷰어
    //[SerializeField]
    //private int towerBuildGold = 50;//타워 건설에 사용되는 골드

    // Start is called before the first frame update
    public void SpawnTower(Transform tileTransform)   
    {
        if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)//타워 건설에 필요한 골드보다 플레이어가 가진 골드가 부족할 경우
        {
            systemTextViewer.PrintText(SystemType.Money);//자금 부족 출력
            return;//타워 건설 X
        }
        Tile tile = tileTransform.GetComponent<Tile>();//선택된 타일의 컴포넌트 가져옴

        if (tile.IsbuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);//건설 불가 메시지 출력
            //타워가 이미 있으면
            return;//아무것도 하지 않는다.
        }
        //타워가 없으면 IsbuildTower를 True로 설정
        tile.IsbuildTower = true;
        playerGold.CurrentGold -= towerTemplate.weapon[0].cost;//타워 건설 비용만큼 골드 감소
        Vector3 position = tileTransform.position+Vector3.back;//타일보다 Z축-1의 위치(타일보다 타워 우선선택_)
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);//선택한 타일 위치에 타워 생성
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner,playerGold,tile);//타워 무기에 enemy,골드,타일 정보 전달

    }
}

//SpawnTower(): 매개변수 위치에 타일 생성
