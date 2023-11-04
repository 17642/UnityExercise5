using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPAlpha : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.5f;//���� ������� �ð� ����
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
            currentTime += Time.deltaTime;//�ð� ��� ����
            percent = currentTime / lerpTime;

            Color color = text.color;
            color.a = Mathf.Lerp(start, end, percent);//��Ʈ ������ Start���� End�� ����
            text.color = color;

            yield return null;
        }
    }
}
