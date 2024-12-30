using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// abstract로 만들어 자식이 구현하게 한다.
// MonoBehaviour를 가진 것만 싱글톤을 사용할 수 있다.
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // static 변수는 단 하나!!
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T; 
            }

            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.AddComponent<T>();
            }
            
            return instance;
        }
    }

    // 실행이 되면 자동으로 실행되게 하는 것이다.
    // 게임이 시작되면 처음으로 불리우는 함수
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void realSington()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        
        OnAwake();
    }

    public virtual void OnAwake()
    {
        
    }
}
