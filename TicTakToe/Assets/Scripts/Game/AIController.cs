﻿public static class AIController
{
    public static (int row, int col)? FindNextMove(GameManager.PlayerType[,] board)
    {
        // 가로, 세로, 대각선 비교
        var result = FindTwoMaker(board, GameManager.PlayerType.PlayerB);
        if (result.HasValue) return result.Value;
        var result2 = FindTwoMaker(board, GameManager.PlayerType.PlayerA);
        if (result2.HasValue) return result2.Value;
        var result3 = FindEmptyPosition(board, GameManager.PlayerType.PlayerA);
        if (result3.HasValue) return result3.Value;
        var result4 = FindEmptyPosition(board, GameManager.PlayerType.PlayerB);
        if (result4.HasValue) return result4.Value;
        var result5 = FindEmptyPosition(board, GameManager.PlayerType.None);
        if (result5.HasValue) return result5.Value;

        return null;
    }
    
    private static (int row, int col)? FindEmptyPosition(GameManager.PlayerType[,] board, GameManager.PlayerType playerType)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == GameManager.PlayerType.None)
                {
                    if(playerType == GameManager.PlayerType.None) return (row, col);
                    
                    for(var i = -1; i <= 1; i++)
                    {
                        for(var j = -1; j <= 1; j++)
                        {
                            if(i == 0 && j == 0) continue;
                            if(row + i < 0 || row  + i >= board.GetLength(0) || col + j < 0 || col + j >= board.GetLength(1)) continue;
                            if(board[row + i, col + j] == playerType) return (row, col);
                        }
                    }
                }
            }
        }

        return null;
    }
    
    // nullable 튜플 뒤에 ?를 붙이면 null을 반환할 수 있다.
    // 튜플은 사실 null을 반환할 수 없는데 이렇게 하면 null을 반환할 수 있다.
    private static (int row, int col)? FindTwoMaker(GameManager.PlayerType[,] board, GameManager.PlayerType playerType)
    {
        // 가로 플레이어 마커가 두 개 이상힌지 확인
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType && board[row, 1] == playerType &&
                board[row, 2] == GameManager.PlayerType.None)
            {
                return (row, 2);
            }

            if (board[row, 1] == playerType && board[row, 2] == playerType &&
                board[row, 0] == GameManager.PlayerType.None)
            {
                return (row, 0);
            }

            if (board[row, 0] == playerType && board[row, 2] == playerType &&
                board[row, 1] == GameManager.PlayerType.None)
            {
                return (row, 1);
            }
        }
        // 세로 플레이어 마커가 두 개 이상힌지 확인
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType && board[1, col] == playerType &&
                board[2, col] == GameManager.PlayerType.None)
            {
                return (2, col);
            }

            if (board[1, col] == playerType && board[2, col] == playerType &&
                board[0, col] == GameManager.PlayerType.None)
            {
                return (0, col);
            }

            if (board[0, col] == playerType && board[2, col] == playerType &&
                board[1, col] == GameManager.PlayerType.None)
            {
                return (1, col);
            }
        }
        
        // 대각선 플레이어 마커가 두 개 이상힌지 확인
        if(board[0,0] == playerType && board[1,1] == playerType && 
            board[2,2] == GameManager.PlayerType.None)
        {
            return (2, 2);
        }
        if(board[0,0] == playerType && board[2,2] == playerType && 
            board[1,1] == GameManager.PlayerType.None)
        {
            return (1, 1);
        }
        if(board[1,1] == playerType && board[2,2] == playerType && 
            board[0,0] == GameManager.PlayerType.None)
        {
            return (0, 0);
        }
                
        if(board[0,2] == playerType && board[1,1] ==playerType && 
            board[2,0] == GameManager.PlayerType.None)
        {
            return (2, 0);
        }
        if(board[0,2] == playerType && board[2,0] == playerType && 
            board[1,1] == GameManager.PlayerType.None)
        {
            return (1, 1);
        }
        if(board[1,1] == playerType && board[2,0] == playerType && 
            board[0,2] == GameManager.PlayerType.None)
        {
            return (0, 2);
        }

        return null;
    }
    
}
