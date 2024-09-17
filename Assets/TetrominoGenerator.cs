using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoGenerator : MonoBehaviour
{
    public static TetrominoGenerator Instance { get; private set; }
    GameObject[] tetrominoes;
    private void Awake()
    {
        // 单例模式实现
        if (Instance == null)
        {
            Instance = this;
            tetrominoes = Mechanics.Instance.Tetrominoes;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
    }
    public GameObject ChooseRandomly()
    {
        int randomIndex = Random.Range(0, tetrominoes.Length);
        return tetrominoes[randomIndex];
    }

    // public GameObject ChooesByMiniMax()
}
