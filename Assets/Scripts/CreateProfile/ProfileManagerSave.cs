using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManagerSave
{
    public string userId;
    public string username;
    public float money;

    public ProfileManagerSave()
    {
        string key = KeyGenerator.GetKey();
        userId = key;
        money = 0;
    }

    public ProfileManagerSave(ProfileManagerSaveData profileManagerSaveData)
    {
        userId = profileManagerSaveData.userId;
        username = profileManagerSaveData.username;
        money = profileManagerSaveData.money;
    }

    public void setUsername(string username)
    {
        this.username = username;
    }

    public void setMoney(float money)
    {
        this.money = money;
    }
}
