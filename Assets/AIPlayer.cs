using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public static AIPlayer Instance { get; private set; }
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

    public void MoveTetrominoRandomly()
    {
        StartCoroutine(MoveTetrominoRandomlyCoroutine());
    }
    public void PlaceTetrominoRandomly()
    {
        StartCoroutine(PlaceTetrominoRandomlyCoroutine());
    }

    private IEnumerator MoveTetrominoRandomlyCoroutine()
    {
        GameObject tetromino = Mechanics.Instance.GetCurrentTetromino();
        if (tetromino == null)
        {
            Debug.LogWarning("No current tetromino found.");
            yield break;
        }

        Motion motion = tetromino.GetComponent<Motion>();
        if (motion == null)
        {
            Debug.LogError("Motion component not found on tetromino.");
            yield break;
        }

        int horizontalSteps = Random.Range(-2, 3); // 允許左右移動
        int direction = (horizontalSteps >= 0) ? 1 : -1;

        for (int i = 0; i < Mathf.Abs(horizontalSteps); i++)
        {
            Vector3 movement = new Vector3(direction, 0, 0);
            tetromino.transform.position += movement;

            if (!motion.ValidMove())
            {
                tetromino.transform.position -= movement;
                break; // 如果移動無效，停止嘗試
            }

            yield return new WaitForSeconds(.5f); // 更快的移動速度
        }
    }
    private IEnumerator PlaceTetrominoRandomlyCoroutine()
    {
        GameObject tetromino = Mechanics.Instance.GetCurrentTetromino();
        if (tetromino == null)
        {
            Debug.LogWarning("No current tetromino found.");
            yield break;
        }

        Motion motion = tetromino.GetComponent<Motion>();
        if (motion == null)
        {
            Debug.LogError("Motion component not found on tetromino.");
            yield break;
        }

        while (true)
        {
            tetromino.transform.position += new Vector3(0, -1, 0);
            if (!motion.ValidMove())
            {
                tetromino.transform.position -= new Vector3(0, -1, 0);
                motion.AddToGrid();
                Mechanics.isPlaced = true;
                motion.enabled = false;
                break;
            }
            yield return new WaitForSeconds(0.1f); // 控制下落速度
        }

        // 在這裡可以添加一些後續處理，比如檢查行是否已滿，生成新的方塊等
    }
}
