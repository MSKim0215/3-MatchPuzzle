using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot : MonoBehaviour
{
    private GameObject blockPrefab;

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
    }
}
