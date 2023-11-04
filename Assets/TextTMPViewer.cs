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
    [SerializeField]
    private TextMeshProUGUI textWave;//TMP UI(���̺�)
    [SerializeField]
    private WaveSystem waveSystem;//���̺� ����
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;//TMP UI(���� ��)
    [SerializeField]
    private EnemySpawner enemySpawner;//�� ����


    // Update is called once per frame
    void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;//���� ü��/�ִ� ü�� ���
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = waveSystem.CurrentWave+"/" + waveSystem.MaxWave;//���� ���̺�/������ ���̺�
        textEnemyCount.text=enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;//���� ���� ��/���̺� �ִ� ��
    }
}
