using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private void Start()
    {
        
        
        AddVertex("집");
        AddVertex("슈퍼마켓");
        AddVertex("미용실");
        AddVertex("레스토랑");
        AddVertex("은행");
        AddVertex("영어학원");
        AddVertex("학교");
        
        AddEdge("집","미용실", 5.0f);
        AddEdge("집","슈퍼마켓", 10.0f);
        AddEdge("집","영어학원", 9.0f);
        
        AddEdge("미용실","슈퍼마켓", 3.0f);
        AddEdge("미용실","은행", 11.0f);
        AddEdge("슈퍼마켓","레스토랑", 3.0f);
        AddEdge("슈퍼마켓","은행", 10.0f);
        AddEdge("슈퍼마켓","영어학원", 7.0f);
        AddEdge("레스토랑","은행", 4.0f);
        
        AddEdge("은행","학교", 2.0f);
        AddEdge("은행","영어학원", 7.0f);
        
        AddEdge("영어학원","학교", 12.0f);

        FindShortestPath("집", "학교");
    }

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
        }
    }
    
    public void AddEdge(string from, string to, float weight)
    {
        if (vertices.ContainsKey(from) && vertices.ContainsKey(to))
        {
            Vertex fromV = vertices[from];
            Vertex toV = vertices[to];

            if (!fromV.neighbors.ContainsKey(toV))
            {
                fromV.neighbors.Add(toV, weight);
            }
        }
    }

    public void BFS(string startName)
    {
        if (!vertices.ContainsKey(startName))
            return;
        
        HashSet<Vertex> visited = new HashSet<Vertex>();
        Queue<Vertex> queue = new Queue<Vertex>();
        
        Vertex startVertex = vertices[startName];
        queue.Enqueue(startVertex);
        visited.Add(startVertex);

        while (queue.Count > 0)
        {
            Vertex currentVertex = queue.Dequeue();
            Debug.Log($"방문 {currentVertex.name}");

            foreach (var neightbor in currentVertex.neighbors.Keys)
            {
                if (!visited.Contains(neightbor))
                {
                    visited.Add(neightbor);
                    queue.Enqueue(neightbor);
                }
            }
        }
    }

    public void DFS(string startName)
    {
        if (!vertices.ContainsKey(startName))
            return;
        
        HashSet<Vertex> visited = new HashSet<Vertex>();
        DFSUtil(vertices[startName], visited);
    }

    public void DFSUtil(Vertex vertex, HashSet<Vertex> visited)
    {
        visited.Add(vertex);
        Debug.Log($"방문 {vertex.name}");

        foreach (var neighbor in vertex.neighbors.Keys)
        {
            if (!visited.Contains(neighbor))
            {
                DFSUtil(neighbor, visited);
            }
        }
    }
    
    public void FindShortestPath(string startName, string endName)
    {
        if (!vertices.ContainsKey(startName) || !vertices.ContainsKey(endName))
        {
            Debug.LogError("시작점 또는 도착점이 그래프에 존재하지 않습니다.");
            return;
        }

        Vertex startVertex = vertices[startName];
        Vertex endVertex = vertices[endName];

        // 각 정점까지의 최단 거리를 저장
        Dictionary<Vertex, float> distances = new Dictionary<Vertex, float>();
        // 각 정점의 이전 정점을 저장 (경로 추적용)
        Dictionary<Vertex, Vertex> previousVertices = new Dictionary<Vertex, Vertex>();
        // 방문하지 않은 정점들을 저장할 우선순위 큐
        SortedDictionary<float, List<Vertex>> priorityQueue = new SortedDictionary<float, List<Vertex>>();

        // 모든 정점의 거리를 무한대로 초기화
        foreach (var vertex in vertices.Values)
        {
            distances[vertex] = float.MaxValue;
            previousVertices[vertex] = null;
        }

        // 시작점의 거리는 0
        distances[startVertex] = 0;
        
        // 우선순위 큐에 시작점 추가
        priorityQueue[0] = new List<Vertex> { startVertex };

        while (priorityQueue.Count > 0)
        {
            // 가장 짧은 거리를 가진 정점 선택
            var shortestDistance = priorityQueue.Keys.Min();
            var currentVertex = priorityQueue[shortestDistance][0];
            priorityQueue[shortestDistance].RemoveAt(0);
            if (priorityQueue[shortestDistance].Count == 0)
            {
                priorityQueue.Remove(shortestDistance);
            }

            // 목적지에 도달했다면 종료
            if (currentVertex == endVertex)
                break;

            // 이웃 정점들을 검사
            foreach (var neighbor in currentVertex.neighbors)
            {
                float newDistance = distances[currentVertex] + neighbor.Value;

                // 더 짧은 경로를 발견했다면 업데이트
                if (newDistance < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = newDistance;
                    previousVertices[neighbor.Key] = currentVertex;

                    // 우선순위 큐에 추가
                    if (!priorityQueue.ContainsKey(newDistance))
                    {
                        priorityQueue[newDistance] = new List<Vertex>();
                    }
                    priorityQueue[newDistance].Add(neighbor.Key);
                }
            }
        }

        // 경로 출력
        if (distances[endVertex] == float.MaxValue)
        {
            Debug.Log($"{startName}에서 {endName}까지 경로가 존재하지 않습니다.");
            return;
        }

        // 경로 역추적
        List<string> path = new List<string>();
        Vertex current = endVertex;
        while (current != null)
        {
            path.Add(current.name);
            current = previousVertices[current];
        }
        path.Reverse();

        // 결과 출력
        Debug.Log($"최단 거리: {distances[endVertex]}");
        Debug.Log($"경로: {string.Join(" -> ", path)}");
    }

}
