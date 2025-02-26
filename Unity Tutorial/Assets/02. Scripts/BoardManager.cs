using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public GameObject tilePrefab;
    
    public Vector2Int  boardSize = new Vector2Int(5, 5);

    public int[,] tileArray;

    public GameObject[] turrets;
    public GameObject currentTurret;
    public Button[] selectButtons;
    
    public GameObject[] prevTurrets;
    
    private int turretIndex;
    private bool isSelected;

    private void Start()
    {
       for(int i = 0; i < selectButtons.Length; i++)
       {
           int index = i;
           selectButtons[i].onClick.AddListener(() => OnSelectTurret(index));
       }
    }

    public void CreateBorad()
    {
        tileArray = new int[boardSize.x, boardSize.y];

        for (int x = 0; x < boardSize.x; x++)
        {
            for(int z = 0; z < boardSize.y; z++)
            {
                GameObject tile = Instantiate(tilePrefab, this.transform);
                tile.transform.position = new Vector3(x, 0, z);
            }
        }
    }
    
    public void RayToBoard()
    {
        if (!isSelected) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int x = Mathf.RoundToInt(hit.collider.transform.position.x);
                int z = Mathf.RoundToInt(hit.collider.transform.position.z);

                if (tileArray[x, z] == 0)
                {
                    GameObject turretObj = Instantiate(turrets[0]);
                    turretObj.transform.position = new Vector3(x, 0, z);

                    if (Input.GetMouseButtonDown(0))
                    {
                        tileArray[x, z] = 1;
                        currentTurret = turrets[turretIndex];
                    }
                }
            }
        }
    }

    private void CreateTurret(GameObject turretPrefb, int x, int y)
    {
        isSelected = false;
        Destroy(currentTurret);
        
        if(tileArray[x, y] == 0)
        {
            GameObject turretObj = Instantiate(turretPrefb, this.transform);
            turretObj.transform.position = new Vector3(x, 0, y);
            tileArray[x, y] = 1;
        }
    }

    private void OnSelectTurret(int index)
    {
        isSelected = true;
        turretIndex = index;
        currentTurret = prevTurrets[index];
    }

    public void OnChangeTurret(int index)
    {
        currentTurret = turrets[index];
    }
}
