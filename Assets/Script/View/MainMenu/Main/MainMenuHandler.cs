using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public UIManagerEvents uiManagerEvents;

    public void Start()
    {
        if (uiManagerEvents == null)
        {
            Debug.LogError("UIManagerEvents is not set in Main Menu");
            return;
        }
    }

    public void OnSettingButtonClick()
    {
        uiManagerEvents.OnOpenSettingMenu?.Invoke();
    }

    public void OnStartButtonClick()
    {
        uiManagerEvents.OnCloseMainMenu?.Invoke();
        uiManagerEvents.OnOpenNewGameMenu?.Invoke();
    }
}
