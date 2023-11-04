using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate;//Ÿ�� ����
    //[SerializeField]
   // private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;//���� �ʿ� �����ϴ� �� ����Ʈ ��������
    [SerializeField]
    private PlayerGold playerGold;//�÷��̾� ��� ��������
    [SerializeField]
    private SystemTextViewer systemTextViewer;//�ý��� �ؽ�Ʈ ���
    //[SerializeField]
    //private int towerBuildGold = 50;//Ÿ�� �Ǽ��� ���Ǵ� ���

    // Start is called before the first frame update
    public void SpawnTower(Transform tileTransform)   
    {
        if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)//Ÿ�� �Ǽ��� �ʿ��� ��庸�� �÷��̾ ���� ��尡 ������ ���
        {
            systemTextViewer.PrintText(SystemType.Money);//�ڱ� ���� ���
            return;//Ÿ�� �Ǽ� X
        }
        Tile tile = tileTransform.GetComponent<Tile>();//���õ� Ÿ���� ������Ʈ ������

        if (tile.IsbuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);//�Ǽ� �Ұ� �޽��� ���
            //Ÿ���� �̹� ������
            return;//�ƹ��͵� ���� �ʴ´�.
        }
        //Ÿ���� ������ IsbuildTower�� True�� ����
        tile.IsbuildTower = true;
        playerGold.CurrentGold -= towerTemplate.weapon[0].cost;//Ÿ�� �Ǽ� ��븸ŭ ��� ����
        Vector3 position = tileTransform.position+Vector3.back;//Ÿ�Ϻ��� Z��-1�� ��ġ(Ÿ�Ϻ��� Ÿ�� �켱����_)
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);//������ Ÿ�� ��ġ�� Ÿ�� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner,playerGold,tile);//Ÿ�� ���⿡ enemy,���,Ÿ�� ���� ����

    }
}

//SpawnTower(): �Ű����� ��ġ�� Ÿ�� ����
