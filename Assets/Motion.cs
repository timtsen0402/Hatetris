using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Mechanics;
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
                isPlaced = true;
                this.enabled = false;
            }
        }
    }
    bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int roundX = Mathf.RoundToInt(child.transform.position.x);
            int roundY = Mathf.RoundToInt(child.transform.position.y);
            if (roundX < 0 || roundX >= map_width || roundY < 0 || roundY >= map_height)
                return false;
            if (grid[roundX, roundY] != null)
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
    void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int roundX = Mathf.RoundToInt(child.transform.position.x);
            int roundY = Mathf.RoundToInt(child.transform.position.y);

            grid[roundX, roundY] = child;
        }
    }
    /*
    void Drop()
    {
        // 隨時間掉落
    }
    */
}
