using UnityEngine;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class TicTacToe : MonoBehaviour
{
    // 初始化井字棋盘
    private char[,] board = new char[3, 3];

    private void Start()
    {
        // 初始化测试棋盘
        char[,] testBoard = new char[3, 3]
        {
            {' ', ' ', ' '},
            {' ', ' ', ' '},
            {' ', ' ', ' '}
        };

        CompareMinimaxAlgorithms(testBoard);
    }

    // 打印棋盘
    private void PrintBoard(char[,] board)
    {
        string boardString = "";
        for (int i = 0; i < 3; i++)
        {
            boardString += $"{board[i, 0]}|{board[i, 1]}|{board[i, 2]}\n";
            if (i < 2) boardString += "-+-+-\n";
        }
        Debug.Log(boardString);
    }

    // 检查是否有玩家获胜
    private char CheckWinner(char[,] board)
    {
        // 检查行和列
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ')
                return board[i, 0];
            if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ')
                return board[0, i];
        }
        // 检查对角线
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ')
            return board[0, 0];
        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' ')
            return board[0, 2];
        return ' ';
    }

    // 检查是否平局
    private bool IsDraw(char[,] board)
    {
        return !board.Cast<char>().Any(c => c == ' ');
    }

    // 原始Minimax算法
    private int MinimaxOriginal(char[,] board, bool isMax)
    {
        char winner = CheckWinner(board);
        if (winner == 'X') return 1;
        if (winner == 'O') return -1;
        if (IsDraw(board)) return 0;

        int bestScore = isMax ? int.MinValue : int.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = isMax ? 'X' : 'O';
                    int score = MinimaxOriginal(board, !isMax);
                    board[i, j] = ' ';
                    bestScore = isMax ? Mathf.Max(score, bestScore) : Mathf.Min(score, bestScore);
                }
            }
        }
        return bestScore;
    }

    // 使用Alpha-Beta剪枝的Minimax函数
    private int MinimaxAlphaBeta(char[,] board, int depth, bool isMax, int alpha, int beta)
    {
        char winner = CheckWinner(board);
        if (winner == 'X') return 1;
        if (winner == 'O') return -1;
        if (IsDraw(board)) return 0;

        int bestScore = isMax ? int.MinValue : int.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = isMax ? 'X' : 'O';
                    int score = MinimaxAlphaBeta(board, depth + 1, !isMax, alpha, beta);
                    board[i, j] = ' ';
                    if (isMax)
                    {
                        bestScore = Mathf.Max(score, bestScore);
                        alpha = Mathf.Max(alpha, bestScore);
                    }
                    else
                    {
                        bestScore = Mathf.Min(score, bestScore);
                        beta = Mathf.Min(beta, bestScore);
                    }
                    if (beta <= alpha) break;
                }
            }
        }
        return bestScore;
    }

    // 比较函数
    private void CompareMinimaxAlgorithms(char[,] board)
    {
        Stopwatch stopwatch = new Stopwatch();

        // 测试原始Minimax
        stopwatch.Start();
        for (int i = 0; i < 100; i++)
        {
            MinimaxOriginal(board, true);
        }
        stopwatch.Stop();
        double originalTime = stopwatch.Elapsed.TotalSeconds;

        // 测试Alpha-Beta剪枝版本
        stopwatch.Restart();
        for (int i = 0; i < 100; i++)
        {
            MinimaxAlphaBeta(board, 0, true, int.MinValue, int.MaxValue);
        }
        stopwatch.Stop();
        double alphaBetaTime = stopwatch.Elapsed.TotalSeconds;

        Debug.Log($"Original Minimax: {originalTime} seconds");
        Debug.Log($"Alpha-Beta Pruning: {alphaBetaTime} seconds");
        Debug.Log($"Improvement: {(originalTime - alphaBetaTime) / originalTime * 100}%");
    }
}