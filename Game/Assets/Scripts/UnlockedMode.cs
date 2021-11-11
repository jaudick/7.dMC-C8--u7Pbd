using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedMode : MonoBehaviour
{
    public LevelMenu levelMenu;

    private void Start()
    {
        LevelMenu.unlockedMode = false;
        levelMenu.SetButtons(false);
    }

    public void SetUnlockedMode()
    {
        LevelMenu.unlockedMode = !LevelMenu.unlockedMode;
        levelMenu.SetButtons(LevelMenu.unlockedMode);
    }
}
