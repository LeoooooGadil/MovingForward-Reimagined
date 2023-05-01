using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopUps
{
	public PopUpType Type;
	public GameObject PopUpPrefab;
}

public enum PopUpResult
{
	None,
	Yes,
	No,
	Ok,
}

public enum PopUpType
{
	None,
	YesNo,
	Ok,
	OkCancel,
	Info,
	Sequence,
}
