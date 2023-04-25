using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MovingForwardAbstractSceneTransitionScriptableObject : ScriptableObject
{
	public float AnimationTime = 0.25f;
	protected Image AnimatedObject;

	public abstract IEnumerator Enter(Canvas Parent);
	public abstract IEnumerator Exit(Canvas Parent);

	protected virtual Image CreateImage(Canvas Parent)
	{
        GameObject child = new GameObject("Transition Image");
        child.transform.SetParent(Parent.transform, false);
		child.AddComponent<Image>();
		child.GetComponent<Image>().raycastTarget = true;
        return child.GetComponent<Image>();
	}

}
