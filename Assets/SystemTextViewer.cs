using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SystemType { Money = 0,Build}//텍스트 불러오는 유형 

public class SystemTextViewer : MonoBehaviour
{

    private TextMeshProUGUI textSystem;
    private TMPAlpha tmpAlpha;
    // Start is called before the first frame update
    void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tmpAlpha = GetComponent<TMPAlpha>();//컴포넌트 가져오기 
    }

    public void PrintText(SystemType type)
    {
        switch (type)
        {
            case SystemType.Money:
                textSystem.text = "SYSTEM: Not Enough Money";
                break;
            case SystemType.Build:
                textSystem.text = "SYSTEM: Invaild Build Tower";
                break;
        }

        tmpAlpha.Fadeout();//글자 점점 흐리게
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
