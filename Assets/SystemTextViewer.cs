using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SystemType { Money = 0,Build}//�ؽ�Ʈ �ҷ����� ���� 

public class SystemTextViewer : MonoBehaviour
{

    private TextMeshProUGUI textSystem;
    private TMPAlpha tmpAlpha;
    // Start is called before the first frame update
    void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tmpAlpha = GetComponent<TMPAlpha>();//������Ʈ �������� 
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

        tmpAlpha.Fadeout();//���� ���� �帮��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
