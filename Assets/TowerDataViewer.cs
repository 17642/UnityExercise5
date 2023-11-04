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
        textDamage.text = "Damage: " + currentTower.Damage;
        textRate.text = "Rate: "+currentTower.Rate;
        textRange.text = "Tange: " + currentTower.Range;
        textLevel.text = "Level: " + currentTower.Level;
    }
}
