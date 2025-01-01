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
            // 화면에 조작 할 수 있는 블록이 있을 때만 작동한다.
            
            // 방향키를 입렵하면서 떨어지면 분해가 안됨.
            
            if (_currentTetrominoData.IsUnityNull())
                return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveBlock(Vector3.left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveBlock(Vector3.right);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                RotateBlock(-90);
            }
            
            // 아래 키 입력
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveBlock(Vector3.down);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DropBlock();
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
                        
                        // 이것은 블록을 생성하기 전에 반드시 해야하는 것
                        // 부모 분해, 여기서 부모가 살아 있다 제거하자
                        FixBlock();

                        // 블록의 라인 체크
                        DeleteLines();
                        
                        SpawnTetromino();
                    }
                }
                
                currentDropTime = dropTime;
            }
        }

        private void MoveBlock(Vector3 direction)
        {
            // 그리드 배열을 세팅하는 것
            SetGridState(0);
                
            // 이동하는 것! 배열료 이동하는 것이 아니고 
            // 이돋을 하고 배열에 넣는다.
            _currentTetrominoData.transform.position += direction;
            var (minX, minY, maxX, maxY) = GetGridState();
            // 중요한 체크!!!
            if (checkBlockCollision() || GridOverlapCheck() || 0 > minY)
            {
                _currentTetrominoData.transform.position -= direction;
            }
            SetGridState(1);
        }

        private void RotateBlock(float angle)
        {
            SetGridState(0);
            _currentTetrominoData.transform.Rotate(new Vector3(0, 0, angle));
                
            var (minX, minY, maxX, maxY) = GetGridState();
                
            if (checkBlockCollision()|| GridOverlapCheck() || 0 > minY)
            {
                _currentTetrominoData.transform.Rotate(new Vector3(0, 0, -angle));
            }
            SetGridState(1);
        }

        private void DropBlock()
        {
            SetGridState(0);
            while (true)
            {
                _currentTetrominoData.transform.position += Vector3.down;
                var (minX, minY, maxX, maxY) = GetGridState();
                if (checkBlockCollision() || GridOverlapCheck() || 0 > minY)
                {
                    _currentTetrominoData.transform.position -= Vector3.down;
                    break;
                }
                
            }
            SetGridState(1);
            
            // 다른 블록을 소환하기 전에 분리해야함.
            FixBlock();
            
            DeleteLines();
            
            SpawnTetromino();
        }

        private void FixBlock()
        {
            GameObject parent = _currentTetrominoData.gameObject;
            foreach (var block in _currentTetrominoData.Blocks)
            {
                block.transform.SetParent(null);
                var (x, y) = GetXYIndex(block);
                gridBlock[y][x] = block.GetComponent<Block>();
            }
            Destroy(parent);
        }

        private void DeleteLines()
        {
            List<int> deleteLines = new List<int>();

            for (int y = 0; y < grid.Length; ++y)
            {
                int count = grid[y].Count(e => e == 1);
                
                if(count == LINE_MAX_INDEX)
                    deleteLines.Add(y);
            }

            if (deleteLines.Count == 0) return;
                
            for (int i = deleteLines.Count - 1; i >= 0; i--)
            {
                int row = deleteLines[i];

                // 현재 줄의 블록을 삭제
                for (int x = 0; x < grid[row].Length; x++)
                {
                    grid[row][x] = 0;
                    if (gridBlock[row][x] != null)
                    {
                        Destroy(gridBlock[row][x].gameObject);
                        gridBlock[row][x] = null;
                    }
                }

                // 위의 모든 줄을 아래로 이동
                for (int y = row; y < grid.Length - 1; y++)
                {
                    grid[y] = grid[y + 1];
                    gridBlock[y] = gridBlock[y + 1];

                    // 블록의 실제 위치도 업데이트
                    for (int x = 0; x < gridBlock[y].Length; x++)
                    {
                        if (gridBlock[y][x] != null)
                        {
                            gridBlock[y][x].transform.position += Vector3.down;
                        }
                    }
                }

                // 가장 위 줄 초기화
                grid[^1] = new int[LINE_MAX_INDEX];
                gridBlock[^1] = new Block[LINE_MAX_INDEX];
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
                
                // 조건문은 블록이 게임판 안에 있을 때!
                // offset을 이용해 왼쪽 아래가 0,0 이라는 것을 명심한다.
                // 현재 움직이고 있는 블록의 자식이 게임판 안에 있을 때
                // 그리드의 값이 1 이면 멈춘다.
                // 블록이 있는 곳은 1
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

                // 입력 받는 state에 따라 0 또는 1로 배열에 저장
                if (y >= 0 && x >= 0 && x < grid[y].Length) 
                    grid[y][x] = state;
            }
        }

        private static (int, int) GetXYIndex(Transform block)
        {
            // 자식 블록의 위치를 배열의 인덱스로 사용
            // offset는 왼쪽 아래를 0,0으로 맞추기 위함
            int y = Mathf.RoundToInt(block.transform.position.y + Y_OFFSET);
            int x = Mathf.RoundToInt(block.transform.position.x + X_OFFSET);
            
            // 자식 블록의 위치에 따른 x, y 좌표
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
            // 생성된 spawndTetromino 에 currentTetrominoData가 있으면 currentTetromino에 타입에 맞는 값을 넣어라.
            // currentTetromino의 타입은 TetrominoData!!!
            // 생성되는 오브젝트는 TetrominoData를 가지고 있는 오브젝트
            // 그래서 currentTetromino에 넣음
        }


        bool checkBlockCollision()
        {
            foreach (var block in _currentTetrominoData.Blocks)
            {
                var (x, y) = GetXYIndex(block);
                // 블럭이 밖으로 나가지 못함
                if (x < 0 || x >= grid[0].Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
