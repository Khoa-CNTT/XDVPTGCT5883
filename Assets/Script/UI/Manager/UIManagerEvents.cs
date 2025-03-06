using UnityEngine;
using UnityEngine.Events;

public class UIManagerEvents : MonoBehaviour
{
    public static UIManagerEvents Instance { get; private set; }

    public GameObject mainMenu;
    public GameObject settingMenu;

    public UnityEvent OnOpenMainMenu;
    public UnityEvent OnCloseMainMenu;
    public UnityEvent OnOpenSettingMenu;
    public UnityEvent OnCloseSettingMenu;

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
    }

    private void OnDisable()
    {
        // Main Menu
        OnOpenMainMenu.RemoveListener(OpenMainMenu);
        OnCloseMainMenu.RemoveListener(CloseMainMenu);

        // Setting Menu
        OnOpenSettingMenu.RemoveListener(OpenSettingMenu);
        OnCloseSettingMenu.RemoveListener(CloseSettingMenu);
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

}
