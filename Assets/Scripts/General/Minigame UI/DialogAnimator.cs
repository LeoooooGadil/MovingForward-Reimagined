using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
	public RuntimeAnimatorController animatorController;
	public string triggerParameterName;
	public bool isActive = true;

	void Start()
	{
		if (animatorController == null)
			return;

		Animator animator = gameObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = animatorController;
	}
	public void ExitDialog()
	{
		StartCoroutine(ExitDialogCoroutine());
	}

	IEnumerator ExitDialogCoroutine()
	{
		Animator animator = GetComponent<Animator>();
		if (animator == null) yield break;
		animator.SetTrigger(triggerParameterName);
		yield return null;
	}
}
