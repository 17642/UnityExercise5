using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;//메인카메라 지저ㅇ
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);//화면의 마우스 좌표 기준으로 월드상의 좌표 추출
        transform.position=mainCamera.ScreenToWorldPoint(position);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);//Z 위치 0 설정
    }
}

