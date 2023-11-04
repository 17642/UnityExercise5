using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;//Ÿ�������� ����
    [SerializeField]
    private TowerDataViewer towerDataViewer;

    private Camera mainCamera;//ī�޶� ������ ����� ���� ���� ����ϴ� �ͺ��� ȿ����
    private Ray ray;
    private RaycastHit hit;
    // Start is called before the first frame update
    private void Awake()
    {//MainCamera �±װ� �ִ� ������Ʈ Ž�� �� Camera ������Ʈ ���� ����
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();//�� ����
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//���콺 ��Ŭ���� �ϸ�
        {
            //ray.origin: ���� ����(ī�޶� ��ġ)
            //ray.direction: ������ �������
            ray=mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))//������ �ε����� ������Ʈ ������ hit�� ����
            {
                if (hit.transform.CompareTag("Tile"))//hit�� ����� ������Ʈ�� �±׿� Tile�� ������
                {
                    towerSpawner.SpawnTower(hit.transform);//�ش� ������Ʈ ��ġ�� SpawnTower ����
                }
                else if (hit.transform.CompareTag("Tower"))//Tower �±װ� ���� ������Ʈ�� ����� ��
                {
                    towerDataViewer.OnPanel(hit.transform);//Ÿ�� ���� �г��� ��
                }
            }
        }
    }
}
