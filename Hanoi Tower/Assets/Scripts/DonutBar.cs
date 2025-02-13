using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DonutBar : MonoBehaviour
{
    public enum BarType { LEFT = -3, CENTER = 0, RIGHT = 3 }
    public BarType e_BarType;
    
    public Stack<GameObject> stack = new Stack<GameObject>();
    
    public GameManager gameManager;

    void OnMouseDown()
    {
        if (!GameManager.isSelected)
        {
            gameManager.selectedDonut = PopDonut();
            GameManager.isSelected = true;
        }
        else if (GameManager.isSelected)
        {
            PushDonut(gameManager.selectedDonut, true);
            
        }
    }
    
    public void PushDonut(GameObject donut, bool isInit)
    {
        if (!isInit)
        {
            GameObject peekDonut = stack.Peek();
            int peekNumber = peekDonut.GetComponent<Donut>().donutNumber;
            int pushNumber = donut.GetComponent<Donut>().donutNumber;
            
            bool result = pushNumber < peekNumber ? true : false;

            if (result)
            {
                GameManager.isSelected = false;
                stack.Push(donut);
        
                donut.transform.position  = new Vector3((int)e_BarType, 3.5f, 0f);

                switch (e_BarType)
                {
                    case BarType.LEFT:
                        gameManager.leftBar = stack.ToList();
                        break;
                    case BarType.CENTER:
                        gameManager.centerBar = stack.ToList();
                        break;
                    case BarType.RIGHT:
                        gameManager.rightBar = stack.ToList();
                        break;
                }
            }
            else
            {
                Debug.Log($"놓으려는 도넛은 {pushNumber}이고, 해당 기둥의 도넛은 {peekNumber}입니다.");
            }
        }
        else
        {
            stack.Push(donut);
        
            donut.transform.position  = new Vector3((int)e_BarType, 3.5f, 0f);

            switch (e_BarType)
            {
                case BarType.LEFT:
                    gameManager.leftBar = stack.ToList();
                    break;
                case BarType.CENTER:
                    gameManager.centerBar = stack.ToList();
                    break;
                case BarType.RIGHT:
                    gameManager.rightBar = stack.ToList();
                    break;
            }
        }
        
        

        
        
    }

    public GameObject PopDonut()
    {
        if (stack.Count > 0)
        {
            GameObject obj = stack.Pop();
            return obj;

        }

        return null;

    }

}
