using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public class Vertex
    {
        public string name;
        public Dictionary<Vertex, float> neighbors = new Dictionary<Vertex, float>();

        public Vertex(string name)
        {
            this.name = name;
        }
    }

    private Dictionary<string, Vertex> vertices = new Dictionary<string, Vertex>();

    public void AddVertex(string name)
    {
        if (!vertices.ContainsKey(name))
        {
            vertices.Add(name, new Vertex(name));
            Debug.Log($"정점 {name}이(가) 추가되었습니다.");
        }
    }

    public void AddEdge(string from, string to, float weight)
    {
        if (vertices.ContainsKey(from) && vertices.ContainsKey(to))
        {
            Vertex formV = vertices[from];
            Vertex toV = vertices[to];

            if (formV.neighbors.ContainsKey(toV))
            {
                formV.neighbors.Add(toV, weight);
                Debug.Log($"간선 {from} -> {to} (가중치: {weight})가 추가되었습니다.");
            }
        }
    }

    public void BFS(string startName)
    {
        if (!vertices.ContainsKey(startName)) return;

        HashSet<Vertex> visited = new HashSet<Vertex>();
        Queue<Vertex> queue = new Queue<Vertex>();

        Vertex startVertex = vertices[startName];
        queue.Enqueue(startVertex);
        visited.Add(startVertex);

        while (queue.Count > 0)
        {
            Vertex currentVertex = queue.Dequeue();
            Debug.Log($"방문: {currentVertex.name}");

            foreach (Vertex neighbor in currentVertex.neighbors.Keys)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    public void DFS(string startName)
    {
        if (!vertices.ContainsKey(startName)) return;

        HashSet<Vertex> visited = new HashSet<Vertex>();
        DFSUtil(vertices[startName], visited);
    }

    public void DFSUtil(Vertex vertex, HashSet<Vertex> visited)
    {
        visited.Add(vertex);
        Debug.Log($"방문: {vertex.name}");

        foreach (Vertex neighbor in vertex.neighbors.Keys)
        {
            if (!visited.Contains(neighbor))
            {
                DFSUtil(neighbor, visited);
            }
        }
    }

    private void Start()
    {
        AddVertex("1");
        AddVertex("2");
        AddVertex("3");
        AddVertex("4");
        AddVertex("5");
        AddVertex("6");
        AddVertex("7");
        AddVertex("8");
        AddVertex("9");
        
        AddEdge("1", "2", 4);
        AddEdge("2", "3", 2);
        AddEdge("3", "4", 1);
        AddEdge("4", "5", 5);
        
        AddEdge("1", "8", 5);
        AddEdge("8", "7", 5);
        AddEdge("8", "9", 5);
        
        Debug.Log("BFS");
        BFS("1");
        Debug.Log("DFS");
        DFS("1");
    }
}
