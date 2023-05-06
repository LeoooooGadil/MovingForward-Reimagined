using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashItem : MonoBehaviour
{
	public bool isBeingThrown = false;
	private DragComponent dragComponent;

    void Start()
    {
        dragComponent = GetComponent<DragComponent>();
    }

	void Update()
	{
		if (isBeingThrown)
		{
			dragComponent.isActve = false;
		}
		else
		{
			dragComponent.isActve = true;
		}
	}
}
