using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
    private Dictionary<InCountArea, List<Monster>> monsterInCountArea = new();

    void Start()
    {
        // 1번방식
        EventManager.Instance.Subscribe((MessageTypeNotifyInCountArea areaMsg) =>
        {
            Entity entityj = areaMsg.other.GetComponent<Entity>();
            if (entityj is Monster j1)
            {
                if (!monsterInCountArea.ContainsKey(areaMsg.InCountArea))
                {
                    monsterInCountArea[areaMsg.InCountArea] = new List<Monster>();
                }
                
                monsterInCountArea[areaMsg.InCountArea].Add(j1);
            }
            else if (entityj is Player j)
            {
                if (monsterInCountArea.TryGetValue(areaMsg.InCountArea, value: out var value))
                {
                    foreach (var monsterJ in value)
                    {
                        monsterJ.OnDetectPlayer(j);
                    }
                }
            }
        });
    }

    public void AddMonster(Monster monster)
    {
        monsters.Add(monster.GetInstanceID(), monster);
    }

    public void RemoveMonster(Monster monster)
    {
        monsters.Remove(monster.GetInstanceID());
    }
    
}
