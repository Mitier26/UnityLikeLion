    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AYellowpaper.SerializedCollections;
    using Unity.VisualScripting;
    using UnityEngine;
    using Random = UnityEngine.Random;

    // 테스리스 블록의 형태 타입
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
        // 기준점을 왼쪽 아래로 맟주기 위한 offset
        const float X_OFFSET = 4.5f;
        const float Y_OFFSET = 8.5f;
        // 한 줄의 최대 인덱스
        private const int LINE_MAX_INDEX = 10;
        
        // 블록이 소환되는 지점
        [SerializeField] private Transform spawnPoint;
        // 이것은 외부 에셋
        // 인스펙터에 딕셔너리가 보임, 매우 편하다.
        [SerializeField] private SerializedDictionary<TetrominoType, string> tetrominoDatas;
        // 블록이 내려 가는 딜레이
        [SerializeField] private float dropTime = 1.0f;

        private TetrominoData _currentTetrominoData;
        // 블록이 떨어지는 경과 시간
        private float currentDropTime = 0.0f;
        
        // 게임판
        private int[][] grid = null;
        // Tetromino 가 안착했을 때 Tetromino 자식을 담는 것
        private Block[][] gridBlock = null;

        public override void OnAwake()
        {
            // Awake에서 초기화 하고 싶지만 상속 부모의 Awake는 건들 수 없다.
            // OnAwake를 이용해 만든다.
            
            // 이 것은 부모의 Awake를 실행
            base.OnAwake();
            
            // 여기 TetrisManager 에서 사용할 Awake
            
            InitialGrid();
        }

        private void InitialGrid()
        {
            // 게임판을 만듬, 높이는 25
            grid = new int[25][];
            
            // 분리되는 블록을 넣는 것
            gridBlock = new Block[25][];
            
            // 한 줄 당 10개의 칸을 만듬
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new int[10];
                // 왜 int 인가?
                // 0 : 비어 있는 것
                // 1 : 블록이 있는 것
                
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
            // 화면에 정보를 표시
            
            // 그리드가 없으면 표시 하지 않음
            if (grid == null)
                return;
            
            for (int y = 0; y < grid.Length; ++y)
            {
                for (int x = 0; x < grid[y].Length; ++x)
                {
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
        
        // 다른 블록과 겹쳐있는지 확인 하는 것
        private bool GridOverlapCheck()
        {
            // _currentTetrominoData.Blocks : 지금 컨트롤 하는 블록의 자식 블록들
            // TetrominoData에 프로퍼티 형태로 자식을 반환한다.
            foreach (var block in _currentTetrominoData.Blocks)
            {
                // 좌, 우와 아래쪽 블록 범위를 확인
                var (x, y) = GetXYIndex(block);
                
                // 블록이 아래 있지 않거나 그리드의 1 ( 블록 있음 ) 이면
                // 블록이 게임판 안에 있는데 다른 블록이 있으면
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
            TetrominoType nextBlockIndex = (TetrominoType)Random.Range(0, (int)TetrominoType.Max - 1) + 1;
            Tetromino_Prefab = Resources.Load<GameObject>($"Prefab/{tetrominoDatas[nextBlockIndex]}");

            GameObject spawndTetromino = Instantiate(Tetromino_Prefab, spawnPoint.position, Quaternion.identity);
            spawndTetromino.TryGetComponent(out _currentTetrominoData);
            // out
            // 
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
