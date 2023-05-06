using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoresSaveData
{
    public List<Chore> chores;
    public List<Chore> completedChores;

    public DateTime date;

    public ChoresSaveData(ChoresSave _choresSave)
    {
        chores = new List<Chore>();
        completedChores = new List<Chore>();

        foreach (Chore chore in _choresSave.chores)
        {
            chores.Add(chore);
        }

        foreach (Chore chore in _choresSave.completedChores)
        {
            completedChores.Add(chore);
        }

        date = _choresSave.date;
    }
}
