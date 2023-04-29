using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TicketManagerSaveData
{
    public Dictionary<string, TicketItemSave> ticketItems = new Dictionary<string, TicketItemSave>();

    public TicketManagerSaveData(TicketManagerSave ticketManagerSave)
    {
        ticketItems = ticketManagerSave.ticketItems;
    }
}
