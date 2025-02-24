using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Person
{
    public string name;
    public int score;
    
    public Person(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class ExampleLINQ : MonoBehaviour
{
    public int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public List<Person> persons = new List<Person>();
    
    private void Start()
    {
        foreach (var number in from n in numbers where n % 2 == 0 select n)
        {
            Debug.Log(number);
        }

        var result = from n in numbers where n % 2 == 0 select n;
        var result2 = from n in numbers
                                        where n % 2 == 0
                                        select n;
        var result3 = numbers.Where(n => n % 2 == 0);
        
        persons.Add(new Person("홍길동", 100));
        persons.Add(new Person("김철수", 50));
        persons.Add(new Person("이영희", 70));
        persons.Add(new Person("박영수", 90));
        persons.Add(new Person("최영희", 80));
        
        CheckScore(70);
    }

    private void CheckScore(int cutlineScore)
    {
        var passPerson = from person in persons
            where person.score >= cutlineScore
            select person;

        foreach (Person person in passPerson)
        {
            Debug.Log($"<color=green>{person.name}</color> 합격");
        }
        
        var failPerson = from person in persons
            where person.score < cutlineScore
            select person;

        foreach (var person in failPerson)
        {
            Debug.Log($"<color=red>{person.name}</color> 불합격");
        }
        
        var failPerson2 = persons.Except(passPerson);
    }

    
}
