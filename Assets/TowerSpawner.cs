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

    // Start is called before the first frame update
    public void SpawnTower(Transform tileTransform)   
    {
        Tile tile = tileTransform.GetComponent<Tile>();//���õ� Ÿ���� ������Ʈ ������

        if (tile.IsbuildTower == true)
        {//Ÿ���� �̹� ������
            return;//�ƹ��͵� ���� �ʴ´�.
        }
        //Ÿ���� ������ IsbuildTower�� True�� ����
        tile.IsbuildTower = true;
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);//������ Ÿ�� ��ġ�� Ÿ�� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);//Ÿ�� ���⿡ enemy ���� ����

    }
}

//SpawnTower(): �Ű����� ��ġ�� Ÿ�� ����
