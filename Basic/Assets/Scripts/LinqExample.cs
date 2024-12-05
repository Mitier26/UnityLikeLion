using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct MonsterTest : IComparable<MonsterTest>
{
    public string name;
    public int health;
    
    public int CompareTo(MonsterTest other)
    {
        return this.health.CompareTo(other.health);
    }
}

public class LinqExample : MonoBehaviour
{
    // LINQ
    // 쿼리를 이용해 데이터를 가공하는 방법
    // 쿼리 : 서버에서 데이터를 다루는 방법, 조건들을 간결하게 사용하는 방법

    public List<MonsterTest> monsters = new List<MonsterTest>()
    {
        new MonsterTest() { name = "A", health = 100 },
        new MonsterTest() { name = "A", health = 30 },
        new MonsterTest() { name = "B", health = 20 },
        new MonsterTest() {name = "B", health = 150},
        new MonsterTest() {name = "C", health = 200},
        new MonsterTest() {name = "C", health = 70},
        
      
    };

    private List<MonsterTest> selectedMonster = new List<MonsterTest>();
    private List<MonsterTest> lastMonster = new List<MonsterTest>();
    private void Start()
    {
        // 몬스터 퀘스트 그룹에서 A 네임을 가진 HP 50 이상인 오브젝트들을 리스트화 체력순 출력
       // MySort();
       MySort2();
       // SamSort();
    }

    void MySort()
    {
        foreach (MonsterTest monster in monsters)
        {
            if (monster.name == "A" && monster.health >= 30)
            {
                selectedMonster.Add(monster);
            }
        }
        
        selectedMonster.Sort();

        foreach (MonsterTest mo in selectedMonster)
        {
            Debug.Log($"몬스터 이름 : {mo.name}, 몬스터 체력 : {mo.health} 입니다.");
        }
    }

    void MySort2()
    {
        foreach (MonsterTest monster in monsters)
        {
            if (monster.name == "A" && monster.health >= 30)
            {
                selectedMonster.Add(monster);
            }
        }
        
        // 정렬
        for (int i = 0; i < selectedMonster.Count; i++)
        {
            for (int j = 0; j < selectedMonster.Count; j++)
            {
                if (selectedMonster[i].health > selectedMonster[j].health)
                {
                    MonsterTest temp = selectedMonster[i];
                    selectedMonster[i] = selectedMonster[j];
                    selectedMonster[j] = temp;
                }
            }
        }
        
        foreach (MonsterTest mo in selectedMonster)
        {
            Debug.Log($"몬스터 이름 : {mo.name}, 몬스터 체력 : {mo.health} 입니다.");
        }
    }

    void SamSort()
    {
        
        List<MonsterTest> filters = new List<MonsterTest>();
        for (var i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].name == "A" && monsters[i].health >= 30)
            {
                filters.Add(monsters[i]);
            }
        }
        
        filters.Sort((l,r)=>l.health >= r.health ? -1 : 1);

        for (var i = 0; i < filters.Count; i++)
        {
            Debug.Log($"몬스터 이름 : {filters[i].name}, 몬스터 체력 : {filters[i].health} 입니다.");
        }
    }

    void LinqSort()
    {
        
        var linqFilter = monsters.Select(e => e)
            .Where(e => e.name == "A" && e.health >= 30)
            .OrderByDescending(e=>e.health)
            .ToList();

        var linqFilter2 = (
            from e in monsters
            where e is {health: >= 30 , name :"A"}
            orderby e.health descending 
            select new {e.name, e.health}).
            ToList();
        
        // Linq 문법 : 외우지 말고 이런 것이 있다고 알자. 자주 사용하면 기억함
        // Select : 특정한 것을 선택, 사실 위의 코드에는 필요 없다.
        // Where : 조건문 
        // OrderByDescending : 내림차순 정렬
        // ToList : List로 변경
        
        // Linq : 데이터를 검색하는 기능해서 사용한다. 
        // 거의 사용되지 않는다. 하지만 중요한 것에 사용!
        
        foreach (MonsterTest mo in linqFilter)
        {
            Debug.Log($"몬스터 이름 : {mo.name}, 몬스터 체력 : {mo.health} 입니다.");
        }
    }
}
