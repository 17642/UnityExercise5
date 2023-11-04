using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;//����ī�޶� ������
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);//ȭ���� ���콺 ��ǥ �������� ������� ��ǥ ����
        transform.position=mainCamera.ScreenToWorldPoint(position);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);//Z ��ġ 0 ����
    }
}

