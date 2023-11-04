using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public enum WeaponType { Cannon=0, Laser}
public enum WeaponState { SearchTarget = 0, TryAttackCannon, TryAttackLaser } //AttackToTarget }//���� ���� ���

public class TowerWeapon : MonoBehaviour {
    [Header("Commons")]//�ν����� â�� ǥ�õǴ� �������� �뵵���� ����. -> ���ڿ� ������ ���� ǥ��
    [SerializeField]
    private WeaponType weaponType;//���� �Ӽ� ����
    [SerializeField]
    private TowerTemplate towerTemplate;//Ÿ�� ����(���ݷ�, ���ݼӵ� ��)
    [SerializeField]
    private Transform spawnPoint;// �߻�ü ���� ��ġ

    [Header("Cannon")]
    [SerializeField]
    private GameObject projectilePrefab;//�߻�ü ������

    [Header("Laser")]
    [SerializeField]
    private LineRenderer lineRenderer;//������ ��(Line)
    [SerializeField]
    private Transform hitEffect;//Ÿ�� ȿ��
    [SerializeField]
    private LayerMask targetLayer;//������ �ε����� ���̾� ����
    
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
        {
            //���� ���� �ڵ� ��ü
            /*
            //���� ����� �� Ž�� - �ش� �� ��Ÿ����� �ִ��� Ȯ�� - ������� �˻��� ������ ������� Ȯ��
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
                ChangeState(WeaponState.TryAttackCannon);//���������� ���� ����
            }
            */
            attackTarget = FineClosestAttackTarget();
            if (attackTarget != null)
            {
                if (weaponType == WeaponType.Cannon)//���� �Ӽ��� ���� ���� ��� ����
                {
                    ChangeState(WeaponState.TryAttackCannon);
                }
                else if (weaponType == WeaponType.Laser)
                {
                    ChangeState(WeaponState.TryAttackLaser);
                }
            }
            yield return null;
        }
    }
    //private IEnumerator AttackToTarget()//��ü��.
    private IEnumerator TryAttackCannon()
    {
        while(true)
        {
            /*
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
            */

            //Target ���� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);//Ÿ���� ������ �� ���� ��� �ٽ� Ÿ�� Ž��
                break;
            }
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);//���� �ӵ���ŭ ���
        }
    }
    private IEnumerator TryAttackLaser() 
    {
        EnableLaser();//������ ȿ�� Ȱ��ȭ

        while(true)
        {
            if (IsPossibleToAttackTarget() == false)//Target�� ������ �� ������
            {
                DisableLaser();//������ ����
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            SpawnLaser();//������ ����

            yield return null;
        }
    }

    private Transform FineClosestAttackTarget()
    {
        float closestDistSpr = Mathf.Infinity;//���� �Ÿ��� ���� ũ�� ����

        for(int i = 0; i < enemySpawner.EnemyList.Count; i++)//EnemyList �ȿ� �ִ� �� �˻�
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            if (distance <= towerTemplate.weapon[level].range && distance <= closestDistSpr)//��Ÿ� ���� �ְ�, ���ݱ��� ������ ������ �Ÿ��� �� ����� ���
            {
                closestDistSpr = distance;//���� ����� �Ÿ� ������Ʈ
                attackTarget = enemySpawner.EnemyList[i].transform;//���� ��� ����
            }
        }
        return attackTarget;
    }

    private bool IsPossibleToAttackTarget()
    {
        if(attackTarget == null)//Ÿ���� �������� ���� ���
        {
            return false;
        }
        float distance = Vector3.Distance(attackTarget.position, transform.position);//Ÿ�ٰ� ���� �Ÿ���
        if (distance > towerTemplate.weapon[level].range)//Ÿ���� ��Ÿ����� �� ��
        {
            attackTarget = null; // Ÿ�� �ʱ�ȭ
            return false;
        }
        return true;//���� ������ ���� ����
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

        if (weaponType == WeaponType.Laser)//���� �Ӽ��� �������� ���
        {
            lineRenderer.startWidth = 0.05f + level * 0.05f;//������ ���� ������ ���� ����
            lineRenderer.endWidth = 0.05f;
        }
        return true;//Ÿ�� ���׷��̵� ����
    }

    public void Sell()
    {
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;//��� ����
        ownerTile.IsbuildTower = false;//Ÿ�� �Ǽ� �ٽ� �����ϰ� ����
        Destroy(gameObject);//Ÿ�� ������Ʈ �ı�
    }

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);//�������� �ǰ� ȿ�� Ȱ��ȭ
    }

    private void DisableLaser() 
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);//�������� �ǰ� ȿ�� ��Ȱ��ȭ
    }

    private void SpawnLaser()
    {
        Vector3 direction = attackTarget.position - spawnPoint.position;//���� ����
        //���� �������� ���� �������� ���� attackTarget�� ������ ������Ʈ�� ����(�ٸ� ���� Ÿ�ٰ� ��ž ���̿� ���� �� Ÿ���� �����ϱ� ����.)
        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction, towerTemplate.weapon[level].range, targetLayer);
    
        for(int i=0;i<hit.Length; i++)
        {
            if (hit[i].transform == attackTarget)//���� �� �ϳ��� ������Ʈ�� �¾����� ���� ��ġ ����
            {
                lineRenderer.SetPosition(0,spawnPoint.position);//���� ��������
                lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);//���� ������
                hitEffect.position = hit[i].point;//����Ʈ ��ġ
                attackTarget.GetComponent<EnemyHP>().TakeDamage(towerTemplate.weapon[level].damage*Time.deltaTime);//�� ü�� ����(1�ʴ� damage)
            }
        }
    }

}
