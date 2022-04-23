using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipPosition : MonoBehaviour
{
    public RectTransform canvas;
    public RectTransform rectTransformPanel;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    Vector2 delta = new Vector2(2, 2);
    void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvas.localScale.x;
        if (anchoredPosition.x + rectTransformPanel.rect.width > canvas.rect.width)
        {
            anchoredPosition.x = canvas.rect.width - rectTransformPanel.rect.width;
        }
        if (anchoredPosition.y + rectTransformPanel.rect.height > canvas.rect.height)
        {
            anchoredPosition.y = canvas.rect.height - rectTransformPanel.rect.height;
        }
        rectTransformPanel.anchoredPosition = anchoredPosition + delta;
    }
}
