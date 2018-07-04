using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour {

    private void OnEnable()
    {
        EEvents.OnXChange += MoveCard;
    }

    private void OnDisable()
    {
        EEvents.OnXChange -= MoveCard;
    }

    public float maxDegreeToFall = 60;

    Quaternion destinationRot;

    IEnumerator ienum; //Required to stop the IEnumerator reliable and check if its working

    float maxAllowedDistanceToFall = -5;
    Vector3 fallPositionTarget;
    bool isFalling = false;

    private void Start()
    {
        fallPositionTarget = new Vector3(0, maxAllowedDistanceToFall* 3 , 0); //Init fall position
    }

    void MoveCard(float x)
    {
        if (!isFalling) // when falling don't come here and can not swipe
        {
            if (x != 0)
            {

                if (x > maxDegreeToFall || x < -maxDegreeToFall) // while swiping if this degrees are passed fall
                {
                    StartCoroutine(FallC());
                }

                SlideMove(x);
            }

            if (x == 0)
            {
                WhenSlideMoveIsStoppedComeBack();
            }
        }
    }

    private void WhenSlideMoveIsStoppedComeBack()
    {
        if (ienum == null)
        {
            ienum = MoveBackTheCardC();
            StartCoroutine(ienum);
        }
    }

    private void SlideMove(float x)
    {
        transform.rotation = Quaternion.Euler(0, 0, x);
        transform.localScale = Vector3.one * ((Mathf.Abs(x)/ (maxDegreeToFall * 2)) + 1); // Scale is increased to max (1.5,1.5,1.5) at the end;
    }

    IEnumerator FallC()
    {
        isFalling = true; // so you can't swipe on top

        while (transform.position.y > maxAllowedDistanceToFall) //-5 is out of camera point
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, fallPositionTarget, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 180), 0.1f);
        }

        ResetTheCard();
    }

    void ResetTheCard()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        isFalling = false; // can swipe again
    }
    

    IEnumerator MoveBackTheCardC()
    {
        Vector3 rotationV3 = transform.rotation.eulerAngles; // TODO: THIS IS KINDA WEIRD LOOK INTO THIS

        while ((rotationV3.z < -0.1f || rotationV3.z > 0.1f)  )
        {
            yield return null;
                        
            destinationRot = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);

            transform.rotation = destinationRot;
        }

        destinationRot = Quaternion.identity; 
        
        ienum = null;
    }
}
