using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class TowerWeapon : MonoBehaviour { 
    [SerializeField]
    private GameObject projectilePrefab;//발사체 프리팹
    [SerializeField]
    private Transform spawnPoint;// 발사체 스폰 위치
    [SerializeField]
    private float attackRate = 0.5f;//공격 속도
    [SerializeField]
    private float attackRange = 2.0f;//사거리
    [SerializeField]
    private float attackDamage;//공격력

    private WeaponState weaponState = WeaponState.SearchTarget;//WeaponState의 기본 상태는 SearchTarget
    private Transform attackTarget = null;//공격 대상 없음
    private EnemySpawner enemySpawner;//스포너 지정(적 정보 획득)

    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
        ChangeState(WeaponState.SearchTarget);//최초 상태를 SearchTarget으로 지정
        
    }

    public void ChangeState(WeaponState state)
    {
        StopCoroutine(weaponState.ToString());//이전 실행중 상태 종료
        weaponState = state;//상태 변경
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
    {//타겟을 향해 회전
        //원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구함
        //각도=arctan(y/x)
        //x,y변위 구하기
        float dx = attackTarget.position.x - transform.position.x;
        float dy=attackTarget.position.y-transform.position.y;
        //변위값을 이용해 각도 구하기
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;//라디안을 도로 변경
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {//제일 가까운 적 탐색 - 해당 적 사거리내에 있는지 확인 - 현재까지 검사한 적보다 가까운지 확인
            float closestDistSpr = Mathf.Infinity;//최초 거리를 가장 크게 설정

            for(int i = 0;i<enemySpawner.EnemyList.Count;i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);//거리 구함
                if(distance<=attackRange&&distance<=closestDistSpr)//공격 범위 내에 있고, 가장 가까우면
                {
                    closestDistSpr = distance;//가장 가까운 거리를 해당 적으로 설정
                    attackTarget = enemySpawner.EnemyList[i].transform;//공격 타겟을 해당 적으로 설정
                }
            }
            if(attackTarget != null)//공격 타겟이 존재하면
            {
                ChangeState(WeaponState.AttackToTarget);//공격중으로 상태 변경
            }
            yield return null;
        }
    }
    private IEnumerator AttackToTarget()
    {
        while(true)
        {
            if (attackTarget == null)//공격할 타겟이 없으면
            {
                ChangeState(WeaponState.SearchTarget);//SearchTarget로 상태 변경
                break;
            }
            float distance = Vector3.Distance(attackTarget.position, transform.position);//거리 확인
            if (distance > attackRange)//거리가 사거리보다 멀면
            {
                attackTarget = null;//타겟 제거
                ChangeState(WeaponState.SearchTarget);//다시 상태 변경
                break;
            }
            yield return new WaitForSeconds(attackRate);//공격속도만큼 대기 후

            SpawnProjectile();//공격
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);//발사체 프리팹 생성
        clone.GetComponent<Projectile>().Setup(attackTarget,attackDamage);//발사체에게 타겟 부여
    }

}
