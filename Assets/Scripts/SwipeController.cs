using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {

    Vector2 startTouchPos;
    Vector2 movingTouchPos;
    float touchDistance = 0;

    private void OnEnable()
    {
        EEvents.OnCardFall += Reset;
    }

    private void OnDisable()
    {
        EEvents.OnCardFall -= Reset;
    }



    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            TouchPhase touchFirstPhase = Input.touches[0].phase;

            if (touchFirstPhase == TouchPhase.Began)
            {
                startTouchPos = Input.touches[0].position;
            }
            else if (touchFirstPhase == TouchPhase.Ended || touchFirstPhase == TouchPhase.Canceled)
            {
                Reset();
            }
            else if (touchFirstPhase == TouchPhase.Moved)
            {
                movingTouchPos = Input.touches[0].position;
            }

            CalculateTouchDistance();

            if (touchDistance > 1)
                BroadCastTouchDistance(touchDistance);
        } 
#endif

#if !UNITY_ANDROID
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        if (Input.GetMouseButton(0))
        {
            if(startTouchPos != Vector2.zero) //this is required to not fall the cards after reset
                movingTouchPos = Input.mousePosition;
        }

        CalculateTouchDistance();

        BroadCastTouchDistance(touchDistance);
#endif

    }

    private void Reset()
    {
        Debug.Log("ResetCome");
        movingTouchPos = startTouchPos = Vector2.zero;
        touchDistance = 0;
        Debug.Log(movingTouchPos + " " + startTouchPos + " " + touchDistance);
    }

    void CalculateTouchDistance()
    {
        touchDistance = startTouchPos.x - movingTouchPos.x;
    }

    void BroadCastTouchDistance(float x) // TODO: NOT SURE IF THIS IS REALLY NEEDED JUST TAKES LONGER THIS WAY
    {
        EEvents.OnXChangeNow(x);
    }
}
