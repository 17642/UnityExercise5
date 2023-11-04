using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Slider hpSlider;//슬라이드를 이용해 체력 디스플레이
    // Start is called before the first frame update
    public void Setup(EnemyHP enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider = GetComponent<Slider>();//컴포넌트 정보 저장
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = enemyHP.CurrentHP / enemyHP.MaxHP;//슬라이더 값을 현재 체력/최대 체력으로 설정
    }
}
