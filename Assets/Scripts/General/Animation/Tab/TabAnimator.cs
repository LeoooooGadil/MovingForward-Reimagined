using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabAnimator : MonoBehaviour
{
	public RuntimeAnimatorController animatorController;
	public string boolParameterName;
	public bool isActive = true;

	private bool isCurrentTab = false;

	void Start()
	{
		if (animatorController == null)
			return;

		Animator animator = gameObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = animatorController;

		if (isCurrentTab)
		{
			animator.SetBool(boolParameterName, true);
		}
	}

	public void SelectTab()
	{
		if (!isActive) return;

		isCurrentTab = true;
		
		Animator animator = GetComponent<Animator>();

		if (animator == null) return;

		animator.SetBool(boolParameterName, true);
	}

	public void DeselectTab()
	{
		if (!isActive) return;

		isCurrentTab = false;

        Animator animator = GetComponent<Animator>();

		if (animator == null) return;

		animator.SetBool(boolParameterName, false);
	}
}
