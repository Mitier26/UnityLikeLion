    using UnityEngine;
    using System.Collections.Generic;

    public class GridPathfinding : MonoBehaviour
    {
    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] private float nodeSize = 1f;
        [SerializeField] private bool allowDiagonalMovement = true;
        [Header("Grid Visualization")]
        [SerializeField] private bool showGrid = true;
        [SerializeField] private Color gridColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        [SerializeField] private Color obstacleColor = new Color(1f, 0f, 0f, 0.5f);
        private Node[,] grid;

        // 직선 이동과 대각선 이동의 비용
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        
        private void Awake()
        {
            CalculateGridOrigin();
            CreateGrid();
        }

        private void CalculateGridOrigin()
        {
            // 그리드의 중심이 (0,0,0)이 되도록 원점 계산
            gridOrigin = new Vector3(
                -gridSize.x * nodeSize * 0.5f,
                0,
                -gridSize.y * nodeSize * 0.5f
            );
        }

        private void CreateGrid()
        {
            grid = new Node[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector3 worldPosition = gridOrigin + new Vector3(
                        x * nodeSize + nodeSize * 0.5f,  // 노드의 중심에 위치하도록 nodeSize * 0.5f 더함
                        0,
                        y * nodeSize + nodeSize * 0.5f
                    );
                    bool walkable = true;
                    grid[x, y] = new Node(walkable, worldPosition, x, y);
                }
            }
        }

        
        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            // 상하좌우 이동
            CheckNeighbor(node, 0, 1, neighbors);  // 상
            CheckNeighbor(node, 0, -1, neighbors); // 하
            CheckNeighbor(node, -1, 0, neighbors); // 좌
            CheckNeighbor(node, 1, 0, neighbors);  // 우

            if (allowDiagonalMovement)
            {
                // 대각선 이동 - 대각선 이동시 벽을 통과하지 못하도록 검사
                bool topFree = IsWalkable(node.gridX, node.gridY + 1);
                bool bottomFree = IsWalkable(node.gridX, node.gridY - 1);
                bool leftFree = IsWalkable(node.gridX - 1, node.gridY);
                bool rightFree = IsWalkable(node.gridX + 1, node.gridY);

                if (topFree && rightFree) CheckNeighbor(node, 1, 1, neighbors);   // 우상
                if (topFree && leftFree) CheckNeighbor(node, -1, 1, neighbors);   // 좌상
                if (bottomFree && rightFree) CheckNeighbor(node, 1, -1, neighbors); // 우하
                if (bottomFree && leftFree) CheckNeighbor(node, -1, -1, neighbors); // 좌하
            }

            return neighbors;
        }

        private void CheckNeighbor(Node node, int xOffset, int yOffset, List<Node> neighbors)
        {
            int checkX = node.gridX + xOffset;
            int checkY = node.gridY + yOffset;

            if (IsValidPosition(checkX, checkY))
            {
                Node neighbor = grid[checkX, checkY];
                if (neighbor.walkable)
                {
                    neighbors.Add(neighbor);
                }
            }
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y;
        }

        private bool IsWalkable(int x, int y)
        {
            if (!IsValidPosition(x, y)) return false;
            return grid[x, y].walkable;
        }

        private int GetDistance(Node nodeA, Node nodeB)
        {
            int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (!allowDiagonalMovement)
            {
                return MOVE_STRAIGHT_COST * (distX + distY);
            }

            int diagonal = Mathf.Min(distX, distY);
            int straight = Mathf.Abs(distX - distY);
            return MOVE_DIAGONAL_COST * diagonal + MOVE_STRAIGHT_COST * straight;
        }

        // 기존 OnDrawGizmos와 나머지 메서드들은 동일하게 유지...

        public void ToggleNode(Vector3 targetPos)
        {
            Node targetNode = GetNodeFromWorldPosition(targetPos);
            if (targetNode != null)
            {
                targetNode.walkable = !targetNode.walkable;
            }
        }
        
        public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = GetNodeFromWorldPosition(startPos);
            Node targetNode = GetNodeFromWorldPosition(targetPos);

            if (startNode == null || targetNode == null)
                return null;

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
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

                foreach (Node neighbor in GetNeighbors(currentNode))
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

        private List<Vector3> RetracePath(Node startNode, Node endNode)
        {
            List<Vector3> path = new List<Vector3>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.worldPosition);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }
        
        private Vector3 gridOrigin;

        public Node GetNodeFromWorldPosition(Vector3 worldPosition)
        {
            // 월드 좌표를 그리드 좌표로 변환
            Vector3 localPosition = worldPosition - gridOrigin;
            float percentX = localPosition.x / (gridSize.x * nodeSize);
            float percentY = localPosition.z / (gridSize.y * nodeSize);

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.FloorToInt(percentX * gridSize.x);
            int y = Mathf.FloorToInt(percentY * gridSize.y);

            // 경계 체크
            x = Mathf.Clamp(x, 0, gridSize.x - 1);
            y = Mathf.Clamp(y, 0, gridSize.y - 1);

            return grid[x, y];
        }

        private void OnDrawGizmos()
        {
            if (!showGrid) return;
        
            if (grid == null)
                CalculateGridOrigin();
            
            //if (grid == null)
            {
                
                for (int x = 0; x < gridSize.x; x++)
                {
                    for (int y = 0; y < gridSize.y; y++)
                    {
                        Gizmos.color = gridColor;
                        Vector3 pos = gridOrigin + new Vector3(
                            x * nodeSize + nodeSize * 0.5f,
                            0,
                            y * nodeSize + nodeSize * 0.5f
                        );
                        Gizmos.DrawWireCube(pos, new Vector3(nodeSize, 0.1f, nodeSize));

                        if (grid != null)
                        {
                            Node curNode = GetNodeFromWorldPosition(pos);

                            if (!curNode.walkable)
                            {
                                Gizmos.color = obstacleColor;
                                Gizmos.DrawCube(pos, new Vector3(nodeSize, 0.1f, nodeSize));
                            }
                        }
                    }
                }
            }
        }
    }

    public class Node
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;
        public int gCost;
        public int hCost;
        public Node parent;

        public int fCost => gCost + hCost;

        public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
        {
            walkable = _walkable;
            worldPosition = _worldPos;
            gridX = _gridX;
            gridY = _gridY;
        }
    }