using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;//타워스포너 지정
    [SerializeField]
    private TowerDataViewer towerDataViewer;

    private Camera mainCamera;//카메라 변수를 만드는 것이 직접 사용하는 것보다 효율적
    private Ray ray;
    private RaycastHit hit;
    // Start is called before the first frame update
    private void Awake()
    {//MainCamera 태그가 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();//와 동일
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//마우스 좌클릭을 하면
        {
            //ray.origin: 광선 시점(카메라 위치)
            //ray.direction: 광선의 진행방향
            ray=mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))//광선에 부딛히는 오브젝트 정보를 hit에 저장
            {
                if (hit.transform.CompareTag("Tile"))//hit에 저장된 오브젝트의 태그에 Tile이 있으면
                {
                    towerSpawner.SpawnTower(hit.transform);//해당 오브젝트 위치에 SpawnTower 실행
                }
                else if (hit.transform.CompareTag("Tower"))//Tower 태그가 붙은 오브젝트에 닿았을 때
                {
                    towerDataViewer.OnPanel(hit.transform);//타워 정보 패널을 켬
                }
            }
        }
    }
}
