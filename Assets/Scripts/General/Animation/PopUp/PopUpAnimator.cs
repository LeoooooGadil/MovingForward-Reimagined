using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAnimator : MonoBehaviour
{
	public RuntimeAnimatorController animatorController;

	void Start()
	{
        if (animatorController == null)
            return;

        Animator animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
	}
}
