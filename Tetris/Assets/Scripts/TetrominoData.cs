using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TetrominoData : MonoBehaviour
{   
    private List<Transform> blocks = new List<Transform>();
    public List<Transform> Blocks { get => blocks; }
    
    
    void Start()
    {
        // 주의
        // transform.GetComponentsInChildren<Transform>()
        // 이것으로 할 경우 [자신] 포함한다. <--- 에러의 원인
        for (int i = 0; i < transform.childCount; ++i)
        {
            // 자식 오브젝트에 Block 이라는 스크립트를 붙인다.
            // 테트리스의 한 줄 확인용
            transform.GetChild(i).gameObject.AddComponent<Block>();
            blocks.Add(transform.GetChild(i));  
        }
    }
}