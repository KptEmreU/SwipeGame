using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EEvents
{
    public delegate void OnXChangeDelegate(float x);
    public static event OnXChangeDelegate OnXChange;

    public static void OnXChangeNow(float x)
    {
        if (OnXChange != null)
        {
            OnXChange(x);
        }
    }
    

}
