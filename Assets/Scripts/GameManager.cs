using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    // 임시 블록 스프라이트 배열
    public static Sprite[] blockSprites;

    private GameObject slotPrefab;

    private const int BOARD_SIZE = 10;

    [SerializeField] private List<Slot> gameSlots = new List<Slot>();

    [SerializeField] private Block grabBlock;
    [SerializeField] private Block targetBlock;

    private Vector2 startPos, endPos;

    private void Start()
    {
        blockSprites = Resources.LoadAll<Sprite>("Sprites/Block");

        slotPrefab = Resources.Load<GameObject>("Prefabs/Slot");

        CreateGameBoard();
    }

    private void CreateGameBoard()
    {
        if(slotPrefab == null)
        {
            Debug.LogWarning("프리팹 없음");
            return;
        }

        for(int i = 0; i < BOARD_SIZE; i++)
        {
            for(int j = 0; j < BOARD_SIZE; j++)
            {
                Vector2 spawnPos = new Vector2((float)j / 2 + 0.5f, (float)i / 2 + 0.5f);
                GameObject spawnObj = Instantiate(slotPrefab, transform);
                spawnObj.transform.localPosition = spawnPos;

                gameSlots.Add(spawnObj.GetComponent<Slot>());  
            }
        }
    }

    private Vector2 dragStartPos;

    private void Update()
    {
        if(grabBlock == null && Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null)
            {
                grabBlock = hit.collider.GetComponent<Block>();
                startPos = grabBlock.transform.position;
                dragStartPos = pos;
            }
        }

        if(grabBlock != null)
        {
            Vector2 currPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dragDistance = Vector2.Distance(currPos, dragStartPos);

            if(dragDistance > 0.3f)
            {
                RaycastHit2D hit = Physics2D.Raycast(currPos, Vector2.zero, 0f);
                if(hit.collider != null)
                {
                    targetBlock = hit.collider.GetComponent<Block>();

                    Vector2 tempPos = targetBlock.transform.position;

                    Vector2 dragDir = currPos - dragStartPos;
                    float absXDiff = Mathf.Abs(dragDir.x);
                    float absYDiff = Mathf.Abs(dragDir.y);

                    if (absXDiff > absYDiff)
                    {
                        if (dragDir.x > 0)
                        {
                            Debug.Log("오른쪽 드래그");
                        }
                        else
                        {
                            Debug.Log("왼쪽 드래그");
                        }
                    }
                    else
                    {
                        if (dragDir.y > 0)
                        {
                            Debug.Log("상단 드래그");
                        }
                        else
                        {
                            Debug.Log("하단 드래그");
                        }
                    }

                    targetBlock.transform.position = grabBlock.transform.position;
                    grabBlock.transform.position = tempPos;

                    grabBlock = null;
                    targetBlock = null;

                    return;
                }
            }
        }

        if(grabBlock != null && Input.GetMouseButtonUp(0))
        {
            grabBlock = null;
        }
    }
}