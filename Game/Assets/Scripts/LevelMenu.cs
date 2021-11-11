using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public int currentLevelsUnlocked = 0;
    public Button[] buttons;
    public static bool unlockedMode = false;

    private void Awake()
    {
        SetButtons();
    }

    public void SetButtons(bool unlocked = false)
    {
        currentLevelsUnlocked = LevelsUnlockedData.LoadLevelData().GetLevelsUnlocked();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = unlocked && i!=11 ? true : i <= currentLevelsUnlocked;
        }
    }
}
