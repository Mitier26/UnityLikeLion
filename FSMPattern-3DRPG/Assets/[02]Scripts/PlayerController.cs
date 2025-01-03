using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Dictionary<string, GameObject> players = new ();
    
    private Dictionary<GameObject, IRecevieInput> inputs = new();
    
    private GameObject currentPlayer;

    // 시작과 동시에 코루틴을 돌릴 수 있다.
    private IEnumerator Start()
    {
        // 로딩 시간 같은 것, 처음에는 1번
        yield return new WaitForSeconds(3f);
        ChangePlayer("Number_1");
    }

    public void AddObserver(string key, GameObject obj)
    {
        players.Add(key, obj);
    }

    public void AddInputObserver(GameObject obj, IRecevieInput input)
    {
        inputs[obj] = input;
    }
    

    private bool ChangePlayer(string key)
    {
        if (players.TryGetValue(key, out var player))
        {
            currentPlayer = player;
            return true;
        }
        
        return false;
    }
    
    public void OnTriggered(string action, bool triggerValue)
    {
        if (triggerValue && ChangePlayer(action))
            return;
        
        if (!currentPlayer)
            return;

        if (ContainsInput())
        {
            inputs[currentPlayer].OnTriggered(action, triggerValue);
        }
    }
    public void OnReadValue(string action, Vector2 value)
    {
        if (!currentPlayer)
            return;
        
        if (ContainsInput())
        {
            inputs[currentPlayer].OnReadValue(action, value);
        }
    }

    private bool ContainsInput()
    {
        return inputs.ContainsKey(currentPlayer) && inputs[currentPlayer] != null;
    }
}
