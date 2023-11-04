using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; // ���� ���������� ��� ���̺� ����
    [SerializeField]
    private EnemySpawner enemySpawner; //�� ������ ����
    private int currentWaveIndex = -1;//���� ���̺� �ε���

    //���̺� ���� ������Ƽ
    public int CurrentWave => currentWaveIndex + 1;//Wave�� 0���� �����ǹǷ� +1�ؼ� ���
    public int MaxWave => waves.Length;//���̺� �迭 ���� ���
    public void StartWave()
    {
        //���� �ʿ� ���� ����&&wave�� ����������
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;//�ε����� ������ -1�̹Ƿ� �ε��� ���� �켱
            enemySpawner.StartWave(waves[currentWaveIndex]);//EnemySpawner�� startwave �Լ� ȣ�� �� ���̺� ���� ����
        }
    }

    
}

[System.Serializable]//����ü/Ŭ���� ����ȭ. -> �ν����� �信�� ���� ����
public struct Wave
{
    public float spawnTime;//�� ���� �ֱ�(���̺꺰)
    public int maxEnemyCount;//�� ����  ����
    public GameObject[] enemyPrefabs;//�� ���� ����
}
