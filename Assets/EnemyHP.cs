using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;//�ִ� ü����
    private float currentHP;//���� ü���� 
    private bool isDie = false;//�縶�� �󤼤� Ȯ���Ӥ�
    private EnemyScript enemy;
    private SpriteRenderer spriteRenderer;//��������Ʈ ������

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;//Set, Get ������Ƽ ����
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;//���� ü�¤��Ѥ� �ִ� ü�������Ѥ��� �����ä� 
        enemy = GetComponent<EnemyScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)//damage�� �޾� �ǰ� ����
    {
        if (isDie) return; //���� ���°� ��� ������ ��� �ڵ� ���� X(ü���� 0 �Ʒ��� �������� �� DIE�� ������ ����Ǵ� �� ����

        currentHP -= damage;//���� ü���� damage��ŭ ����

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");//�ǰ� ������ �� �ִϸ��̼� �ٽ� ����

        if (currentHP <= 0)//ü���� 0 �Ʒ��� �������� ��
        {
            isDie=true;//��� üũ�ϰ�
            enemy.OnDie(EnemyDestroyType.kill);//OnDie ����
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;//���� ������ color ������ ����

        color.a = 0.4f;//���� 0.4
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);//0,05�� ���

        color.a = 1.0f;
        spriteRenderer.color=color;//���� 100%�� ����
    }
    // Update is called once per frame
    
}
