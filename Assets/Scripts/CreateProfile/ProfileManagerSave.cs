using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManagerSave
{
    public string userId;
    public string username;

    public ProfileManagerSave()
    {
        string key = KeyGenerator.GetKey();
        userId = key;
    }

    public ProfileManagerSave(ProfileManagerSaveData profileManagerSaveData)
    {
        userId = profileManagerSaveData.userId;
        username = profileManagerSaveData.username;
    }

    public void setUsername(string username)
    {
        this.username = username;
    }
}
