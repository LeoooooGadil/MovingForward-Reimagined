using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileManagerSaveData
{
    public string userId;
    public string username;
    public string age;
    public float money;

    public ProfileManagerSaveData(ProfileManagerSave profileManagerSave)
    {
        userId = profileManagerSave.userId;
        username = profileManagerSave.username;
        age = profileManagerSave.age;
        money = profileManagerSave.money;
    }
}
