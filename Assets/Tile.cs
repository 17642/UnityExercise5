using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsbuildTower { set; get; }//Ÿ�� ���� ���� Ȯ��/�ڵ����� ������Ƽ ����
    // Start is called before the first frame update
    void Awake()
    {
        IsbuildTower = false;
    }

    
   
}
