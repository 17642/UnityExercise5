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
    private bool isOnTowerButton = false;//Ÿ�� ��ư�� �������� Ȯ��
    private GameObject followTowerClone = null;//�ӽ�Ÿ�� ������ ���� ����

    // Start is called before the first frame update
    public void ReadyToSpawnTower()
    {
        if (isOnTowerButton == true)
        {
            return;//�̹� ��ư�� ���� ��� �ٽ� ��ư ������ ���� ����
        }
        if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)//Ÿ���� �Ǽ��� ��ŭ�� ��尡 �ִ��� Ȯ��
        {
            systemTextViewer.PrintText(SystemType.Money);//�ڱ� ���� ���
            return;//Ÿ�� �Ǽ� X
        }

        isOnTowerButton = true;//Ÿ�� ��ư ������ ����
        followTowerClone = Instantiate(towerTemplate.followTowerPrefab);//�ӽ� Ÿ�� ����
        StartCoroutine("OnTowerCancelSystem");//Ÿ�� �Ǽ� ��� ������ �ڷ�ƾ ����
    }
    public void SpawnTower(Transform tileTransform)   
    {
        if (isOnTowerButton == false)
        {
            return;//��ư�� ������ �ʾ��� ��� �Ǽ� X
        }
        //if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)//Ÿ�� �Ǽ��� �ʿ��� ��庸�� �÷��̾ ���� ��尡 ������ ���
        //{
        //    systemTextViewer.PrintText(SystemType.Money);//�ڱ� ���� ���
        //    return;//Ÿ�� �Ǽ� X
        //}
        Tile tile = tileTransform.GetComponent<Tile>();//���õ� Ÿ���� ������Ʈ ������

        if (tile.IsbuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);//�Ǽ� �Ұ� �޽��� ���
            //Ÿ���� �̹� ������
            return;//�ƹ��͵� ���� �ʴ´�.
        }
        isOnTowerButton = false;//�ٽ� Ÿ�� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ����
        //Ÿ���� ������ IsbuildTower�� True�� ����
        tile.IsbuildTower = true;
        playerGold.CurrentGold -= towerTemplate.weapon[0].cost;//Ÿ�� �Ǽ� ��븸ŭ ��� ����
        Vector3 position = tileTransform.position+Vector3.back;//Ÿ�Ϻ��� Z��-1�� ��ġ(Ÿ�Ϻ��� Ÿ�� �켱����_)
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);//������ Ÿ�� ��ġ�� Ÿ�� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner,playerGold,tile);//Ÿ�� ���⿡ enemy,���,Ÿ�� ���� ����
        Destroy(followTowerClone);//Ÿ�� ��ġ�� �Ϸ������Ƿ� �ӽ� Ÿ�� ����
        StopCoroutine("OnTowerCancelSystem");//Ÿ�� �Ǽ� ��� ������ �ڷ�ƾ ��

    }
    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.Escape)||Input.GetMouseButtonDown(1)) {//ESC Ű Ȥ�� ���콺 ��Ŭ�� ���� ��
                isOnTowerButton = false;//Ÿ�� �Ǽ� ���
                Destroy(followTowerClone);//�ӽ� Ÿ�� ����
                break;
            
            }
            yield return null;
        }
        
    }
}

//SpawnTower(): �Ű����� ��ġ�� Ÿ�� ����
