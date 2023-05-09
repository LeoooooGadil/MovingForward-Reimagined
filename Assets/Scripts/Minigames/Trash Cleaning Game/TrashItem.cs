using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashItem : MonoBehaviour
{
	public bool isBeingThrown = false;
	public ItemType itemType;
	public TakeMeOutGame game;
	private DragComponent dragComponent;

	void Start()
	{
		dragComponent = GetComponent<DragComponent>();
	}

	void Update()
	{
		// if (isBeingThrown)
		// {
		// 	dragComponent.isActive = false;
		// }
		// else
		// {
		// 	dragComponent.isActive = true;
		// }
	}
}

public enum ItemType
{
	Trash,
	NotTrash
}
