using UnityEngine;
using UnityEngine.Events;

public class UIManagerEvents : MonoBehaviour
{
    public static UIManagerEvents Instance { get; private set; }

    //UI Elements
    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject newGameMenu;

    //Unity Events

    // Main Menu
    public UnityEvent OnOpenMainMenu;
    public UnityEvent OnCloseMainMenu;

    // Setting Menu
    public UnityEvent OnOpenSettingMenu;
    public UnityEvent OnCloseSettingMenu;

    // newGame Menu
    public UnityEvent OnOpenNewGameMenu;
    public UnityEvent OnCloseNewGameMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        // Main Menu
        OnOpenMainMenu.AddListener(OpenMainMenu);
        OnCloseMainMenu.AddListener(CloseMainMenu);

        // Setting Menu
        OnOpenSettingMenu.AddListener(OpenSettingMenu);
        OnCloseSettingMenu.AddListener(CloseSettingMenu);

        // newGame Menu
        OnOpenNewGameMenu.AddListener(OpenNewGameMenu);
        OnCloseNewGameMenu.AddListener(CloseNewGameMenu);
    }

    private void OnDisable()
    {
        // Main Menu
        OnOpenMainMenu.RemoveListener(OpenMainMenu);
        OnCloseMainMenu.RemoveListener(CloseMainMenu);

        // Setting Menu
        OnOpenSettingMenu.RemoveListener(OpenSettingMenu);
        OnCloseSettingMenu.RemoveListener(CloseSettingMenu);

        // newGame Menu
        OnOpenNewGameMenu.RemoveListener(OpenNewGameMenu);
        OnCloseNewGameMenu.RemoveListener(CloseNewGameMenu);
    }

    // ==================== MAIN MENU ====================
    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void CloseMainMenu()
    {
        mainMenu.SetActive(false);
    }

    // ==================== SETTING MENU ====================
    public void OpenSettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void CloseSettingMenu()
    {
        settingMenu.SetActive(false);
    }

    // ==================== NEW GAME MENU ====================
    public void OpenNewGameMenu()
    {
        newGameMenu.SetActive(true);
    }

    public void CloseNewGameMenu()
    {
        newGameMenu.SetActive(false);
    }
}
