using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsbuildTower { set; get; }//타워 존재 여부 확인/자동으로 프로퍼티 구현
    // Start is called before the first frame update
    void Awake()
    {
        IsbuildTower = false;
    }

    
   
}
