using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;//TMP UI(ü��)
    [SerializeField]
    private playerHP playerHP;//�÷��̾� ü�� ����
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // TMP UI(���)
    [SerializeField]
    private PlayerGold playerGold;//�÷��̾� ��� ����


    // Update is called once per frame
    void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;//���� ü��/�ִ� ü�� ���
        textPlayerGold.text = playerGold.CurrentGold.ToString();
    }
}
