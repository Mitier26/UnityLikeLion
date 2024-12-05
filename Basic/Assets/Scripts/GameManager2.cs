using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData2
{
    public string playerName;
    [NonSerialized] public float Distance;
    [NonSerialized] public int Rank;
    [NonSerialized] public RaceButton RaceButton2;
}
public class GameManager2 : MonoBehaviour
{
    private float stepBattleDuration = 1.0f;

    public List<PlayerData2> Players2 = new List<PlayerData2>();

    public RaceButton buttonPrefab;

    public Transform buttonParent;
    
    private void Start()
    {
        for (int i = 0; i < Players2.Count; i++)
        {
            var newObj = Instantiate(buttonPrefab.gameObject, buttonParent);
            RaceButton raceButton = newObj.GetComponent<RaceButton>();

            raceButton.text.text = Players2[i].playerName;
            Players2[i].RaceButton2 = raceButton;
            Players2[i].Rank = i;
        }
    }

    private void Update()
    {
        

        stepBattleDuration += Time.deltaTime;
    }
}
