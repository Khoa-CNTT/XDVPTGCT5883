using UnityEngine;
using UnityEngine.Events;

public class UIManagerEvents : MonoBehaviour
{
    public static UIManagerEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Main Menu
    [HideInInspector] public UnityEvent OnOpenMainMenu = new UnityEvent();
    [HideInInspector] public UnityEvent OnCloseMainMenu = new UnityEvent();

    // Setting Menu
    [HideInInspector] public UnityEvent OnOpenSettingMenu = new UnityEvent();
    [HideInInspector] public UnityEvent OnCloseSettingMenu = new UnityEvent();

    // newGame Menu
    [HideInInspector] public UnityEvent OnOpenNewGameMenu = new UnityEvent();
    [HideInInspector] public UnityEvent OnCloseNewGameMenu = new UnityEvent();
}
