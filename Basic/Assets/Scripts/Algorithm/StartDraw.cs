using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDraw : MonoBehaviour
{
    public GameObject cube;
    private string star;
    private void Start()
    {
        // Level1();
        // Level2();
        // Level3();
        // Level4();
        // Level5();
        Level6();
    }

    private void Level1()
    {
        Debug.Log("*****");
        Debug.Log(" ***");
        Debug.Log("  *");
    }
    private void Level2()
    {
        star = "";
        for (int i = 0; i < 5; i++)
        {
            star += "*";
        }
        Debug.Log(star);
        star = "";
        for (int i = 0; i < 4; i++)
        {
            if (i >= 1) star += "*";
            else star += "  ";
        }
        Debug.Log(star);
        star = "";
        for (int i = 0; i < 3; i++)
        {
            if (i >= 2) star += "*";
            else star += "  ";
        }
        Debug.Log(star);
    }
    private void Level3()
    {
        for (int i = 0; i < 3; i++)
        {
            star = "";
            for (int j = 0; j < 5 - i; j++)
            {
                if (j >= i) star += "*";
                else star += "  ";
            }
            Debug.Log(star);
        }
    }
    private void Level4()
    {
        star = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5 - i; j++)
            {
                star += j >= i ? "*" : "  ";
            }
            Debug.Log(star);
        }
    }
    private void Level5()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5 - i; j++)
            {
                if (j >= i)
                {
                    Instantiate(cube, new Vector3(j, -i, 0), Quaternion.identity);
                }
            }
        }
    }

    private int addtive = 0;
    
    private void Level6()
    {
        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5 + addtive; j++)
            {
                Instantiate(cube, new Vector3(j, -i, 0), Quaternion.identity);
            }
            
            if (i >= 2)
            {
                addtive += 2;
            }
            else
            {
                addtive -= 2;
            }
        }
    }
}
