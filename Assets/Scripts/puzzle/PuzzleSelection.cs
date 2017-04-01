using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSelection : MonoBehaviour
{
    Vector2 m_goodOffset;
    Vector2 m_offset;
    Vector2 m_scale;

    public RawImage image;
    private Rect rect;


    // для упрощеного перемещения пазлов
    public Rect PuzzlePart
    {
        get
        {
            return rect;
        }
        set
        {
            rect = value;
            image.uvRect = rect;
        }
    }


    internal void CreatePuzzlePiece(int size)
    {
        transform.localScale = new Vector3(1.0f * transform.localScale.x / size, 1.0f * transform.localScale.z / size, 1);
    }

    internal void AssignImage(Vector2 scale, Vector2 offset)
    {
        m_goodOffset = offset;
        m_scale = scale;
        AssignImage(offset);
    }

    private void AssignImage(Vector2 offset)
    {
        m_offset = offset;
        image = GetComponent<RawImage>();
        rect = new Rect(offset.x, offset.y, m_scale.x, m_scale.y);
        image.uvRect = rect;
    }
}
