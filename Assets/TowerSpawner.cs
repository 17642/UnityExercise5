using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;//현재 맵에 존재하는 적 리스트 가져오기

    // Start is called before the first frame update
    public void SpawnTower(Transform tileTransform)   
    {
        Tile tile = tileTransform.GetComponent<Tile>();//선택된 타일의 컴포넌트 가져옴

        if (tile.IsbuildTower == true)
        {//타워가 이미 있으면
            return;//아무것도 하지 않는다.
        }
        //타워가 없으면 IsbuildTower를 True로 설정
        tile.IsbuildTower = true;
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);//선택한 타일 위치에 타워 생성
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);//타워 무기에 enemy 정보 전달

    }
}

//SpawnTower(): 매개변수 위치에 타일 생성
