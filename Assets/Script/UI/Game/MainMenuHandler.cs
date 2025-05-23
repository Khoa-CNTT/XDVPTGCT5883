using System.Collections;
using EasyTransition;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour, IBaseUI
{
    public TransitionSettings transitionSettings;
    CanvasGroup canvasGroup;

    void OnEnable()
    {
        UIManagerEvents.OnOpenMainMenu.AddListener(Open);
        UIManagerEvents.OnCloseMainMenu.AddListener(Close);
    }

    void OnDisable()
    {
        UIManagerEvents.OnOpenMainMenu.RemoveListener(Open);
        UIManagerEvents.OnCloseMainMenu.RemoveListener(Close);
    }

    IEnumerator Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        yield return new WaitUntil(() => UIManager.Instance != null);
        UIManager.Instance.StartGameButton.gameObject.GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        UIManager.Instance.SettingButton.gameObject.GetComponent<Button>().onClick.AddListener(OnSettingButtonClick);
    }

    public void OnSettingButtonClick()
    {
        UIManagerEvents.OnOpenSettingMenu?.Invoke();
    }

    public void OnStartButtonClick()
    {
        // UIManagerEvents.OnCloseMainMenu?.Invoke();
        // UIManagerEvents.OnOpenNewGameMenu?.Invoke();
        TransitionManager.Instance().Transition("UIMainMenuScrene", transitionSettings, 0.5f);
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
