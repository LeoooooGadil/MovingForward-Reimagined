using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManagerSave
{
	public Dictionary<string, TicketItemSave> ticketItems = new Dictionary<string, TicketItemSave>();

	public TicketManagerSave(TicketManagerSaveData ticketManagerSaveData)
	{
		ticketItems = ticketManagerSaveData.ticketItems;
	}

	public TicketManagerSave()
	{
        ticketItems = new Dictionary<string, TicketItemSave>();
	}

    public void RemoveTicketItem(string ticketName)
    {
        ticketItems.Remove(ticketName);
    }

	public void AddTicketItem(string ticketName, int currentTickets, int maxTickets)
	{
		TicketItemSave ticketItemSave = new TicketItemSave();
		ticketItemSave.ticketName = ticketName;
		ticketItemSave.currentTickets = currentTickets;
        ticketItemSave.maxTickets = maxTickets;
		ticketItems.Add(ticketName, ticketItemSave);
	}

    public void UpdateTicketItem(string ticketName, int currentTickets)
    {
        ticketItems[ticketName].currentTickets = currentTickets;
    }

    public TicketItemSave GetTicketItem(string ticketName)
    {
        if (!ticketItems.ContainsKey(ticketName))
        {
            return null;
        }

        return ticketItems[ticketName];
    }

	internal void ResetAllTickets()
	{
		ticketItems.Clear();
	}
}

[System.Serializable]
public class TicketItemSave
{
	public string ticketName;
	public int currentTickets = 0;
    public int maxTickets = 0;
}
