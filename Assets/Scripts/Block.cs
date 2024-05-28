using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    private Vector2 startPos, endPos;

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
    }
}
