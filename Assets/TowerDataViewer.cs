using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image imageTower;//타워 이미지
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TowerAttackRange attackRange;

    private TowerWeapon currentTower;//현재 타워 정보
    void Awake()//기본적으로 패널 비활성화
    {
        OffPanel();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//ESC 입력 시
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)
    {
        currentTower = towerWeapon.GetComponent<TowerWeapon>();//출력해야 할 타워 정보
        attackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);//선택한 타워 위치와 사거리 정보 입력
        //Panel ON
        gameObject.SetActive(true);
        UpdateTowerData();//타워 정보 갱신

    }

    public void OffPanel()
    {
        //타워 정보 Panel OFF
        gameObject.SetActive(false);
        attackRange.OffAttackRange();//사거리 표시 비활성화
    }
    private void UpdateTowerData()
    {
        textDamage.text = "Damage: " + currentTower.Damage;
        textRate.text = "Rate: "+currentTower.Rate;
        textRange.text = "Tange: " + currentTower.Range;
        textLevel.text = "Level: " + currentTower.Level;
    }
}
