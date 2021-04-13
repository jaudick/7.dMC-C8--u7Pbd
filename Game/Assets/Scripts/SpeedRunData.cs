using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeedRunData
{
    public float[] times = new float[11];

    public SpeedRunData()
    {
        //Debug.Log("<color=green>Creating new Empty SpeedRun Data object</color>");
        times = new float[11];
    }

    public void AssignLevelTime(int level, float time)
    {
        if (times[level] == 0)
        {
            times[level] = time;
        }
        else if (times[level] > time)
        {
            times[level] = time;
        }
    }
}
