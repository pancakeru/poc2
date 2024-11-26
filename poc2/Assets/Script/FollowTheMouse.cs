using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheMouse : MonoBehaviour
{
    public RectTransform target;
    public Canvas canvas;

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint);

        target.anchoredPosition = localPoint;
    }
}
