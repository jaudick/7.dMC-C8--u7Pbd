using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridsTimeManager : MonoBehaviour
{
    public GameObject[] grids;
    private void Update()
    {
        if (TimeCore.check)
        {
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].SetActive(TimeCore.times[i] == 0);
            }
        }

    }
}
