using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleComp : MonoBehaviour
{
    private PriorityQueue<QueueEvent> eventQueue = new PriorityQueue<QueueEvent>();
    
    // Start is called before the first frame update
    void Start()
    {
// 이벤트 추가
        eventQueue.Enqueue(new QueueEvent("일반 몬스터 생성", 3));
        eventQueue.Enqueue(new QueueEvent("보스 몬스터 생성", 1));
        eventQueue.Enqueue(new QueueEvent("아이템 생성", priority: 2));

// 우선순위가 높은 순서대로 처리
        while (!eventQueue.IsEmpty)
        {
            QueueEvent nextEvent = eventQueue.Dequeue();
            Debug.Log(nextEvent.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
