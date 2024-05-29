using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BlockType
{
    BEAR, CAT, DEER, DOG, DUCK, FROG, MOUSE, PANDA, PIG, RABBIT, NONE
}

public class Block : MonoBehaviour
{
    // 드래그 좌표
    private Vector2 startPos, endPos;

    // 블록 위치 좌표
    private Vector2 originPos, destPos;

    private Block targetBlock;
    private GameManager board;

    public bool isMatched = false;

    private SpriteRenderer blockRender;
    private BlockType blockType = BlockType.NONE;

    public BlockType GetBlockType() => blockType;

    private void Start()
    {
        originPos = transform.parent.localPosition;

        board = FindObjectOfType<GameManager>();

        blockRender = GetComponent<SpriteRenderer>();

        blockType = (BlockType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(BlockType)).Length - 1);
        blockRender.sprite = GameManager.blockSprites[(int)blockType];
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

        targetBlock = board.gameSlots[(int)destPos.x, (int)destPos.y].GetComponentInChildren<Block>();
        targetBlock.transform.parent = transform.parent;
        transform.parent = board.gameSlots[(int)destPos.x, (int)destPos.y].transform;

        StartCoroutine(CMove());

        originPos = transform.parent.localPosition;
    }

    private IEnumerator CMove()
    {
        float timer = 0f;

        while(true)
        {
            yield return null;

            transform.localPosition = Vector2.Lerp(transform.localPosition, Vector2.zero, .4f);
            targetBlock.transform.localPosition = Vector2.Lerp(targetBlock.transform.localPosition, Vector2.zero, .4f);

            timer += Time.deltaTime;

            if(timer >= 1f)
            {
                transform.localPosition = Vector3.zero;
                targetBlock.transform.localPosition = Vector3.zero;

                yield break;
            }
        }
    }

    private void Update()
    {
        FindMatched();

        if(isMatched)
        {
            blockRender.color = Color.black;
        }
    }

    private void FindMatched()
    {
        if(0 < originPos.x && originPos.x < board.GetBoardSize() - 1)
        {
            Block leftBlock = board.gameSlots[(int)originPos.x - 1, (int)originPos.y].GetComponentInChildren<Block>();
            Block rightBlock = board.gameSlots[(int)originPos.x + 1, (int)originPos.y].GetComponentInChildren<Block>();

            if (leftBlock.GetBlockType() == this.GetBlockType() && rightBlock.GetBlockType() == this.GetBlockType())
            {
                leftBlock.isMatched = true;
                rightBlock.isMatched = true;
                isMatched = true;
            }
        }

        if(0 < originPos.y && originPos.y < board.GetBoardSize() - 1)
        {
            Block downBlock = board.gameSlots[(int)originPos.x, (int)originPos.y - 1].GetComponentInChildren<Block>();
            Block upBlock = board.gameSlots[(int)originPos.x, (int)originPos.y + 1].GetComponentInChildren<Block>();

            if (downBlock.GetBlockType() == this.GetBlockType() && upBlock.GetBlockType() == this.GetBlockType())
            {
                downBlock.isMatched = true;
                upBlock.isMatched = true;
                isMatched = true;
            }
        }
    }
}
