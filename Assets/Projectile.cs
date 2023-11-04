using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;//�̵�
    private Transform target;//Ÿ��

    public void Setup(Transform target)
    {
        movement2D=GetComponent<Movement2D>();//movement2D�� ������Ʈ �����´�.
        this.target = target;//Tower�� ������ Target�� �����´�
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)//target�� �����ϸ�
        {
            Vector3 direction = (target.position - transform.position).normalized;//Ÿ�ٰ� �߻�ü�� ����� ��ġ
            movement2D.MoveTo(direction);//�߻�ü�� Ÿ������ �̵�
        }
        else//Ÿ���� ������
        {
            Destroy(gameObject);//�߻�ü ����
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;//���� �ƴ� ��ü�� ������ ����
        if (collision.transform != target) return;  //���� ����[��[ �¾����� Ÿ���� �ƴ� ��� ����

        collision.GetComponent<EnemyScript>().OnDie();//����ü�� Ÿ���� ������ ��� �� ��� ȣ��
        Destroy(gameObject);//�߻�ü ������Ʈ �ı�

    }
}
