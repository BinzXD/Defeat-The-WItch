using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditSet : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    private float[] pos;
    private int posisi = 0;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isDragging = false;

    void Start()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            float swipeValue = touchEndPos.x - touchStartPos.x;
            if (Mathf.Abs(swipeValue) > 50 && isDragging)
            {
                if (swipeValue > 0)
                    SwipeRight();
                else if (swipeValue < 0)
                    SwipeLeft();
            }
            isDragging = false;
        }

        if (isDragging)
        {
            touchEndPos = Input.mousePosition;
        }

        // Lerp scrollbar position
        if (!isDragging)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + 0.05f && scroll_pos > pos[i] - 0.05f)
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                    posisi = i;
                }
            }
        }
    }

    public void SwipeRight()
    {
        if (posisi > 0)
        {
            posisi--;
            scroll_pos = pos[posisi];
        }
    }

    public void SwipeLeft()
    {
        if (posisi < pos.Length - 1)
        {
            posisi++;
            scroll_pos = pos[posisi];
        }
    }
}
