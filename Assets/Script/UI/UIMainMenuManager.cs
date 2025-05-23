using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;

    public void Open()
    {
        settingMenu.SetActive(true);
    }

    public void Close()
    {
        settingMenu.SetActive(false);
    }
}
