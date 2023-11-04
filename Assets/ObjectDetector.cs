using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;//Ÿ�������� ����
    [SerializeField]
    private TowerDataViewer towerDataViewer;

    private Camera mainCamera;//ī�޶� ������ ����� ���� ���� ����ϴ� �ͺ��� ȿ����
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null; // ���콺�� ������ ������Ʈ �ӽ� ����
    // Start is called before the first frame update
    private void Awake()
    {//MainCamera �±װ� �ִ� ������Ʈ Ž�� �� Camera ������Ʈ ���� ����
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();//�� ����
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)//���콺�� UI�� �ӹ��� ���� ��
        {
            return;//�ڵ� ���� X
        }
        if (Input.GetMouseButtonDown(0))//���콺 ��Ŭ���� �ϸ�
        {
            //ray.origin: ���� ����(ī�޶� ��ġ)
            //ray.direction: ������ �������
            ray=mainCamera.ScreenPointToRay(Input.mousePosition);//2D ����͸� �̿��� 3D ������ ������Ʈ�� �����ϴ� ��

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))//������ �ε����� ������Ʈ ������ hit�� ����
            {
                hitTransform = hit.transform;
                if (hit.transform.CompareTag("Tile"))//hit�� ����� ������Ʈ�� �±׿� Tile�� ������
                {
                    towerSpawner.SpawnTower(hit.transform);//�ش� ������Ʈ ��ġ�� SpawnTower ����
                }
                else if (hit.transform.CompareTag("Tower"))//Tower �±װ� ���� ������Ʈ�� ����� ��
                {
                    towerDataViewer.OnPanel(hit.transform);//Ÿ�� ���� �г��� ��
                }
            }
        }else if (Input.GetMouseButtonUp(0))//���콺 ���� ��ư�� ���� ��
        {
            if (hitTransform == null || hitTransform.CompareTag("Tower") == false)//������ ������Ʈ�� ���ų� Ÿ���� �ƴ� ���
            {
                towerDataViewer.OffPanel();//Ÿ�� �г� off
            }
            hitTransform = null;//hitTransform �ʱ�ȭ
        }
    }
}
