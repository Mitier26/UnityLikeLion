using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutComp : MonoBehaviour
{
    [Header("Life")] 
    [Tooltip("인생에 관한 변수 입니다.")]
    public string data1;
    public string data2;
    public string data3;
    
    [Header("Love")]
    public string data4;
    public string data5;
    public string data6;
    
    [Header("Power")]
    [Range(0.1f, 5f)]
    public float data7;
    public float data8;
    
}
