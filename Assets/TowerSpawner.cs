using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;//���� �ʿ� �����ϴ� �� ����Ʈ ��������
    [SerializeField]
    private PlayerGold playerGold;//�÷��̾� ��� ��������
    [SerializeField]
    private int towerBuildGold = 50;//Ÿ�� �Ǽ��� ���Ǵ� ���

    // Start is called before the first frame update
    public void SpawnTower(Transform tileTransform)   
    {
        if (towerBuildGold > playerGold.CurrentGold)//Ÿ�� �Ǽ��� �ʿ��� ��庸�� �÷��̾ ���� ��尡 ������ ���
        {
            return;//Ÿ�� �Ǽ� X
        }
        Tile tile = tileTransform.GetComponent<Tile>();//���õ� Ÿ���� ������Ʈ ������

        if (tile.IsbuildTower == true)
        {//Ÿ���� �̹� ������
            return;//�ƹ��͵� ���� �ʴ´�.
        }
        //Ÿ���� ������ IsbuildTower�� True�� ����
        tile.IsbuildTower = true;
        playerGold.CurrentGold -= towerBuildGold;//Ÿ�� �Ǽ� ��븸ŭ ��� ����
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);//������ Ÿ�� ��ġ�� Ÿ�� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);//Ÿ�� ���⿡ enemy ���� ����

    }
}

//SpawnTower(): �Ű����� ��ġ�� Ÿ�� ����
