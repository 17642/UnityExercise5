using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    private TowerWeapon towerWeapon;
    // Start is called before the first frame update
    private void Awake()
    {
        
        towerWeapon = GetComponent<TowerWeapon>();//컴포넌트 불러옴
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))//적 태그가 Enemy가 아닐경우
        {
            return;//무시
        }

        Movement2D movement2D = collision.GetComponent<Movement2D>();

        movement2D.MoveSpeed -= movement2D.MoveSpeed * towerWeapon.Slow;//이동속도=이동속도-이동속도*감속률
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        collision.GetComponent<Movement2D>().ResetMoveSpeed();//이동속도 초기화

    }
}
