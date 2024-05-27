using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    BEAR, CAT, DEER, DOG, DUCK, FROG, MOUSE, PANDA, PIG, RABBIT, NONE
}

public class Slot : MonoBehaviour
{
    private GameObject blockPrefab;
    private SpriteRenderer blockRender;
    private BlockType blockType = BlockType.NONE;

    private void Start()
    {
        blockPrefab = Resources.Load<GameObject>("Prefabs/Block");

        CreateBlock();
    }

    private void CreateBlock()
    {
        if(blockPrefab == null)
        {
            Debug.LogWarning("블록 프리팹 없음");
            return;
        }

        GameObject spawnBlock = Instantiate(blockPrefab, transform);
        spawnBlock.transform.localPosition = Vector3.zero;

        blockRender = spawnBlock.GetComponent<SpriteRenderer>();

        blockType = (BlockType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(BlockType)).Length - 1);
        blockRender.sprite = GameManager.blockSprites[(int)blockType];
    }
}
