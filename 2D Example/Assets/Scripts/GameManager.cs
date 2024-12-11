using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMove player;
    
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;

    public GameObject[] stages;

    public void NextStage()
    {
        if (stageIndex < stages.Length - 1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            totalPoint += stagePoint;
            stagePoint = 0;
        }
        else
        {
            Time.timeScale = 0;
        }

    }

    public void HealthDown()
    {
        if (health > 1) health--;
        else
        {
            player.OnDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (health > 1)
            {
               PlayerReposition();
            }
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
}
