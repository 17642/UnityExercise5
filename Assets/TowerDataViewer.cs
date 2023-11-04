using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image imageTower;//Ÿ�� �̹���
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
    private Button buttonUpgrade;//���׷��̵� ��ư Ȱ��ȭ/��Ȱ��ȭ �뵵
    [SerializeField]
    private SystemTextViewer systemTextViewer;//�ý��� �ؽ�Ʈ ���
    [SerializeField]
    private TextMeshProUGUI textUpgradeCost;
    [SerializeField]
    private TextMeshProUGUI textSellCost;

    private TowerWeapon currentTower;//���� Ÿ�� ����
    void Awake()//�⺻������ �г� ��Ȱ��ȭ
    {
        OffPanel();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//ESC �Է� ��
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)
    {
        currentTower = towerWeapon.GetComponent<TowerWeapon>();//����ؾ� �� Ÿ�� ����
        attackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);//������ Ÿ�� ��ġ�� ��Ÿ� ���� �Է�
        //Panel ON
        gameObject.SetActive(true);
        UpdateTowerData();//Ÿ�� ���� ����

    }

    public void OffPanel()
    {
        //Ÿ�� ���� Panel OFF
        gameObject.SetActive(false);
        attackRange.OffAttackRange();//��Ÿ� ǥ�� ��Ȱ��ȭ
    }
    private void UpdateTowerData()
    {
        if (currentTower.WeaponType == WeaponType.Cannon || currentTower.WeaponType == WeaponType.Laser)//Ÿ���� �������� �ִ� Ÿ����
        {
            imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = "Damage: " + currentTower.Damage+"+"+"<color=red>"+currentTower.AddedDamage.ToString("F1")+"</color>";
            //�ؽ�Ʈ �ڿ� ���������� �߰� ���ݷ� ���
        }
        else
        {
            imageTower.rectTransform.sizeDelta = new Vector2(59, 59);//ũ�⸦ ��������Ʈ ũ�⿡ �°� ����
            if (currentTower.WeaponType == WeaponType.Slow)//Ÿ�� �Ӽ��� Slow��
            {
                textDamage.text = "Slow: " + currentTower.Slow * 100 + "%";//���ӷ� ���
            }else if (currentTower.WeaponType == WeaponType.Buff)//Ÿ�� �Ӽ��� Buff�̸�
            {
                textDamage.text = "Buff: " + currentTower.Buff * 100 + "%";//���ӷ� ���
            }
        }
        imageTower.sprite = currentTower.TowerSprite;//�̹����� ���� Ÿ���� �̹����� ����(������ ���� ����)
        //textDamage.text = "Damage: " + currentTower.Damage;
        textRate.text = "Rate: "+currentTower.Rate;
        textRange.text = "Tange: " + currentTower.Range;
        textLevel.text = "Level: " + currentTower.Level;

        textUpgradeCost.text = currentTower.Upgragecost.ToString();
        textSellCost.text = currentTower.SellCost.ToString();

        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;//������ MaxLevel���� Ŭ ��� ��ư ��Ȱ��ȭ
    }
     public void OnClickEventTowerUpgrade()
    {
        bool isSuccess = currentTower.Upgrade();//Ÿ�� ���׷��̵� �õ�(���: bool)
        if (isSuccess)
        {
            UpdateTowerData();//Ÿ�� ���� ����
            attackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);//Ÿ�� ��Ÿ� ����
        }
        else
        {
            //Ÿ�� ���׷��̵� ��� �����ϴ� ���
            systemTextViewer.PrintText(SystemType.Money);
        }
    }

    public void OnClickEventTowerSell()
    {
        currentTower.Sell();//Ÿ�� �Ǹ�
        OffPanel();//Ÿ���� ���������Ƿ� �г� �ݱ�, ���ݹ��� off
    }

}
