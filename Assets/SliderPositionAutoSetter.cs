using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 20.0f;//UI가 표시될 위치
    private Transform targetTransform;//HP를 표시할 적
    private RectTransform rectTransform;//UI의 위치 지정

    // Start is called before the first frame update
    
    public void Setup(Transform target)
    {
        targetTransform = target;//타겟 지정
        rectTransform = GetComponent<RectTransform>();//컴포넌트 정보 가져오기
    }
    void LateUpdate()//위치 갱신 이후 UI 이동을 위해 LateUpdate 사용
    {
        if (targetTransform == null)//쫓아갈 적이 파괴될 경우
        {
            Destroy(gameObject);//해당 UI 삭제
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);//월드 좌표를 기준으로 화면의서의 좌표 추정
        rectTransform.position = screenPosition+distance;//화면내 좌표+distance = UI 위치
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
