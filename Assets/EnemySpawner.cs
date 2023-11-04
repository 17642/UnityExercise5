using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemPrefab;//�� ������ ����
    [SerializeField]
    private float spawnTIme;//���� �ð�
    [SerializeField]
    private Transform[] wayPoints;//���� �������� �̵� ���
    // Start is called before the first frame update
    void Awake()
    {
        //�� ���� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemPrefab);//�������� �̿��� �� ����
            EnemyScript enemy = clone.GetComponent<EnemyScript>();//��� ������ ���� ������Ʈ
            enemy.Setup(wayPoints);//waypoint�� Setup ȣ��

            yield return new WaitForSeconds(spawnTIme);//SpawnTime���� ���
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
