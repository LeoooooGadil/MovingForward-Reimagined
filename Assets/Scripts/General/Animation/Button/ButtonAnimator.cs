using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public RuntimeAnimatorController animatorController;
    public string boolParameterName;
    public bool isActive = true;
    private Button button;
    

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
        if(!isActive) return;

		Animator animator = GetComponent<Animator>();
        animator.SetBool(boolParameterName, true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
        if(!isActive) return;

		Animator animator = GetComponent<Animator>();
        animator.SetBool(boolParameterName, false);
	}
}
