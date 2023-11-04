using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    private TowerWeapon towerWeapon;
    // Start is called before the first frame update
    private void Awake()
    {
        
        towerWeapon = GetComponent<TowerWeapon>();//������Ʈ �ҷ���
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))//�� �±װ� Enemy�� �ƴҰ��
        {
            return;//����
        }

        Movement2D movement2D = collision.GetComponent<Movement2D>();

        movement2D.MoveSpeed -= movement2D.MoveSpeed * towerWeapon.Slow;//�̵��ӵ�=�̵��ӵ�-�̵��ӵ�*���ӷ�
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        collision.GetComponent<Movement2D>().ResetMoveSpeed();//�̵��ӵ� �ʱ�ȭ

    }
}
