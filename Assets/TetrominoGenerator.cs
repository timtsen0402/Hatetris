using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TetrominoAlgorithm
{
    ChooseRandomly,
    Choose7Bag,
    ChooseSZ
}

public class TetrominoGenerator : MonoBehaviour
{
    public static TetrominoGenerator Instance { get; private set; }
    GameObject[] tetrominoes;
    List<GameObject> currentTetrominoes = new List<GameObject>();

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
        currentTetrominoes = tetrominoes.ToList();
    }
    public GameObject ChooseRandomly()
    {
        int randomIndex = Random.Range(0, tetrominoes.Length);
        return tetrominoes[randomIndex];
    }
    public GameObject ChooseSZ()
    {
        if (Mechanics.used % 2 == 0)
            return tetrominoes[0];
        else
            return tetrominoes[1];
    }
    public GameObject Choose7Bag()
    {
        if (currentTetrominoes.Count == 0)
        {
            currentTetrominoes = tetrominoes.ToList();
        }
        int randomIndex = Random.Range(0, currentTetrominoes.Count);
        GameObject selectedTetrominoes = currentTetrominoes[randomIndex];
        currentTetrominoes.Remove(selectedTetrominoes);
        return selectedTetrominoes;

    }


    // public GameObject ChooesByMiniMax()
}
