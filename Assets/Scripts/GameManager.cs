using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 임시 블록 스프라이트 배열
    public static Sprite[] blockSprites;

    private GameObject slotPrefab;

    private const int BOARD_SIZE = 10;

    public int GetBoardSize() => BOARD_SIZE;

    public Slot[,] gameSlots;

    [SerializeField] private Block grabBlock;
    [SerializeField] private Block targetBlock;

    private void Start()
    {
        blockSprites = Resources.LoadAll<Sprite>("Sprites/Block");
        slotPrefab = Resources.Load<GameObject>("Prefabs/Slot");

        gameSlots = new Slot[BOARD_SIZE, BOARD_SIZE];

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
                Vector2 spawnPos = new Vector2(i, j);
                GameObject spawnObj = Instantiate(slotPrefab, transform);
                spawnObj.transform.localPosition = spawnPos;

                gameSlots[i,j] = spawnObj.GetComponent<Slot>();
            }
        }
    }
}