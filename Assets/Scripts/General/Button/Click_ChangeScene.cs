using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_ChangeScene : MonoBehaviour
{
	public RuntimeAnimatorController animatorController;
	public string boolParameterName;
	public bool isActive = true;
	public string sceneName;

	void Start()
	{
		if (animatorController == null)
			return;

		Animator animator = GetComponent<Animator>();

		if (animator == null)
			animator = gameObject.AddComponent<Animator>();

		animator.runtimeAnimatorController = animatorController;
	}

	public void ChangeScene()
	{
		if(MenuManager.Instance.GetIsMenuOpen()) return;

		AudioManager.instance.PlaySFX("PopClick");
		LevelManager.instance.ChangeScene(sceneName, true, SceneTransitionMode.Slide, false);
	}

	private void OnMouseDown()
	{
		if (!isActive) return;

		Animator animator = GetComponent<Animator>();
		animator.SetBool(boolParameterName, true);
		ChangeScene();
	}

	private void OnMouseUp()
	{
		if (!isActive) return;

		Animator animator = GetComponent<Animator>();
		animator.SetBool(boolParameterName, false);
	}


}
