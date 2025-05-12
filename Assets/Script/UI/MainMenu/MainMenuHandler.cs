using UnityEngine;

public class MainMenuHandler : MonoBehaviour, IBaseUI
{
    CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        UIManagerEvents.Instance?.OnOpenMainMenu.AddListener(Open);
        UIManagerEvents.Instance?.OnCloseMainMenu.AddListener(Close);
    }

    public void OnSettingButtonClick()
    {
        UIManagerEvents.Instance?.OnOpenSettingMenu?.Invoke();
    }

    public void OnStartButtonClick()
    {
        UIManagerEvents.Instance?.OnCloseMainMenu?.Invoke();
        UIManagerEvents.Instance?.OnOpenNewGameMenu?.Invoke();
    }

    public void Open()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Close()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
