using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;

    public RuntimeAnimatorController animatorController;

    // Start is called before the first frame update
    void Start()
    {
        if (animatorController == null)
            return;

        Animator animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;

        button = GetComponent<Button>();
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		Animator animator = GetComponent<Animator>();
        animator.SetBool("isPushed", true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Animator animator = GetComponent<Animator>();
        animator.SetBool("isPushed", false);
	}
}
