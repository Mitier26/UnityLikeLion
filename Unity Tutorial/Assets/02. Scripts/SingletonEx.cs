using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person2
{
    public string name;
    public int age;

    public Person2(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

public class SingletonEx : MonoBehaviour
{
    private int number;

    public int Number
    {
        get { return number; }
        set {number = value; }
    }
    
    private static SingletonEx instance;
    public static SingletonEx Instance
    {
        get
        {
            if (instance == null)
            {
                SingletonEx obj = FindObjectOfType<SingletonEx>();
                
                if (obj == null)
                {
                    obj = new GameObject("SingletonEx").AddComponent<SingletonEx>();
                    
                    instance = obj;
                }
                else
                {
                    instance = obj.GetComponent<SingletonEx>();
                }
            }
            return instance;
        }
    }

    private EffectSystem effectSystem;
    private void Awake()
    {
        instance = this;

        // 퍼사드 패턴
        // 퍼사드 패턴은 복잡한 시스템을 간단하게 제공하는 패턴
        effectSystem = new EffectSystem();
    }

    void Start()
    {
        Person2 person2 = new Person2("asd", 10);

        effectSystem.OnEffect();
        effectSystem.AddEffect();
    }
}

public class EffectSystem
{
    public void OnEffect()
    {
        Debug.Log("Effect On");
    }
    
    public void AddEffect()
    {
        Debug.Log("Effect Add");
    }
}