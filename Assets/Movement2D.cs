using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    public float MoveSpeed => moveSpeed;//moveSpeed 변수의 프로퍼티 ( get 가능 )

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    

    public void MoveTo(Vector3 direction)//이동 방향을 설정
    {
        moveDirection = direction;
    }
}
