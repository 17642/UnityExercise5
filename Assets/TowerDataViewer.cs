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
    [SerializeField]
    private Button buttonUpgrade;//업그레이드 버튼 활성화/비활성화 용도
    [SerializeField]
    private SystemTextViewer systemTextViewer;//시스템 텍스트 뷰어

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
        if (currentTower.WeaponType == WeaponType.Cannon || currentTower.WeaponType == WeaponType.Laser)//타워가 데미지를 주는 타워면
        {
            imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = "Damage: " + currentTower.Damage;
        }
        else
        {
            imageTower.rectTransform.sizeDelta = new Vector2(59, 59);//크기를 스프라이트 크기에 맞게 변경
            textDamage.text = "Slow: " + currentTower.Slow*100+"%";//감속률 출력
        }
        imageTower.sprite = currentTower.TowerSprite;//이미지를 현재 타워의 이미지로 설정(레벨에 따라 갱신)
        //textDamage.text = "Damage: " + currentTower.Damage;
        textRate.text = "Rate: "+currentTower.Rate;
        textRange.text = "Tange: " + currentTower.Range;
        textLevel.text = "Level: " + currentTower.Level;

        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;//레벨이 MaxLevel보다 클 경우 버튼 비활성화
    }
     public void OnClickEventTowerUpgrade()
    {
        bool isSuccess = currentTower.Upgrade();//타워 업그레이드 시도(결과: bool)
        if (isSuccess)
        {
            UpdateTowerData();//타워 정보 갱신
            attackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);//타워 사거리 갱신
        }
        else
        {
            //타워 업그레이드 비용 부족하다 출력
            systemTextViewer.PrintText(SystemType.Money);
        }
    }

    public void OnClickEventTowerSell()
    {
        currentTower.Sell();//타워 판매
        OffPanel();//타워가 없어졌으므로 패널 닫기, 공격범위 off
    }

}
