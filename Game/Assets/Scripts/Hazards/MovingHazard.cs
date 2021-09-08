using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public bool rotates = false;
    public bool rotateRight = true;
    public float rotationSpeed = 10f;
    public bool is4Way;
    public Transform beam;
    public bool goingTo1 = false;
    public bool goingTo2 = true;
    public bool goingTo3 = false;
    public bool goingTo4 = false;
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
        if (!doesNotMove)
        {
            if (is4Way)
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
                            beam.transform.position = Vector3.Lerp(point4.position, point1.position, percentageDone);
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    goingTo1 = false;
                    goingTo2 = true;
                    goingTo3 = false;
                    goingTo4 = false;
                }
                else if (goingTo2)
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
                    goingTo2 = false;
                    goingTo3 = true;
                    goingTo4 = false;
                }

                else if (goingTo3)
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
                            beam.transform.position = Vector3.Lerp(point2.position, point3.position, percentageDone);
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    goingTo1 = false;
                    goingTo2 = false;
                    goingTo3 = false;
                    goingTo4 = true;
                }

                else if (goingTo4)
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
                            beam.transform.position = Vector3.Lerp(point3.position, point4.position, percentageDone);
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    goingTo1 = true;
                    goingTo2 = false;
                    goingTo3 = false;
                    goingTo4 = false;
                }
            }

            else
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
                            beam.transform.position = Vector3.Lerp(point2.position, point1.position, percentageDone);
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    goingTo1 = false;
                    goingTo2 = true;
                }
                else if (goingTo2)
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
                    goingTo2 = false;
                    goingTo1 = true;
                }
            }
            StartCoroutine(LerpPosition());
        }
    }

    private void Update()
    {
        if (rotates && shiftable.localTime != 0)
        {
            if (rotateRight)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime) ;
            }
            else
            {
                transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
            }
        }
    }
}
