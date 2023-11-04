using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyDestroyType { kill=0,Arrive}
public class EnemyScript : MonoBehaviour
{
    private int wayPointCount;      //�̵� ��� ��
    private Transform[] wayPoints;  //�̵� ��� ����
    private int currentIndex = 0;   //��������Ʈ �ε���
    private Movement2D movement2D;  //�̵� ����
    private EnemySpawner spawner;//���� ������ EnemySpawner���� �ο�
    // Start is called before the first frame update
    [SerializeField]
    private int gold = 10;
    public void Setup(EnemySpawner spawner,Transform[] wayPoints)//��������Ʈ ���� �Լ�
    {
        movement2D = GetComponent<Movement2D>();
        this.spawner = spawner;

        wayPointCount=wayPoints.Length;//wayPoint ���� ����
        this.wayPoints = new Transform[wayPointCount];//wayPoint ����
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;//ù ��������Ʈ�� �̵�

        StartCoroutine("OnMove");//OnMove �ڷ�ƾ���� ����
    }

    private IEnumerator OnMove()
    {
        NextMoveto();//���� �̵� ���� ����

        while (true)//��� ����
        {
            transform.Rotate(Vector3.forward * 10);//������Ʈ ȸ��

            //���� ��ġ�� ��ǥ���� �Ÿ��� �̵��ӵ�*0.02���� ����� ���
            //moveSpeed�� ������ ������ ��ü�� 0.02�� �Ѿ� ��θ� Ż���� �� ����.
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed){
                NextMoveto();//���� �̵����� ����
            }

            yield return null;
        }
    }

    private void NextMoveto()
    {
        if(currentIndex < wayPointCount-1) {//�̵��� waypoint�� �������� ���

            transform.position = wayPoints[currentIndex].position;//���� ��ġ�� ��ǥ�������� ����
            currentIndex++;//�ε��� ����(��ǥ������ ���� ��ǥ�������� ����
            //���� ����
            Vector3 direction = (wayPoints[currentIndex].position-transform.position).normalized;
            movement2D.MoveTo(direction);

        }
        else//������ waypoint�� �����ߴٸ�
        {
            gold = 0;//gold�� ���� �� ���� ����
            //Destroy(gameObject);//������Ʈ ����
            OnDie(EnemyDestroyType.Arrive);//������Ʈ ���
        }
    }

    // Update is called once per frame
    public void OnDie(EnemyDestroyType type)//������Ʈ�� ����� ��
    {
        //EnemySpawner�� ó���� �ϵ��� �Լ� ȣ��(List�� �����ʿ��� �����ϹǷ�)
        spawner.DestroyEnemy(type,this,gold);
    }
}
