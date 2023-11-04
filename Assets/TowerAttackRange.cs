using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        OffAttackRange();
    }

    public void OnAttackRange(Vector3 position, float range)
    {
        gameObject.SetActive(true);

        float diameter = range * 2.0f;//공격 범위 크기
        transform.localScale = Vector3.one * diameter;//크기를 공격 범위 크기로 할당

        transform.position = position;//공격 범위 위치
    }
    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}
