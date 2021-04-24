using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunCube : MonoBehaviour
{
    public GameObject[] walls;

    public void SetWallsFalse(GameObject wall)
    {
        for(int i = 0; i<walls.Length; i ++)
        {
            if(walls[i] == wall)
            {
                continue;
            }
            else
            {
                walls[i].tag = "MainCamera";
            }
        }
    }

    public void SetWallsTrue()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].tag = "WallRun";
        }
    }
}
