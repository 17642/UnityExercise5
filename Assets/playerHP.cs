using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20; //�ִ� ü��
    private float currentHP;// ���� ü��
    [SerializeField]
    private Image imageScreen;//���� ȭ�� �̹���

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;//������Ƽ ����

    void Awake()
    {
        currentHP = maxHP;//���� ü���� �ִ� ü������ ����
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;//ü���� damage��ŭ ���

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if (currentHP <= 0)//ü���� 0�� �Ǹ� ���� ����
        {

        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = imageScreen.color;//imageScreen�� ����
        color.a = 0.4f;//���� 0.4
        imageScreen.color = color;

        while (color.a >= 0.0f){//������ 0�� �ɶ�����
            color.a -= Time.deltaTime;//����
            imageScreen.color = color;

            yield return null;
        }
    }


}
