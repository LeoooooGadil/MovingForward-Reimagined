using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButtonLifeCycle : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public GameObject playText;
	public GameObject timerText;
	public bool isReadyToPlay = false;
	public string lifeCycle;

	private Button button;
	private ButtonAnimator buttonAnimator;
	private float timer = 0;
	private float holdTime = 1.5f;
	private bool isHolding = false;

	private void Start()
	{
		CheckLifeCycle();

		playText.SetActive(true);
		timerText.SetActive(false);
	}

	void CheckLifeCycle()
	{
		if (LifeCycleManager.instance == null) return;

		LifeCycleItem thisLifeCycle = LifeCycleManager.instance.GetLifeCycleItem(lifeCycle);

		isReadyToPlay = false;

		if (thisLifeCycle == null)
		{
			isReadyToPlay = true;
			return;
		}

		if (thisLifeCycle.Envoke)
		{
			LifeCycleManager.instance.EnvokeLifeCycleItem(lifeCycle);

			// make the game active
			isReadyToPlay = true;
			TicketAccess.RemoveTicket(lifeCycle);
		}
	}

	private void Update()
	{

		button = GetComponent<Button>();
		buttonAnimator = GetComponent<ButtonAnimator>();

		if (isReadyToPlay)
		{
			playText.SetActive(true);
			timerText.SetActive(false);
			button.interactable = true;
			buttonAnimator.isActive = true;
		}
		else
		{
			playText.SetActive(false);
			timerText.SetActive(true);
			button.interactable = false;
			buttonAnimator.isActive = false;
			CheckLifeCycle();
		}

		if (isHolding)
		{
			timer += Time.deltaTime;

			if (timer >= holdTime)
			{
				AudioManager.instance.PlaySFX("EhhEhhClick");
				OnScreenNotificationManager.instance.CreateNotification("Dev Mode: Hello There!", OnScreenNotificationType.Warning);
				OnScreenNotificationManager.instance.CreateNotification("Timer Bypassed", OnScreenNotificationType.Warning);
				LifeCycleManager.instance.BypassLifeCycleItem(lifeCycle);
				TicketAccess.RemoveTicket(lifeCycle);
				timer = 0;
				isHolding = false;
			}
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (isReadyToPlay) return;

		isHolding = false;
		timer = 0;

		AudioManager.instance.PlaySFX("EhhEhhClick");
		OnScreenNotificationManager.instance.CreateNotification("You can't play this yet.", OnScreenNotificationType.Warning);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isReadyToPlay) return;

		isHolding = true;
	}
}
