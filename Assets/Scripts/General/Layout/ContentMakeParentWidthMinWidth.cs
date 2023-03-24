using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentMakeParentWidthMinWidth : MonoBehaviour
{
    // this script gets the width of the parent and sets the layout element min width to that value
    // this is used to make the content of a scroll view the same width as the scroll view

    private RectTransform parentRectTransform;
    private RectTransform contentRectTransform;
    private LayoutElement layoutElement;

    private void Awake()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        contentRectTransform = GetComponent<RectTransform>();
        layoutElement = GetComponent<LayoutElement>();

        layoutElement.minWidth = parentRectTransform.rect.width;
    }

    private void Update()
    {
        layoutElement.minWidth = parentRectTransform.rect.width;
    }
}
