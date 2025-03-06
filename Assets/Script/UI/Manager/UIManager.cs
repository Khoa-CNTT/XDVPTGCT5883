using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public UIManagerEvents uiManagerEvents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        CloseAllScene();
    }

    public void Start()
    {
        ShowMainMenu();
    }

    public void CloseAllScene()
    {
        uiManagerEvents.OnCloseMainMenu?.Invoke();
        uiManagerEvents.OnCloseSettingMenu?.Invoke();
    }

    public void ShowMainMenu()
    {
        uiManagerEvents.OnOpenMainMenu?.Invoke();
    }
}
