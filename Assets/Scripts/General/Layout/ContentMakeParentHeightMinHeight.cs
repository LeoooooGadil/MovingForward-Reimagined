using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentMakeParentHeightMinHeight : MonoBehaviour
{
    private RectTransform parentRectTransform;
    private RectTransform contentRectTransform;
    private LayoutElement layoutElement;

    private void Awake()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        contentRectTransform = GetComponent<RectTransform>();
        layoutElement = GetComponent<LayoutElement>();

        layoutElement.minHeight = parentRectTransform.rect.height;
    }

    private void Update()
    {
        layoutElement.minHeight = parentRectTransform.rect.height;
    }
}
