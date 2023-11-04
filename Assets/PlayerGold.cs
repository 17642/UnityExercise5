using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    private int currentGold = 100;

    public int CurrentGold//
    {
        set => currentGold = Mathf.Max(0, value);//골드를 음수만큼 추가하는 것 방지
        get => currentGold;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
