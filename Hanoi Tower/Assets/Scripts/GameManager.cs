using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum HanoiLevel { LV1 = 3, LV2, LV3}
    public HanoiLevel e_HanoiLevel;
    
    public GameObject donutPerfab;

    public DonutBar[] donutBars;
    
    public static bool isSelected = false;
    
    public List<GameObject> leftBar = new List<GameObject>();
    public List<GameObject> centerBar = new List<GameObject>();
    public List<GameObject> rightBar = new List<GameObject>();
    
    public GameObject selectedDonut;
    
    IEnumerator Start()
    {
        for (int i = (int)e_HanoiLevel; i >= 1; i--)
        {
            Vector3 createPos = new Vector3((int)DonutBar.BarType.LEFT, 3.5f, 0f);
            GameObject donutObj = Instantiate(donutPerfab, createPos, Quaternion.identity);
            donutObj.name = "Donut_" + i;
            
            donutObj.GetComponent<Donut>().donutNumber = i;
            
            donutObj.transform.localScale = Vector3.one * 5 * (i * 0.2f + 1f);
            
            donutBars[0].PushDonut(donutObj,false);

            yield return new WaitForSeconds(1f);
        }
    }

}
