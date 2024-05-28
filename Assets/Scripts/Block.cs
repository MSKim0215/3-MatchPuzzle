using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    // 드래그 좌표
    private Vector2 startPos, endPos;

    // 블록 위치 좌표
    private Vector2 originPos, destPos;

    private Block targetBlock;
    private GameManager board;

    private void Start()
    {
        originPos = transform.parent.localPosition;

        board = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CalculateAngle();
    }

    private void CalculateAngle()
    {
        Vector2 dragDir = endPos - startPos;
        float absXDiff = Mathf.Abs(dragDir.x);
        float absYDiff = Mathf.Abs(dragDir.y);

        if (absXDiff > absYDiff)
        {
            if (dragDir.x > 0)
            {
                destPos = originPos + Vector2.right;

                if (destPos.x >= board.GetBoardSize()) return;
            }
            else
            {
                destPos = originPos + Vector2.left;

                if (destPos.x < 0) return;
            }
        }
        else
        {
            if (dragDir.y > 0)
            {
                destPos = originPos + Vector2.up;

                if (destPos.y >= board.GetBoardSize()) return;
            }
            else
            {
                destPos = originPos + Vector2.down;

                if (destPos.y < 0) return;
            }
        }

        transform.parent = board.gameSlots[(int)destPos.x, (int)destPos.y].transform;
        transform.localPosition = Vector3.zero;
        originPos = transform.parent.localPosition;
    }

    private void Move()
    {
        
    }
}
