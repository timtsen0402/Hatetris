using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    public GameObject[] tetrominos;
    public static int map_height = 20;
    public static int map_width = 10;
    public static bool isPlaced = false;
    public static bool isGameOver = false;
    public static Vector3 spawnPosition = new Vector3(5, 17, 0);
    public static Transform[,] grid = new Transform[map_width, map_height];
    public static int score = 0;

    void Update()
    {
        Spawn();
        CheckLines();
    }
    void Spawn()
    {
        if (isPlaced)
        {
            int randomIndex = Random.Range(0, 7);
            Instantiate(tetrominos[randomIndex], spawnPosition, Quaternion.identity);
            isPlaced = false;
        }
    }
    void CheckLines()
    {
        for (int j = map_height - 1; j >= 0; j--)
        {
            if (HasRow(j))
            {
                DeleteLine(j);
                UpdateLine(j);

                score++;
                //音效
                //UI
            }
        }
    }
    bool HasRow(int j)
    {
        for (int i = 0; i < map_width; i++)
        {
            if (grid[i, j] == null)
                return false;
        }
        return true;
    }
    void DeleteLine(int j)
    {
        for (int i = 0; i < map_width; i++)
        {
            Destroy(grid[i, j].gameObject);
            grid[i, j] = null;
        }
    }
    void UpdateLine(int j)
    {
        for (int k = j + 1; k < map_height; k++)
        {
            for (int i = 0; i < map_width; i++)
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

    /*
    void CheckGameOver()
    {
        if (HasBlockOverLine())
        {
            isGameOver = true;
            //凍結畫面並顯示重新開始或離開
        }
    }
    bool HasBlockOverLine()
    {
        for (int j = 16; j < map_height; j++)
            for (int i=0; i<map_width;i++)
        {

        }
    }
    */

}
