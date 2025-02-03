using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Animal
{
    public string name;

    public void Eat()
    {
        Debug.Log(name + " is eating");
    }
}

class Dog : Animal
{
    public void Bark()
    {
        Debug.Log(name + " is barking");
    }
}

class A
{
    public A()
    {
        B b = new B(Run);
        b.Run();
    }

    public void Run()
    {
        Debug.Log("A Run!!");
    }
}

class B
{

    public delegate void RunDelegate();

    public RunDelegate AfterRun;

    public B(RunDelegate afterRun)
    {
        AfterRun = afterRun;
    }
    
    public void Run()
    {
        Debug.Log("Run!!");
        AfterRun.Invoke();
    }
}

public class Test : MonoBehaviour
{
    private void Start()
    {
        // RecursiveFunction(5);
        Dog dog = new Dog();
        dog.Eat();
        dog.Bark();
    }

    void RecursiveFunction(int count)
    {
        if(count <= 0 )
        {
            Debug.Log("종료");
            return;
        }

        Debug.Log("Count : " + count);
        RecursiveFunction(count - 1);
    }
}
