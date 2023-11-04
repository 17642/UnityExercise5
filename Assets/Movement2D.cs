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

    private float baseMoveSpeed;//�⺻ �̵��ӵ�
    // Start is called before the first frame update
    //public float MoveSpeed => moveSpeed;//moveSpeed ������ ������Ƽ ( get ���� )
    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Max(0, value);//�̵��ӵ��� ������ 0���� ŭ
        get => moveSpeed;
    }

    private void Awake()
    {
        baseMoveSpeed = moveSpeed;//�̵��ӵ��� �⺻���� ����
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    

    public void MoveTo(Vector3 direction)//�̵� ������ ����
    {
        moveDirection = direction;
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = baseMoveSpeed;//�̵��ӵ� ����
    }
}
