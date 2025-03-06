using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public UIManagerEvents uiManagerEvents;

    public void OnSettingButtonClick()
    {
        uiManagerEvents.OnOpenSettingMenu?.Invoke();
    }
}
