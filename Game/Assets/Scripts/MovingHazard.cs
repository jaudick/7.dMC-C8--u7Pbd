using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform beam;
    public bool goingTo1 = false;
    public bool goingTo2 = true;
    public bool doesNotMove = true;
    public float timeBetweenPoints = 1f;
    HazardShiftable shiftable;
    float currentTime = 0;
    private void Start()
    {
        shiftable = GetComponentInChildren<HazardShiftable>();
        if(!doesNotMove)
            StartCoroutine(LerpPosition());
    }

    private IEnumerator LerpPosition()
    {
        float timeStarted = Time.time;
        float percentageDone = 0;
        if (goingTo1)
        {
            while (percentageDone < 1)
            {
                if (shiftable.localTime == 0)
                {
                    timeStarted += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    percentageDone = (Time.time - timeStarted) / timeBetweenPoints;
                    beam.transform.position = Vector3.Lerp(point1.position, point2.position, percentageDone);
                    yield return new WaitForEndOfFrame();
                }
            }
            goingTo1 = false;
            goingTo2 = true;
        }
        else if(goingTo2)
        {
            while (percentageDone < 1)
            {
                if (shiftable.localTime == 0)
                {
                    timeStarted += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    percentageDone = (Time.time - timeStarted) / timeBetweenPoints;
                    beam.transform.position = Vector3.Lerp(point2.position, point1.position, percentageDone);
                    yield return new WaitForEndOfFrame();
                }
            }
            goingTo2 = false;
            goingTo1 = true;
        }
        StartCoroutine(LerpPosition());
    }
}
