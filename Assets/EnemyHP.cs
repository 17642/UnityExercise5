using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;//최대 체려ㄱ
    private float currentHP;//현재 체려ㄱ 
    private bool isDie = false;//사마ㅇ 상ㅌㅐ 확ㅇㅣㄴ
    private EnemyScript enemy;
    private SpriteRenderer spriteRenderer;//스프라이트 렌더러

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;//Set, Get 프로퍼티 설정
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;//현재 체력ㅇㅡㄹ 최대 체려ㄱㅇㅡㄹㅗ 설ㅈㅓㅇ 
        enemy = GetComponent<EnemyScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)//damage를 받아 피격 실행
    {
        if (isDie) return; //적의 상태가 사망 상태일 경우 코드 실행 X(체력이 0 아래로 떨어졌을 때 DIE가 여러번 실행되는 것 방지

        currentHP -= damage;//현재 체력을 damage만큼 감소

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");//피격 당했을 때 애니메이션 다시 실행

        if (currentHP <= 0)//체력이 0 아래로 떨어졌을 때
        {
            isDie=true;//사망 체크하고
            enemy.OnDie(EnemyDestroyType.kill);//OnDie 실행
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;//적의 색상을 color 변수에 저장

        color.a = 0.4f;//투명도 0.4
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);//0,05초 대기

        color.a = 1.0f;
        spriteRenderer.color=color;//투명도 100%로 설정
    }
    // Update is called once per frame
    
}
