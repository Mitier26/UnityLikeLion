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
        if (!gameManager.isSelected)
        {
            gameManager.selectedDonut = PopDonut();
            gameManager.isSelected = true;
        }
        else if (gameManager.isSelected)
        {
            PushDonut(gameManager.selectedDonut);
            
        }
    }
    private bool CheckDonutNumber(GameObject pushDouut)
    {
        bool result = true;

        if (stack.Count > 0)
        {
            int pushNumber = pushDouut.GetComponent<Donut>().donutNumber;
            int peekNumber = stack.Peek().GetComponent<Donut>().donutNumber;

            result = pushNumber < peekNumber;
            
            if(!result)
                Debug.Log($"놓으려는 도넛은 {pushNumber}이고, 해당 기둥의 도넛은 {peekNumber}입니다.");
        }

        return result;
    }

    public void PushDonut(GameObject pushDounut)
    {
        if (!CheckDonutNumber(pushDounut))
            return;

        gameManager.isSelected = false;
        gameManager.selectedDonut = null;

        stack.Push(pushDounut);
        pushDounut.transform.SetPositionAndRotation(new Vector3((int)e_BarType, 3.5f, 0f), Quaternion.identity);

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

    public GameObject PopDonut()
    {
        GameObject obj = null;
        
        if (stack.Count > 0)
        {
            obj = stack.Pop();
            gameManager.isSelected = true;
            
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
            return obj;
        }
        return null;
    }

}
