using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class TowerWeapon : MonoBehaviour {
    [SerializeField]
    private TowerTemplate towerTemplate;//Ÿ�� ����(���ݷ�, ���ݼӵ� ��)
    [SerializeField]
    private GameObject projectilePrefab;//�߻�ü ������
    [SerializeField]
    private Transform spawnPoint;// �߻�ü ���� ��ġ
    //[SerializeField]
    //private float attackRate = 0.5f;//���� �ӵ�
    //[SerializeField]
    //private float attackRange = 2.0f;//��Ÿ�
   // [SerializeField]
    //private float attackDamage = 1;//���ݷ�

    private int level = 0;//����

    private WeaponState weaponState = WeaponState.SearchTarget;//WeaponState�� �⺻ ���´� SearchTarget
    private Transform attackTarget = null;//���� ��� ����
    private EnemySpawner enemySpawner;//������ ����(�� ���� ȹ��)
    private SpriteRenderer spriteRenderer;// ��������Ʈ �̹��� �����
    private PlayerGold playerGold;//�÷��̾��� ��� ���� �� ���� 
    private Tile ownerTile;//Ÿ���� ��ġ�� Ÿ��

    //Ÿ�� ������ ���� ������Ƽ
    //public float Damage => attackDamage;
    //public float Rate => attackRate;
    // public float Range => attackRange;
    public int Level => level+1;//level�� 0���� �����ϹǷ�
    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float Damage => towerTemplate.weapon[level].damage;
    public float Rate => towerTemplate.weapon[level].rate;
    public float Range => towerTemplate.weapon[level].range;//���� towerTemplate �迭�� ��ҷ� ����
    public int MaxLevel => towerTemplate.weapon.Length;//���� �ִ�ġ


    public void Setup(EnemySpawner enemySpawner,PlayerGold playerGold,Tile ownerTile)
    {
        spriteRenderer=GetComponent<SpriteRenderer>();//������Ʈ �ҷ�����
        this.enemySpawner = enemySpawner;
        ChangeState(WeaponState.SearchTarget);//���� ���¸� SearchTarget���� ����
        this.playerGold = playerGold;//�÷��̾� ��� ����
        this.ownerTile = ownerTile;
        
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
                if (distance <= towerTemplate.weapon[level].range &&distance<=closestDistSpr)//���� ���� ���� �ְ�, ���� ������
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
            if (distance > towerTemplate.weapon[level].range)//�Ÿ��� ��Ÿ����� �ָ�
            {
                attackTarget = null;//Ÿ�� ����
                ChangeState(WeaponState.SearchTarget);//�ٽ� ���� ����
                break;
            }
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);//���ݼӵ���ŭ ��� ��

            SpawnProjectile();//����
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);//�߻�ü ������ ����
        clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage);//�߻�ü���� Ÿ�� �ο�
    }

    public bool Upgrade()
    {
        if (playerGold.CurrentGold < towerTemplate.weapon[level + 1].cost)//���׷��̵� ����� ������ ���
        {
            return false;
        }
        //Ÿ�� ���� ����
        level++;
        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;//��������Ʈ ����
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;//�� ����
        return true;//Ÿ�� ���׷��̵� ����
    }

    public void Sell()
    {
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;//��� ����
        ownerTile.IsbuildTower = false;//Ÿ�� �Ǽ� �ٽ� �����ϰ� ����
        Destroy(gameObject);//Ÿ�� ������Ʈ �ı�
    }

}
