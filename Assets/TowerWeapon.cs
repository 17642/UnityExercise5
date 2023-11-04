using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class TowerWeapon : MonoBehaviour { 
    [SerializeField]
    private GameObject projectilePrefab;//�߻�ü ������
    [SerializeField]
    private Transform spawnPoint;// �߻�ü ���� ��ġ
    [SerializeField]
    private float attackRate = 0.5f;//���� �ӵ�
    [SerializeField]
    private float attackRange = 2.0f;//��Ÿ�
    [SerializeField]
    private float attackDamage;//���ݷ�

    private WeaponState weaponState = WeaponState.SearchTarget;//WeaponState�� �⺻ ���´� SearchTarget
    private Transform attackTarget = null;//���� ��� ����
    private EnemySpawner enemySpawner;//������ ����(�� ���� ȹ��)

    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
        ChangeState(WeaponState.SearchTarget);//���� ���¸� SearchTarget���� ����
        
    }

    public void ChangeState(WeaponState state)
    {
        StopCoroutine(weaponState.ToString());//���� ������ ���� ����
        weaponState = state;//���� ����
        StartCoroutine(weaponState.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {//Ÿ���� ���� ȸ��
        //�������κ����� �Ÿ��� ���������κ����� ������ �̿��� ��ġ�� ����
        //����=arctan(y/x)
        //x,y���� ���ϱ�
        float dx = attackTarget.position.x - transform.position.x;
        float dy=attackTarget.position.y-transform.position.y;
        //�������� �̿��� ���� ���ϱ�
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;//������ ���� ����
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {//���� ����� �� Ž�� - �ش� �� ��Ÿ����� �ִ��� Ȯ�� - ������� �˻��� ������ ������� Ȯ��
            float closestDistSpr = Mathf.Infinity;//���� �Ÿ��� ���� ũ�� ����

            for(int i = 0;i<enemySpawner.EnemyList.Count;i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);//�Ÿ� ����
                if(distance<=attackRange&&distance<=closestDistSpr)//���� ���� ���� �ְ�, ���� ������
                {
                    closestDistSpr = distance;//���� ����� �Ÿ��� �ش� ������ ����
                    attackTarget = enemySpawner.EnemyList[i].transform;//���� Ÿ���� �ش� ������ ����
                }
            }
            if(attackTarget != null)//���� Ÿ���� �����ϸ�
            {
                ChangeState(WeaponState.AttackToTarget);//���������� ���� ����
            }
            yield return null;
        }
    }
    private IEnumerator AttackToTarget()
    {
        while(true)
        {
            if (attackTarget == null)//������ Ÿ���� ������
            {
                ChangeState(WeaponState.SearchTarget);//SearchTarget�� ���� ����
                break;
            }
            float distance = Vector3.Distance(attackTarget.position, transform.position);//�Ÿ� Ȯ��
            if (distance > attackRange)//�Ÿ��� ��Ÿ����� �ָ�
            {
                attackTarget = null;//Ÿ�� ����
                ChangeState(WeaponState.SearchTarget);//�ٽ� ���� ����
                break;
            }
            yield return new WaitForSeconds(attackRate);//���ݼӵ���ŭ ��� ��

            SpawnProjectile();//����
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);//�߻�ü ������ ����
        clone.GetComponent<Projectile>().Setup(attackTarget,attackDamage);//�߻�ü���� Ÿ�� �ο�
    }

}
