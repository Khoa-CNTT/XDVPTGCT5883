using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameHandler : MonoBehaviour, IBaseUI
{
    public int newGameSceneIndex = 1;
    public float delayBeforeLoading = 0.5f;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        UIManagerEvents.Instance?.OnOpenNewGameMenu.AddListener(Open);
        UIManagerEvents.Instance?.OnCloseNewGameMenu.AddListener(Close);
    }

    public void OnBackButtonClick()
    {
        UIManagerEvents.Instance?.OnOpenMainMenu?.Invoke();
        UIManagerEvents.Instance?.OnCloseNewGameMenu?.Invoke();
    }

    public void OnSelectSlotButtonClick()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        if (!SceneManager.GetSceneByBuildIndex(newGameSceneIndex).isLoaded)
        {
            SceneManager.LoadSceneAsync(newGameSceneIndex, LoadSceneMode.Additive);
        }
        UIManagerEvents.Instance?.OnCloseNewGameMenu?.Invoke();
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
