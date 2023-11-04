using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;//TMP UI(체력)
    [SerializeField]
    private playerHP playerHP;//플레이어 체력 정보
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // TMP UI(골드)
    [SerializeField]
    private PlayerGold playerGold;//플레이어 골드 정보
    [SerializeField]
    private TextMeshProUGUI textWave;//TMP UI(웨이브)
    [SerializeField]
    private WaveSystem waveSystem;//웨이브 정보
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;//TMP UI(적의 수)
    [SerializeField]
    private EnemySpawner enemySpawner;//적 정보


    // Update is called once per frame
    void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;//현재 체력/최대 체력 출력
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = waveSystem.CurrentWave+"/" + waveSystem.MaxWave;//현재 웨이브/마지막 웨이브
        textEnemyCount.text=enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;//현재 남은 적/웨이브 최대 적
    }
}
