using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    //void Awake()
    //{
    //    OffAttackRange();
    //}ó�� ������Ʈ ��Ȱ��ȭ�� Awake�� ȣ����� �ʰ� �ٽ� Ȱ��ȭ�� �� ȣ��Ǿ� Range�� ������ ���� �� ����.

    public void OnAttackRange(Vector3 position, float range)
    {
        gameObject.SetActive(true);

        float diameter = range * 2.0f;//���� ���� ũ��
        transform.localScale = Vector3.one * diameter;//ũ�⸦ ���� ���� ũ��� �Ҵ�

        transform.position = position;//���� ���� ��ġ
    }
    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}
