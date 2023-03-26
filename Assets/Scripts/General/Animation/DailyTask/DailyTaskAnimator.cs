using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskAnimator : MonoBehaviour
{
	public RuntimeAnimatorController animatorController;
	public string triggerParameterName;
	public bool isActive = true;

	private DailyTaskItem dailyTaskItem;
	private bool oldIsCompleted = false;

	void Start()
	{
		dailyTaskItem = GetComponentInParent<DailyTaskItem>();

		if (animatorController == null)
			return;

		Animator animator = gameObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = animatorController;
	}

	void Update()
	{
        if (dailyTaskItem.IsCompleted() != oldIsCompleted)
        {

            StartCoroutine(TriggerAnimation());
            oldIsCompleted = dailyTaskItem.IsCompleted();
        }
	}

	IEnumerator TriggerAnimation()
	{
		if (!isActive) yield break;

		Animator animator = GetComponent<Animator>();
		animator.SetTrigger(triggerParameterName);
	}
}
