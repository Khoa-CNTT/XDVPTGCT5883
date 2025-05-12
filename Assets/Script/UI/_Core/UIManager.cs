using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        CloseAllScene();
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        UIManagerEvents.Instance?.OnOpenMainMenu?.Invoke();
    }

    public void CloseAllScene()
    {
        UIManagerEvents.Instance?.OnCloseMainMenu?.Invoke();
        UIManagerEvents.Instance?.OnCloseSettingMenu?.Invoke();
        UIManagerEvents.Instance?.OnCloseNewGameMenu?.Invoke();
    }
}
