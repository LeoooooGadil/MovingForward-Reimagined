using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour, IPointerClickHandler
{
	public GameObject checkmark;
	public Sprite defaultBackground;
	public Sprite completedBackground;
	public DailyTaskMenuManager dailyTaskMenuManager;

	public Text taskNameText;
	public Text taskCompensationText;
	public Text taskPointsText;
	public Button taskResetButton;

	public Task task;
	public int index;

	private Image image;
	private Animator animator;
	private float animSpeed = 15f;
	private bool isResetEnabled = true;

	void Start()
	{
		image = GetComponent<Image>();
		animator = GetComponent<Animator>();
		taskResetButton.onClick.AddListener(ResetTask);

		if (task == null) return;

		animator.SetBool("isCompleted", task.isCompleted);
	}

	void Update()
	{
		UpdateText();
		UpdateResetButton();
		UpdateBackground();
	}

	void UpdateText()
	{
		if (task == null) return;

		taskNameText.text = task.taskName;
		taskCompensationText.text = "+₱" + task.taskCompenstation.ToString("F0");
		taskPointsText.text = task.taskPoints.ToString() + "xp";
	}

	void UpdateResetButton()
	{
		int ticketCount = TicketAccess.GetTicketCount("DailyTask");
		if (ticketCount == 0) {
			isResetEnabled = false;
			taskResetButton.interactable = false;
		} else {
			isResetEnabled = true;
			taskResetButton.interactable = true;
		}
	}

	void UpdateBackground()
	{
		if (task.isCompleted)
		{
			image.sprite = completedBackground;
			taskResetButton.interactable = false;
			checkmark.SetActive(true);
			image.color = new Color(
				Mathf.Lerp(image.color.r, 0.75f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.g, 0.75f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.b, 0.75f, Time.deltaTime * animSpeed),
				1f);
		}
		else
		{
			image.sprite = defaultBackground;
			taskResetButton.interactable = true;
			checkmark.SetActive(false);
			image.color = new Color(
				Mathf.Lerp(image.color.r, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.g, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.b, 1f, Time.deltaTime * animSpeed),
				1f);
		}
	}

	public void ResetTask()
	{
		Debug.Log("Resetting task: " + task.taskName);
		AudioManager.instance.PlaySFX("AcceptClick");

		TicketAccess.RemoveOneFromTicket("DailyTask");
		int ticketCount = TicketAccess.GetTicketCount("DailyTask");
		if (ticketCount == 0) isResetEnabled = false;

		Task reloadedTask = dailyTaskMenuManager.ResetTask(index, task);
		task = reloadedTask;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (task == null) return;
		if (task.isCompleted)
		{
			AudioManager.instance.PlaySFX("EhhEhhClick");
			return;
		}

		AudioManager.instance.PlaySFX("AcceptClick");
		dailyTaskMenuManager.CompleteTask(task);
		task.isCompleted = true;
		animator.SetBool("isCompleted", task.isCompleted);
	}
}
