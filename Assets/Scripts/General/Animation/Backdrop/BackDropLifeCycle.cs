using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDropLifeCycle : MonoBehaviour
{
	private Animator animator;

	public void ExitAnimation()
	{
		animator = GetComponent<Animator>();
		animator.SetTrigger("isExit");
	}
}
