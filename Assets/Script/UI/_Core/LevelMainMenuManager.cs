using System.Collections.Generic;
using EasyTransition;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMainMenuManager : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    public Button[] buttons;

    void Awake()
    {
        // PlayerPrefs.DeleteKey("UnlockedLevel");

        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelid)
    {
        string levelName = "Level_" + levelid;

        if (!string.IsNullOrEmpty(levelName) && transitionSettings != null)
        {
            TransitionManager.Instance().Transition(levelName, transitionSettings, 0.5f);
        }
    }
}
