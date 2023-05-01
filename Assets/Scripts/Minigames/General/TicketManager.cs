using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketManager : MonoBehaviour
{
	public static TicketManager instance;

	[HideInInspector]
	public TicketManagerSave ticketManagerSave;

	public string ticketName;
	public int maxTickets;

	private TicketItemSave ticketItemSave;

	private string saveFileName = "TicketManagerSave";

	public Text ticketText;

	private float timer = 0;
	private float maxTimer = 1f;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		LoadTicketManagerSave();
	}

	void Start()
	{
		GetTicketItemSave();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= maxTimer)
		{
			LoadTicketManagerSave();
			GetTicketItemSave();
			timer = 0;
		}

		ticketText.text = ticketItemSave.currentTickets.ToString() + "/" + ticketItemSave.maxTickets.ToString();
	}

	void LoadTicketManagerSave()
	{
		TicketManagerSaveData ticketManagerSaveData = SaveSystem.Load(saveFileName) as TicketManagerSaveData;

		if (ticketManagerSaveData != null)
		{
			this.ticketManagerSave = new TicketManagerSave(ticketManagerSaveData);
		}
		else
		{
			this.ticketManagerSave = new TicketManagerSave();
		}
	}

	void GetTicketItemSave()
	{
		TicketItemSave currentTicket = ticketManagerSave.GetTicketItem(ticketName);

		if (currentTicket != null)
		{
			ticketItemSave = currentTicket;
		}
		else
		{
			CreateTicket();
		}
	}

	void CreateTicket()
	{
		ticketManagerSave.AddTicketItem(ticketName, maxTickets, maxTickets);
		ticketItemSave = ticketManagerSave.GetTicketItem(ticketName);

		SaveTicketManager();
	}

	void SaveTicketManager()
	{
		TicketManagerSaveData ticketManagerSaveData = new TicketManagerSaveData(ticketManagerSave);
		SaveSystem.Save(saveFileName, ticketManagerSaveData);
	}
}
