using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; // 현재 스테이지의 모든 웨이브 정보
    [SerializeField]
    private EnemySpawner enemySpawner; //적 스포너 지정
    private int currentWaveIndex = -1;//현재 웨이브 인덱스

    //웨이브 정보 프로퍼티
    public int CurrentWave => currentWaveIndex + 1;//Wave는 0부터 지정되므로 +1해서 출력
    public int MaxWave => waves.Length;//웨이브 배열 길이 출력
    public void StartWave()
    {
        //현재 맵에 적이 없고&&wave가 남아있으면
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;//인덱스의 시작이 -1이므로 인덱스 증가 우선
            enemySpawner.StartWave(waves[currentWaveIndex]);//EnemySpawner의 startwave 함수 호출 및 웨이브 정보 제공
        }
    }

    
}

[System.Serializable]//구조체/클래스 직렬화. -> 인스펙터 뷰에서 설정 가능
public struct Wave
{
    public float spawnTime;//적 생성 주기(웨이브별)
    public int maxEnemyCount;//적 등장  숫자
    public GameObject[] enemyPrefabs;//적 등장 종류
}
