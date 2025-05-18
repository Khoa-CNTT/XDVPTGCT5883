using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using EasyTransition;

public class NewGameHandler : MonoBehaviour, IBaseUI
{
    public int newGameSceneIndex = 1;
    public float delayBeforeLoading = 0.5f;
    private CanvasGroup canvasGroup;
    public TransitionSettings transitionSettings;
    public float delay = 0.5f;

    void OnEnable()
    {
        UIManagerEvents.OnOpenNewGameMenu.AddListener(Open);
        UIManagerEvents.OnCloseNewGameMenu.AddListener(Close);
    }

    void OnDisable()
    {
        UIManagerEvents.OnOpenNewGameMenu.RemoveListener(Open);
        UIManagerEvents.OnCloseNewGameMenu.RemoveListener(Close);
    }

    IEnumerator Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        yield return new WaitUntil(() => UIManager.Instance != null);
        foreach (Button button in UIManager.Instance.SaveSlotsButton)
        {
            button.gameObject.GetComponent<Button>().onClick.AddListener(OnSelectSlotButtonClick);
        }
        UIManager.Instance.OnNewGameBackButton.gameObject.GetComponent<Button>().onClick.AddListener(OnBackButtonClick);
    }

    public void OnBackButtonClick()
    {
        UIManagerEvents.OnOpenMainMenu?.Invoke();
        UIManagerEvents.OnCloseNewGameMenu?.Invoke();
    }

    public void OnSelectSlotButtonClick()
    {
        TransitionManager.Instance().Transition("UIMainMenuScrene", transitionSettings, delay);
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
