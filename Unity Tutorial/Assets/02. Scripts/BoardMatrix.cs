using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMatrix : MonoBehaviour
{
    public GameObject tilePerfab;
    
    public Vector2 boardSize = new Vector2(5, 5);
    
    public int[,] tileArray;
    
    public GameObject turretPrefab;
    public GameObject[] turretArray;

    private void Start()
    {
        tileArray = new int[(int)boardSize.x, (int)boardSize.y];
        
        for(int i = 0; i < boardSize.x; i++)
        {
            for(int j = 0; j < boardSize.y; j++)
            {
                GameObject tileObj = Instantiate(tilePerfab);
                tileObj.transform.position = new Vector3(i, 0, j);
                
                // tileArray[i, j] = 1;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int x = Mathf.RoundToInt(hit.collider.transform.position.x);
                int z = Mathf.RoundToInt(hit.collider.transform.position.z);

                if (tileArray[x, z] == 0)
                {
                    GameObject turretObj = Instantiate(turretPrefab);
                    turretObj.transform.position = new Vector3(x, 0, z);
                    
                    tileArray[x, z] = 1;
                }
            }
        }
    }

    public void OnChangeTurret(int index)
    {
        turretPrefab = turretArray[index];
    }
}
