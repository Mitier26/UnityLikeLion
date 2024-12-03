using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    #region 링크드리스트 구현

        // <T>
        // 제네릭
        // 모든 타입과 연동되는 데이터 타입
        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        #region <T> 제네릭 이란

            public class NodeInt
            {
                public int Data { get; set; }
                public NodeInt Next { get; set; }

                public NodeInt(int data)
                {
                    Data = data;
                    Next = null;
                }
            }
            
            public class NodeFloat
            {
                public float Data { get; set; }
                public NodeFloat Next { get; set; }

                public NodeFloat(float data)
                {
                    Data = data;
                    Next = null;
                }
            }
            
            // 이렇게 타입별로 만들어야 하는 것을 <T> 를 이용해
            // 전부 가능하게 만드는 것!!
            // T1 ~ T8 
            // T 는 이름일 뿐 A 건 B건 상관없다
            // T : Template

        #endregion

        public class LinkedListCustom<T>
        {
            public Node<T> Head { get; private set; }

            public void AddLast(T data)
            {
                Node<T> newNode = new Node<T>(data);
                if (Head == null)
                {
                    Head = newNode;
                }
                else
                {
                    Node<T> current = Head;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = newNode;
                }
            }

            public void AddFirst(T data)
            {
                Node<T> newNode = new Node<T>(data);
                if (Head == null)
                {
                    Head = newNode;
                }
                else
                {
                    Node<T> next = Head;
                    Head = newNode;

                    Head.Next = next;
                }
            }
            
            public void Traverse()
            {
                Node<T> current = Head;
                while (current != null)
                {
                    Debug.Log(current.Data);
                    current = current.Next;
                }
            }
        }
    #endregion

    #region 더블 링크드리스트 구현

        public class NodeDouble<T>
        {
            public T Data { get; set; }
            public NodeDouble<T> Next { get; set; }
            public NodeDouble<T> Prev { get; set; }

            public NodeDouble(T data)
            {
                Prev = null;
                Data = data;
                Next = null;
            }
        }

        public class DoubleLinkedListCustom<T>
        {
            public NodeDouble<T> Head { get; private set; }     // 맨 앞 부분을 가리키는 오브젝트
            public NodeDouble<T> Tail { get; private set; }     // 맨 마지막 부분을 가리키는 오브젝트

            public void AddLast(T data)
            {
                // 노드를 생성한다.
                NodeDouble<T> node = new NodeDouble<T>(data);
                
                // Head가 없다면
                if (Head == null)
                {
                    Head = node;
                    // Head에 새로 만든 node를 넣는다.
                    Tail = node;
                    // Head가 없다면 끝도 없다는 것이다.
                    // 데이터가 아무것도 없다면 Head와 Tail는 하나!! 같은것
                }
                else
                {
                    node.Prev = Tail;
                    Tail.Next = node;
                    Tail = node;
                }
            }
            
            public void AddFirst(T data)
            {
                NodeDouble<T> node = new NodeDouble<T>(data);
                if (Head == null)
                {
                    Head = node;
                    Tail = node;
                }
                else if (Head == Tail)
                {
                    Head = node;
                    Head.Next = Tail;
                    Tail.Prev = node;
                }
                else
                {
                    node.Prev = node;
                    node.Next = Head;
                    Head = node;
                }
            }

            public void Insert(int index, T data)
            {
                NodeDouble<T> current = Head;
                NodeDouble<T> newNode = new NodeDouble<T>(data);
                
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                current.Prev.Next = newNode;
                newNode.Prev = current.Prev;
                current.Prev = newNode;
                newNode.Next = current;
            }

            
            public void Traverse()
            {
                NodeDouble<T> current = Head;
                while (current != null)
                {
                    Debug.Log(current.Data);
                    current = current.Next;
                }
            }
        }

    #endregion
public class ListExample : MonoBehaviour
{
    private void Start()
    {
        // NormalLinkedList();
        // CustomLinkedList();
        DoubleLinkedList();
    }

    private void NormalLinkedList()
    {
        // 링크드 리스트
        LinkedList<int> list = new LinkedList<int>();
        // 선언부                  할당부
        // int Type의 LinkedList를 만들었다.

        // AddLast : 리스트의 tail(꼬리, 마지막)에 1을 추가 한다.
        list.AddLast(1);

        // AddFirst : 리스트의 ( head, 앞 ) 에 2를 추가 한다.
        list.AddFirst(2);
        
        // [] : 요소
        // 배열과 리스트의 메모리적 차이
        // 배열의 메모리
        // [][][][][][][][][][][][] + [][][][]  뒤에 것을 추가하려면
        // [][][][][][][][][][][][][][][]][]    뒤에 붙이는 것이 아니고
        // 앞에 미리 있는 배열을 삭제하고 다시 만든다.
        
        // 링크드 리스트의 메모리
        // * : 연속적이지 않다. 어딘가 메모리가 존재 한다.
        // 해당 변수의 주소를 담고 있다. 주소!
        // 싱글 링크드 리스트
        // []* -> []* -> []*
        // 앞의 공간이 뒤의 공간의 주소를 알고 있다 ( 메모리 주소 )
        // 더블 링크드 리스트
        // *[]* -> *[]* -> *[]*
        // 앞,뒤 공간의 주소를 알고 있다.
        
        // 각 요소들이 서로의 주소값을 가지고 연결되어 있다.
        
        // [] 요소를 앞과뒤에 추가하는 것이 빠르다
        // 앞과 뒤는 앞 뒤의 주소가 없다. 주소가 없는 것을 찾으면 된다.
        // 시간 복잡도 ( O(1) )
        
        // 배열은 공간이 할당되어 있어 몇번에 있는 데이터를 찾는 것이 빠르다
        // 링크드리스트는 서로의 주소를 가지고 있기 때문에 
        // 요소를 찾기 위해서는 주소를 타고 타고 들어가 찾아야 한다.
        // 랜덤 엑세스가 불가!!
        // 요소에 순차적으로 접근하는 것에는 좋다

        list.AddLast(3);
        list.AddLast(4);
        list.AddLast(5);
        list.AddLast(6);

        list.AddFirst(0);

        var enumerator = list.GetEnumerator();

        int findIndex = 3;
        int currentIndex = 0;

        while (enumerator.MoveNext())
        {
            if (currentIndex == findIndex)
            {
                Debug.Log("찾았다");
                Debug.Log(enumerator.Current);
                break;
            }

            currentIndex++;
        }
    }

    private void CustomLinkedList()
    {
        LinkedListCustom<int> list = new LinkedListCustom<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(4);
        
        list.AddFirst(0);
        
        list.Traverse();
    }

    private void DoubleLinkedList()
    {
        DoubleLinkedListCustom<int> list = new DoubleLinkedListCustom<int>();
        
        list.AddLast(111);
        list.AddLast(222);
        list.AddLast(333);
        list.AddLast(444);
        list.Insert(2, 666);
        list.Traverse();
        
    }
}
