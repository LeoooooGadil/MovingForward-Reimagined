using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeStamp
{
    public static int GetTimeStamp()
    {
        return (int)System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds;
    }
}
