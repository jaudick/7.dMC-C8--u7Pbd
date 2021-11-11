using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUnlocker
{
    public int currentLevelsUnlocked = 0;

    public LevelUnlocker()
    {
        currentLevelsUnlocked = 0;
    }

    public void SetLevelsUnlocked(int num)
    {
        currentLevelsUnlocked = num;
    }

    public int GetLevelsUnlocked()
    {
        return currentLevelsUnlocked;
    }
}
