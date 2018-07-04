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

    public delegate void OnCardFallDelegate();
    public static event OnCardFallDelegate OnCardFall;

    public static void OnCardFallingNow()
    {
        if (OnCardFall != null)
        {
            OnCardFall();
        }
    }


}
