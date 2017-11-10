/*
 * Name : Lee Kong Hue 
 * ID : 1303181
 * Description :
 * 
 * this script is written to allow any unit to look left, right, and middle
 */
using UnityEngine;
using System.Collections;

public class LookAround : MonoBehaviour
{
    public enum LOOKSTATE { LEFT, MIDDLE, RIGHT}

    public LOOKSTATE lookState = LOOKSTATE.MIDDLE;
    public float speed = 0.2f;
    public float speedChangeView = 2;

    private float time = 0;
    public GameObject vision;

    /*
     * rotate vision from one point to another point according to look state such as middle, left, or right
     */
    void FixedUpdate()
    { 
        time += speed * Time.deltaTime;

        //Debug.Log("time : " + time);

        if (lookState == LOOKSTATE.MIDDLE)
        {
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, vision.transform.localEulerAngles.y, 0), Quaternion.Euler(0, 0, 0), time);
        }
        else if (lookState == LOOKSTATE.LEFT)
        {
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, vision.transform.localEulerAngles.y, 0), Quaternion.Euler(0, 45, 0), time);
        }
        else if (lookState == LOOKSTATE.RIGHT)
        {
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, vision.transform.localEulerAngles.y, 0), Quaternion.Euler(0, -45, 0), time);
        }
	}

    //function used to change the vision left and right continously 
    public void StartLookAround()
    {
        StartCoroutine("LookLeftRight");
    }

    //function to stop change of vision 
    public void StopLookAround()
    {
        StopCoroutine("LookLeftRight");
    }

    //function to make unit look at front view
    public void LookMiddle()
    {
        lookState = LOOKSTATE.MIDDLE;
    }

    //change view point from left to right or right to left and loop the whole process
    IEnumerator LookLeftRight()
    {
        ChangeLookDirection(LOOKSTATE.LEFT);

        while (true)
        {
            yield return new WaitForSeconds(speedChangeView);
            if (lookState == LOOKSTATE.LEFT)
            {
                ChangeLookDirection(LOOKSTATE.RIGHT);
            }
            else if (lookState == LOOKSTATE.RIGHT)
            {
                ChangeLookDirection(LOOKSTATE.LEFT);
            }
        }
    }

    //function to call once the view direction is change
    public void ChangeLookDirection(LOOKSTATE newLookState)
    {
        time = 0;
        lookState = newLookState;
    }
}
