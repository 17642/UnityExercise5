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
    private List<EnemyScript> enemyList;//���� �ʿ� �����ϴ� �� ����Ʈ

    public List<EnemyScript> EnemyList => enemyList;//���� ������ ������ EnemySpawner���� �����ϹǷ� Set�� �ʿ����
    // Start is called before the first frame update
    void Awake()
    {
        //�� ���� �ڷ�ƾ �Լ� ȣ��
        enemyList = new List<EnemyScript>();//�� ����Ʈ �޸� �Ҵ�
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemPrefab);//�������� �̿��� �� ����
            EnemyScript enemy = clone.GetComponent<EnemyScript>();//��� ������ ���� ������Ʈ
            enemy.Setup(this,wayPoints);//waypoint�� Setup ȣ��
            enemyList.Add(enemy);//enemyList�� �� �߰�

            yield return new WaitForSeconds(spawnTIme);//SpawnTime���� ���
        }
    }
    // Update is called once per frame
    public void DestroyEnemy(EnemyScript enemy)
    {
        enemyList.Remove(enemy);//����Ʈ�� ����ϴ� �� ����
        Destroy(enemy.gameObject);//�� ������Ʈ ����
    }
}
