using UnityEngine;
using System.Collections.Generic;

public class HexGridPathfinding : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
    [SerializeField] private float hexRadius = 0.5f;
    [SerializeField] private bool showGrid = true;
    [SerializeField] private Color gridColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    [SerializeField] private Color obstacleColor = new Color(1f, 0f, 0f, 0.5f);

    private HexNode[,] grid;
    private Vector3 gridOrigin;
    private const int MOVE_COST = 10;
    
    // 육각 그리드의 상수들
    private float hexWidth;
    private float hexHeight;
    private float hexVerticalSpacing;
    private float hexHorizontalSpacing;

    // 육각형의 6방향 이웃 좌표 (짝수/홀수 행에 따라 다름)  
    // private readonly (int x, int y)[] evenRowNeighbors = new (int x, int y)[]
    // {
    //     (-1, 0),  // 좌
    //     (-1, 1),  // 좌상
    //     (0, 1),   // 우상
    //     (1, 0),   // 우
    //     (0, -1),  // 우하
    //     (-1, -1)  // 좌하
    // };
    //
    // private readonly (int x, int y)[] oddRowNeighbors = new (int x, int y)[]
    // {
    //     (-1, 0),  // 좌
    //     (0, 1),   // 좌상
    //     (1, 1),   // 우상
    //     (1, 0),   // 우
    //     (1, -1),  // 우하
    //     (0, -1)   // 좌하
    // };
    
    // private readonly (int x, int y)[] evenRowNeighbors = new (int x, int y)[]
    // {
    //     (-1, 0),  // 좌
    //     (0, 1),   // 상
    //     (1, 0),   // 우
    //     (0, -1)   // 하
    // };
    //
    // private readonly (int x, int y)[] oddRowNeighbors = new (int x, int y)[]
    // {
    //     (-1, 0),  // 좌
    //     (0, 1),   // 상
    //     (1, 0),   // 우
    //     (0, -1)   // 하
    // };

    
    private readonly (int x, int y)[] evenRowNeighbors = new (int x, int y)[]
    {
        (-1, 0),  // 좌
        (0, 1),   // 상
        (1, 0),   // 우
        (0, -1)   // 하
    };

    private readonly (int x, int y)[] oddRowNeighbors = new (int x, int y)[]
    {
        (-1, 0),  // 좌
        (0, 1),   // 상
        (1, 0),   // 우
        (0, -1)   // 하
    };


    private void Awake()
    {
        CalculateHexConstants();
        CalculateGridOrigin();
        CreateGrid();
    }

    private void CalculateHexConstants()
    {
        hexWidth = hexRadius * 2;
        hexHeight = hexWidth * Mathf.Sqrt(3) / 2;
        hexHorizontalSpacing = hexWidth * 0.75f;
        hexVerticalSpacing = hexHeight;
    }

    private void CalculateGridOrigin()
    {
        gridOrigin = new Vector3(
            -gridSize.x * hexHorizontalSpacing * 0.5f,
            0,
            -gridSize.y * hexVerticalSpacing * 0.5f
        );
    }

    private void CreateGrid()
    {
        grid = new HexNode[gridSize.x, gridSize.y];
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 worldPosition = GetWorldPositionFromHexCoords(x, y);
                bool walkable = true;
                grid[x, y] = new HexNode(walkable, worldPosition, x, y);
            }
        }
    }

    private Vector3 GetWorldPositionFromHexCoords(int x, int y)
    {
        float xPos = x * hexHorizontalSpacing;
        float zPos = y * hexVerticalSpacing;
        
        // 홀수 행은 오프셋 적용
        if (y % 2 != 0)
        {
            xPos += hexHorizontalSpacing * 0.5f;
        }

        return gridOrigin + new Vector3(xPos, 0, zPos);
    }

    private List<HexNode> GetNeighbors(HexNode node)
    {
        List<HexNode> neighbors = new List<HexNode>();
        var neighborDirections = (node.gridY % 2 == 0) ? evenRowNeighbors : oddRowNeighbors;

        foreach (var direction in neighborDirections)
        {
            int checkX = node.gridX + direction.x;
            int checkY = node.gridY + direction.y;

            if (IsValidPosition(checkX, checkY))
            {
                HexNode neighbor = grid[checkX, checkY];
                if (neighbor.walkable)
                {
                    neighbors.Add(neighbor);
                }
            }
        }

        return neighbors;
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y;
    }

    private int GetDistance(HexNode nodeA, HexNode nodeB)
    {
        // 육각 그리드에서의 거리 계산 (맨해튼 거리 변형)
        int dX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        
        // 홀수 행 보정
        if ((nodeA.gridY % 2 == 0 && nodeB.gridY % 2 != 0) ||
            (nodeA.gridY % 2 != 0 && nodeB.gridY % 2 == 0))
        {
            dX = Mathf.Max(0, dX - (dY + 1) / 2);
        }
        
        return MOVE_COST * (dX + Mathf.Max(0, dY - dX / 2));
    }

    public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        HexNode startNode = GetNodeFromWorldPosition(startPos);
        HexNode targetNode = GetNodeFromWorldPosition(targetPos);

        if (startNode == null || targetNode == null)
            return null;

        List<HexNode> openSet = new List<HexNode>();
        HashSet<HexNode> closedSet = new HashSet<HexNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            HexNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost ||
                    openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (HexNode neighbor in GetNeighbors(currentNode))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    private List<Vector3> RetracePath(HexNode startNode, HexNode endNode)
    {
        List<Vector3> path = new List<Vector3>();
        HexNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.worldPosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    public HexNode GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        // 육각 그리드에서 가장 가까운 노드 찾기
        float minDistance = float.MaxValue;
        HexNode closestNode = null;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                float distance = Vector3.Distance(grid[x, y].worldPosition, worldPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestNode = grid[x, y];
                }
            }
        }

        return closestNode;
    }

    private void OnDrawGizmos()
    {
        if (!showGrid) return;

        if (grid == null)
        {
            CalculateHexConstants();
            CalculateGridOrigin();
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 center = GetWorldPositionFromHexCoords(x, y);
                DrawHexagon(center, grid != null && !grid[x, y].walkable);
            }
        }
    }

    private void DrawHexagon(Vector3 center, bool isObstacle)
    {
        Gizmos.color = isObstacle ? obstacleColor : gridColor;
        
        Vector3[] vertices = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            float angle = i * 60f * Mathf.Deg2Rad;
            vertices[i] = center + new Vector3(
                hexRadius * Mathf.Cos(angle),
                0,
                hexRadius * Mathf.Sin(angle)
            );
        }

        for (int i = 0; i < 6; i++)
        {
            Gizmos.DrawLine(vertices[i], vertices[(i + 1) % 6]);
        }
    }

    public void ToggleNode(Vector3 targetPos)
    {
        HexNode targetNode = GetNodeFromWorldPosition(targetPos);
        if (targetNode != null)
        {
            targetNode.walkable = !targetNode.walkable;
        }
    }
}

public class HexNode
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public HexNode parent;

    public int fCost => gCost + hCost;

    public HexNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
}