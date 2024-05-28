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
}