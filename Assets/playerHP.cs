using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20; //최대 체력
    private float currentHP;// 현재 체력
    [SerializeField]
    private Image imageScreen;//붉은 화면 이미지

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;//프로퍼티 설정

    void Awake()
    {
        currentHP = maxHP;//현재 체력을 최대 체력으로 설정
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;//체력을 damage만큼 깎고

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if (currentHP <= 0)//체력이 0이 되면 게임 오버
        {

        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = imageScreen.color;//imageScreen의 색상
        color.a = 0.4f;//알파 0.4
        imageScreen.color = color;

        while (color.a >= 0.0f){//투명도가 0이 될때까지
            color.a -= Time.deltaTime;//감소
            imageScreen.color = color;

            yield return null;
        }
    }


}
