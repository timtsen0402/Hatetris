// 使用 SerializeField 使这些变量可在 Inspector 中编辑


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    // 单例实例
    public static Mechanics Instance { get; private set; }

    public GameObject[] tetrominos;
    public TextMeshProUGUI score_txt;
    public GameObject Map;

    [field: SerializeField] public int MapHeight { get; private set; } = 20;
    [field: SerializeField] public int MapWidth { get; private set; } = 10;
    [field: SerializeField] public int Vacuum { get; private set; } = 3;

    int deadLine;

    public static bool isPlaced = true;
    public static bool isGameOver = false;
    public static Vector3 offset = new Vector2(-0.5f, -0.5f);
    public static Vector3 spawnPosition;
    public static Transform[,] grid;
    public static int score = 0;

    private void Awake()
    {
        // 单例模式实现
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        // 初始化网格
        grid = new Transform[MapWidth, MapHeight];

        // 设置地图大小
        Map.transform.localScale = new Vector2(MapWidth, MapHeight);

        deadLine = MapHeight - Vacuum;

        // 计算生成位置
        spawnPosition = new Vector2(MapWidth / 2, deadLine);

        score = 0;
        isGameOver = false;
        isPlaced = true;
    }

    void Update()
    {
        if (!isGameOver)
        {
            Spawn();
            CheckLines();
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        score_txt.text = score.ToString();
    }

    public void Spawn()
    {
        if (isPlaced)
        {
            int randomIndex = Random.Range(0, tetrominos.Length);
            GameObject newTetromino = Instantiate(tetrominos[randomIndex], spawnPosition, Quaternion.identity);


            newTetromino.transform.position += offset;

            isPlaced = false;
            CheckGameOver();
        }
    }

    void CheckLines()
    {
        for (int j = MapHeight - 1; j >= 0; j--)
        {
            if (HasRow(j))
            {
                DeleteLine(j);
                UpdateLine(j);
                score++;
            }
        }
    }

    bool HasRow(int j)
    {
        for (int i = 0; i < MapWidth; i++)
        {
            if (grid[i, j] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int j)
    {
        for (int i = 0; i < MapWidth; i++)
        {
            Destroy(grid[i, j].gameObject);
            grid[i, j] = null;
        }
    }

    void UpdateLine(int j)
    {
        for (int k = j + 1; k < MapHeight; k++)
        {
            for (int i = 0; i < MapWidth; i++)
            {
                if (grid[i, k] != null)
                {
                    grid[i, k - 1] = grid[i, k];
                    grid[i, k] = null;
                    grid[i, k - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void CheckGameOver()
    {
        for (int j = deadLine; j < MapHeight; j++)
        {
            for (int i = 0; i < MapWidth; i++)
            {
                if (grid[i, j] != null)
                {
                    isGameOver = true;
                    Debug.Log("Game Over");
                    // 在这里添加游戏结束的逻辑
                    return;
                }
            }
        }
    }

    public void RestartGame()
    {
        // 清除现有的所有方块
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                if (grid[i, j] != null)
                {
                    Destroy(grid[i, j].gameObject);
                    grid[i, j] = null;
                }
            }
        }

        // 重新初始化游戏
        InitializeGame();
    }
}