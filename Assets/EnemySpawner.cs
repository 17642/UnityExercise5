using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Wave currentWave;//���� ���̺� ����
    //�Ʒ� ���� �ΰ� ��� ���̺� ���
    //[SerializeField]
    //private GameObject enemPrefab;//�� ������ ����
    //[SerializeField]
    //private float spawnTIme;//���� �ð�
    [SerializeField]
    private Transform[] wayPoints;//���� �������� �̵� ���
    [SerializeField]
    private GameObject enemyHPSliderPrefab;//�� ü�� ǥ�ÿ� �����̴� UI ������
    [SerializeField]
    private Transform canvasTransform;//Canvas ������Ʈ�� Transform
    [SerializeField]
    private playerHP playerHP;//�÷��̾� ü��
    [SerializeField]
    private PlayerGold playerGold;//�÷��̾� ���
    private List<EnemyScript> enemyList;//���� �ʿ� �����ϴ� �� ����Ʈ

    private int currentEnemyCount;//���� ���̺꿡 �����ִ� ��(���̺� ���۽� max/ �� ����ø��� -1)

    public List<EnemyScript> EnemyList => enemyList;//���� ������ ������ EnemySpawner���� �����ϹǷ� Set�� �ʿ����

    public int CurrentEnemyCount => currentEnemyCount;//���� ���̺꿡 �����ִ��� 
    public int MaxEnemyCount => currentWave.maxEnemyCount;//���� ���̺��� �ִ� ��
    // Start is called before the first frame update
    void Awake()
    {
        //�� ���� �ڷ�ƾ �Լ� ȣ��
        enemyList = new List<EnemyScript>();//�� ����Ʈ �޸� �Ҵ�
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        currentWave= wave;//���̺� ���� ����
        currentEnemyCount = currentWave.maxEnemyCount;//���� ���� ���� ���� ���̺��� �ִ� �� ���� ����
        StartCoroutine("SpawnEnemy");//�� ���� �ڷ�ƾ �Լ� ȣ��(���̺� ������ ��)
    }

    private IEnumerator SpawnEnemy()
    {

        int spawnEnemyCount = 0; //������ ���� ��
        //while (true)
        
        while(spawnEnemyCount<currentWave.maxEnemyCount)//���̺꿡 �Ҵ�� �� ���ڸ�ŭ��  ����
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);//������ �� ����
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);//�������� �̿��� �� ����
            EnemyScript enemy = clone.GetComponent<EnemyScript>();//��� ������ ���� ������Ʈ
            enemy.Setup(this,wayPoints);//waypoint�� Setup ȣ��
            enemyList.Add(enemy);//enemyList�� �� �߰�

            SpawnEnemyHPSlider(clone);//�ش� ���� HP Slider �߰�

            spawnEnemyCount++;//������ �� �� ����
            //Wave���� ���� �ð��� �ٸ� �� ����
            yield return new WaitForSeconds(currentWave.spawnTime);//SpawnTime���� ���
        }
    }
    // Update is called once per frame
    public void DestroyEnemy(EnemyDestroyType type, EnemyScript enemy,int gold)
    {
        if (type == EnemyDestroyType.Arrive)//���� �������� ��
        {
            playerHP.TakeDamage(1);//�÷��̾� ü�� -1
        }else if (type == EnemyDestroyType.kill)//���� �߻�ü�� ���� ������� ��
        {
            playerGold.CurrentGold += gold;//��� �߰�
        }
        currentEnemyCount--;//�� �� -1
        enemyList.Remove(enemy);//����Ʈ�� ����ϴ� �� ����
        Destroy(enemy.gameObject);//�� ������Ʈ ����
    }

    public void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);//�����̴� UI ����
        sliderClone.transform.SetParent(canvasTransform);//�����̴� UI�� ĵ������ �ڽ����� ����(�׷��� ȭ�鿡 ����)
        sliderClone.transform.localScale = Vector3.one;//�ڽ����� ����Ǿ� �ٲ� ũ�⸦ 1,1,1�� ����(vector3.one)

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);//�Ѿƴٴ� ����� enemy�� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());//Slider UI�� enemy�� ü�� ������ ǥ��
    }
}
