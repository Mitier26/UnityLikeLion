using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public TMP_Text stageText;
    public TMP_Text countText;
    
    private void Awake()
    {
        stageText.text = "/ "+totalItemCount;
    }

    public void GetItem(int count)
    {
        countText.text = count.ToString();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(stage);
        }
    }
}
