using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TicketAccess
{
	private static string saveFileName = "TicketManagerSave";

	static TicketManagerSave LoadTicketManagerSave()
	{
		TicketManagerSaveData ticketManagerSaveData = SaveSystem.Load(saveFileName) as TicketManagerSaveData;

		if (ticketManagerSaveData != null)
		{
			return new TicketManagerSave(ticketManagerSaveData);
		}
		else
		{
			return new TicketManagerSave();
		}
	}

	public static void ResetAllTickts()
	{
		TicketManagerSave ticketManagerSave = LoadTicketManagerSave();

		ticketManagerSave.ResetAllTickets();

		SaveTicketManager(ticketManagerSave);
	}

	public static int GetTicketCount(string ticketName)
	{
		TicketManagerSave ticketManagerSave = LoadTicketManagerSave();

		TicketItemSave ticketItemSave = ticketManagerSave.GetTicketItem(ticketName);

		if (ticketItemSave != null)
		{
			return ticketItemSave.currentTickets;
		}
		else
		{
			return 0;
		}
	}

	public static void CreateTicket(string ticketName, int maxTickets)
	{
		TicketManagerSave ticketManagerSave = LoadTicketManagerSave();

		ticketManagerSave.AddTicketItem(ticketName, maxTickets, maxTickets);

		SaveTicketManager(ticketManagerSave);
	}

	public static void RemoveOneFromTicket(string ticketName)
	{
		TicketManagerSave ticketManagerSave = LoadTicketManagerSave();

		TicketItemSave ticketItemSave = ticketManagerSave.GetTicketItem(ticketName);

		if (ticketItemSave != null)
		{
			if (ticketItemSave.currentTickets > 0)
			{
				ticketItemSave.currentTickets -= 1;
				Debug.Log("Removed one piece of ticket from " + ticketName);
			}
		}

		SaveTicketManager(ticketManagerSave);
	}

	public static void RemoveTicket(string ticketName)
	{
		TicketManagerSave ticketManagerSave = LoadTicketManagerSave();

		TicketItemSave ticketItemSave = ticketManagerSave.GetTicketItem(ticketName);

		if (ticketItemSave != null)
		{
			ticketManagerSave.RemoveTicketItem(ticketName);
		}
		else
		{
			Debug.Log("Ticket " + ticketName + " does not exist.");
		}

		SaveTicketManager(ticketManagerSave);
	}

	public static void ResetTicket(string ticketName)
	{
		TicketManagerSave ticketManagerSave = LoadTicketManagerSave();

		TicketItemSave ticketItemSave = ticketManagerSave.GetTicketItem(ticketName);

		if (ticketItemSave != null)
		{
			ticketItemSave.currentTickets = ticketItemSave.maxTickets;
		}
		else
		{
			Debug.Log("Ticket " + ticketName + " does not exist.");
		}

		SaveTicketManager(ticketManagerSave);
	}

	static void SaveTicketManager(TicketManagerSave ticketManagerSave)
	{
		TicketManagerSaveData ticketManagerSaveData = new TicketManagerSaveData(ticketManagerSave);
		SaveSystem.Save(saveFileName, ticketManagerSaveData);
	}
}
