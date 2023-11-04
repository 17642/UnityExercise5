using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 20.0f;//UI�� ǥ�õ� ��ġ
    private Transform targetTransform;//HP�� ǥ���� ��
    private RectTransform rectTransform;//UI�� ��ġ ����

    // Start is called before the first frame update
    
    public void Setup(Transform target)
    {
        targetTransform = target;//Ÿ�� ����
        rectTransform = GetComponent<RectTransform>();//������Ʈ ���� ��������
    }
    void LateUpdate()//��ġ ���� ���� UI �̵��� ���� LateUpdate ���
    {
        if (targetTransform == null)//�Ѿư� ���� �ı��� ���
        {
            Destroy(gameObject);//�ش� UI ����
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);//���� ��ǥ�� �������� ȭ���Ǽ��� ��ǥ ����
        rectTransform.position = screenPosition+distance;//ȭ�鳻 ��ǥ+distance = UI ��ġ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
