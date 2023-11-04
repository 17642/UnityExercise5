using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPAlpha : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.5f;//글자 사라지는 시간 지정
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
       text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fadeout()
    {
        StartCoroutine(AlphaLerp(1, 0));
    }

    private IEnumerator AlphaLerp(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;//시간 계속 측정
            percent = currentTime / lerpTime;

            Color color = text.color;
            color.a = Mathf.Lerp(start, end, percent);//폰트 투명도를 Start에서 End로 변경
            text.color = color;

            yield return null;
        }
    }
}
