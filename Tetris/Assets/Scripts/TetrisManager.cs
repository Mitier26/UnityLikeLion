    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AYellowpaper.SerializedCollections;
    using Unity.VisualScripting;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public enum TetrominoType : byte
    {
        None,
        I,
        O,
        Z,
        S,
        J,
        L,
        T,
        Max
    }

    public class TetrisManager : Singleton<TetrisManager>
    {
        const float X_OFFSET = 4.5f;
        const float Y_OFFSET = 8.5f;
        private const int LINE_MAX_INDEX = 10;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private SerializedDictionary<TetrominoType, string> tetrominoDatas;
        [SerializeField] private float dropTime = 1.0f;

        private TetrominoData _currentTetrominoData;
        private float currentDropTime = 0.0f;

        private int[][] grid = null;
        private Block[][] gridBlock = null;

        public override void OnAwake()
        {
            base.OnAwake();

            grid = new int[25][];
            gridBlock = new Block[25][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new int[10];
                gridBlock[i] = new Block[10];
            }
        }

        void Start()
        {
            currentDropTime = dropTime;
            SpawnTetromino();
        }

        private void OnDrawGizmos()
        {
            if (grid == null)
                return;
            
            for (int y = 0; y < grid.Length; ++y)
            {
                for (int x = 0; x < grid[y].Length; ++x)
                {
                    if (grid[y][x] == 1)
                    {
                        int a = 10;
                    }
                    
                    Color color = grid[y][x] == 1 ? Color.green : Color.red;
                    Gizmos.color = color;
                    Gizmos.DrawSphere(new Vector3(x - X_OFFSET, y - Y_OFFSET, 0), 0.3f);
                }
            }
        }

        void Update()
        {
            if (_currentTetrominoData.IsUnityNull())
                return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                SetGridState(0);
                _currentTetrominoData.transform.position += Vector3.left;
                
                if (checkBlockCollision() || GridOverlapCheck())
                {
                    _currentTetrominoData.transform.position -= Vector3.left;
                }
                SetGridState(1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                SetGridState(0);
                _currentTetrominoData.transform.position += Vector3.right;
                if (checkBlockCollision() || GridOverlapCheck())
                {
                    _currentTetrominoData.transform.position -= Vector3.right;
                }
                SetGridState(1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                SetGridState(0);
                _currentTetrominoData.transform.Rotate(new Vector3(0, 0, -90));
                
                var (minX, minY, maxX, maxY) = GetGridState();
                
                if (checkBlockCollision()|| GridOverlapCheck() || 0 > minY)
                {
                    _currentTetrominoData.transform.Rotate(new Vector3(0, 0, 90));
                }
                SetGridState(1);
            }

            currentDropTime -= Time.deltaTime;
            if (currentDropTime <= 0.0f)
            {
                SetGridState(0);
                
                _currentTetrominoData.transform.position += Vector3.down;
                
                if (GridOverlapCheck())
                {
                    _currentTetrominoData.transform.position -= Vector3.down;
                    SetGridState(1);
                    SpawnTetromino();
                }
                else
                {
                    SetGridState(1);

                    var (minX, minY, maxX, maxY) = GetGridState();
                    
                    if (minY == -1)
                    {
                        
                        SetGridState(0);
                        _currentTetrominoData.transform.position -= Vector3.down;                    
                        SetGridState(1);

                        foreach (var block in _currentTetrominoData.Blocks)
                        {
                            block.transform.SetParent(null);
                            var (x, y) = GetXYIndex(block);
                            gridBlock[y][x] = block.GetComponent<Block>();
                        }

                        
                        int yIndex = 0;
                        int count = grid[yIndex].Count(e => e == 1);
                        Debug.Log(count);

                        if (LINE_MAX_INDEX == count)
                        {
                            for (var i = 0; i < grid[yIndex].Length; i++)
                            {
                                grid[yIndex][i] = 0;
                                Destroy(gridBlock[yIndex][i].gameObject);
                                gridBlock[yIndex][i] = null;
                            }

                            for (int i = 0; i < grid.Length - 1; ++i)
                            {
                                grid[i] = grid[i + 1];
                                for (int x = 0; x < gridBlock[i].Length; ++x)
                                {
                                    if (gridBlock[i][x])
                                        gridBlock[i][x].transform.position += Vector3.down;
                                }
                                gridBlock[i] = gridBlock[i + 1];
                            }
                            
                            for (var i = 0; i < grid[^1].Length; i++)
                            {
                                grid[^1][i] = 0;
                                gridBlock[^1][i] = null;
                            }
                        }
                        
                        SpawnTetromino();
                    }
                }
                
                currentDropTime = dropTime;
            }
        }

        private (int, int, int, int) GetGridState()
        {
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;
            int maxX = Int32.MinValue;
            int maxY = Int32.MinValue;

            foreach (var block in _currentTetrominoData.Blocks)
            {
                var (x, y) = GetXYIndex(block);
                ;
                minX = Mathf.Min(minX, x);
                minY = Mathf.Min(minY, y);
                maxX = Mathf.Max(maxX, x);
                maxY = Mathf.Max(maxY, y);
            }

            return (minX, minY, maxX, maxY);
        }
        
        private bool GridOverlapCheck()
        {
            foreach (var block in _currentTetrominoData.Blocks)
            {
                var (x, y) = GetXYIndex(block);

                if (y >= 0 && x >= 0 && x < grid[y].Length && grid[y][x] == 1)
                {
                    return true;
                }
            }

            return false;
        }
        
        private void SetGridState(int state)
        {
            foreach (var block in _currentTetrominoData.Blocks)
            {
                var (x, y) = GetXYIndex(block);

                if (y >= 0 && x >= 0 && x < grid[y].Length) 
                    grid[y][x] = state;
            }
        }

        private static (int, int) GetXYIndex(Transform block)
        {
            int y = Mathf.RoundToInt(block.transform.position.y + Y_OFFSET);
            int x = Mathf.RoundToInt(block.transform.position.x + X_OFFSET);
            return (x, y);
        }


        private void SpawnTetromino()
        {
            GameObject Tetromino_Prefab = null;
            TetrominoType nextBlockIndex = TetrominoType.I;//(TetrominoType)Random.Range(0, (int)TetrominoType.Max - 1) + 1;
            Tetromino_Prefab = Resources.Load<GameObject>($"Prefab/{tetrominoDatas[nextBlockIndex]}");

            GameObject spawndTetromino = Instantiate(Tetromino_Prefab, spawnPoint.position, Quaternion.identity);
            spawndTetromino.TryGetComponent(out _currentTetrominoData);
        }


        bool checkBlockCollision()
        {
            foreach (var block in _currentTetrominoData.Blocks)
            {
                var (x, y) = GetXYIndex(block);
                if (x < 0 || x >= grid[0].Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
