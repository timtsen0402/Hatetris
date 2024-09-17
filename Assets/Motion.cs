using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class Motion : MonoBehaviour
{
    public Vector3 rotationPoint;

    float moveDelay = .2f;
    private float moveTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > moveTime)
        {
            Move();
            Rotate();
        }

    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.position += new Vector3(-1, 0, 0);
            moveTime = Time.time + moveDelay;
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            moveTime = Time.time + moveDelay;
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);
            moveTime = Time.time + moveDelay;
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                Mechanics.isPlaced = true;
                this.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (; ; )
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGrid();
                    Mechanics.isPlaced = true;
                    this.enabled = false;
                    break;
                }
            }

        }
    }
    public bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int roundX = (int)Mathf.Floor(child.transform.position.x);
            int roundY = (int)Mathf.Floor(child.transform.position.y);

            // 首先检查 X 坐标
            if (roundX < 0 || roundX >= Mechanics.Instance.MapWidth)
                return false;

            // 然后检查 Y 坐标
            if (roundY < 0 || roundY >= Mechanics.Instance.MapHeight)
                return false;

            // 最后检查网格占用情况
            if (Mechanics.grid[roundX, roundY] != null)
                return false;
        }

        return true;
    }
    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
    }
    public void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int roundX = (int)Mathf.Floor(child.transform.position.x);
            int roundY = (int)Mathf.Floor(child.transform.position.y);

            Mechanics.grid[roundX, roundY] = child;
        }
    }
    /*
    void Drop()
    {
        // 隨時間掉落
    }
    */
}
