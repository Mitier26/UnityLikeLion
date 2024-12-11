using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 메인 게임 매니저
public class Match3Game : MonoBehaviour
{
    [Header("Board Settings")]
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float gemSpacing = 1f;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject[] gemPrefabs;
    
    private Gem[,] gems;
    private Gem selectedGem;
    private bool isProcessing = false;
    private int movingGems = 0;
    
    private void Start()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        gems = new Gem[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                CreateGem(x, y);
            }
        }
        
        // 초기 매치 제거
        while (FindAllMatches().Count > 0)
        {
            foreach (var match in FindAllMatches())
            {
                RemoveGem(match.row, match.col);
                CreateGem(match.row, match.col);
            }
        }
    }

    private void CreateGem(int row, int col)
    {
        // 랜덤 보석 타입 선택
        GemType randomType = (GemType)Random.Range(0, gemPrefabs.Length);
        
        // 보석 생성
        GameObject gemObj = Instantiate(gemPrefabs[(int)randomType], transform);
        Gem gem = gemObj.GetComponent<Gem>();
        
        // 초기화
        gem.Initialize(this, randomType, row, col);
        gems[row, col] = gem;
    }

    public Vector2 GetWorldPosition(int row, int col)
    {
        return new Vector2(col * gemSpacing, row * gemSpacing);
    }

    public void SelectGem(Gem gem)
    {
        if (isProcessing) return;

        if (selectedGem == null)
        {
            selectedGem = gem;
        }
        else
        {
            if (IsAdjacent(selectedGem, gem))
            {
                StartSwap(selectedGem, gem);
            }
            selectedGem = null;
        }
    }

    private bool IsAdjacent(Gem gem1, Gem gem2)
    {
        return Mathf.Abs(gem1.row - gem2.row) + Mathf.Abs(gem1.col - gem2.col) == 1;
    }

    private void StartSwap(Gem gem1, Gem gem2)
    {
        isProcessing = true;
        
        // 배열에서 위치 교환
        gems[gem1.row, gem1.col] = gem2;
        gems[gem2.row, gem2.col] = gem1;
        
        // 실제 위치 이동
        int tempRow = gem1.row;
        int tempCol = gem1.col;
        
        gem1.MoveTo(gem2.row, gem2.col);
        gem2.MoveTo(tempRow, tempCol);
        
        movingGems = 2;
    }

    public void OnGemFinishedMoving()
    {
        movingGems--;
        
        if (movingGems == 0)
        {
            CheckMatches();
        }
    }

    private void CheckMatches()
    {
        List<Gem> matches = FindAllMatches();
        
        if (matches.Count > 0)
        {
            // 매치된 보석 제거
            foreach (var gem in matches)
            {
                RemoveGem(gem.row, gem.col);
            }
            
            // 빈 공간 채우기
            FillBoard();
        }
        else
        {
            isProcessing = false;
        }
    }

    private List<Gem> FindAllMatches()
    {
        HashSet<Gem> matches = new HashSet<Gem>();
        
        // 가로 매치 확인
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width - 2; col++)
            {
                Gem gem1 = gems[row, col];
                Gem gem2 = gems[row, col + 1];
                Gem gem3 = gems[row, col + 2];
                
                if (gem1.type == gem2.type && gem2.type == gem3.type)
                {
                    matches.Add(gem1);
                    matches.Add(gem2);
                    matches.Add(gem3);
                }
            }
        }
        
        // 세로 매치 확인
        for (int row = 0; row < height - 2; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Gem gem1 = gems[row, col];
                Gem gem2 = gems[row + 1, col];
                Gem gem3 = gems[row + 2, col];
                
                if (gem1.type == gem2.type && gem2.type == gem3.type)
                {
                    matches.Add(gem1);
                    matches.Add(gem2);
                    matches.Add(gem3);
                }
            }
        }
        
        return matches.ToList();
    }

    private void RemoveGem(int row, int col)
    {
        Destroy(gems[row, col].gameObject);
        gems[row, col] = null;
    }

    private void FillBoard()
    {
        for (int col = 0; col < width; col++)
        {
            int emptySpaces = 0;
            
            // 아래에서 위로 검사하며 빈 공간 채우기
            for (int row = 0; row < height; row++)
            {
                if (gems[row, col] == null)
                {
                    emptySpaces++;
                }
                else if (emptySpaces > 0)
                {
                    // 보석을 아래로 이동
                    gems[row - emptySpaces, col] = gems[row, col];
                    gems[row, col] = null;
                    gems[row - emptySpaces, col].MoveTo(row - emptySpaces, col);
                }
            }
            
            // 맨 위에 새로운 보석 생성
            for (int i = 0; i < emptySpaces; i++)
            {
                CreateGem(height - 1 - i, col);
            }
        }
        
        // 새로운 매치 확인
        CheckMatches();
    }
}