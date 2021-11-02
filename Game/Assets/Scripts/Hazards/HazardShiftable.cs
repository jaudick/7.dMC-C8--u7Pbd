using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardShiftable : MonoBehaviour
{
    public float localTime = 0;
    public int timeZone;
    public GameObject killbox;
    public GameObject cylinder;
    private bool checkMeOnStart = true;
    void Start()
    {
        localTime = timeZone == -1 ? 1 : TimeCore.times[timeZone];
        checkMeOnStart = true;
    }

    private void Update()
    {
        if (TimeCore.check || checkMeOnStart)
        {
            localTime = timeZone == -1 ? 1 : TimeCore.times[timeZone];
            if(localTime == 0)
            {
                cylinder.SetActive(true);
                killbox.SetActive(false);
            }
            else
            {
                cylinder.SetActive(false);
                killbox.SetActive(true);
            }
            if(checkMeOnStart) checkMeOnStart = false;
        }
    }
}
