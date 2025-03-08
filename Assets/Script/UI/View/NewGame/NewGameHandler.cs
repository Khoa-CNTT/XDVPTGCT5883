using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameHandler : MonoBehaviour
{
    public UIManagerEvents uiManagerEvents;
    public int newGameSceneIndex = 1;
    public float delayBeforeLoading = 0.5f;

    private void Start()
    {
        if (uiManagerEvents == null)
        {
            Debug.LogError("UIManagerEvents is not set in New Game");
            return;
        }
    }

    public void OnBackButtonClick()
    {
        uiManagerEvents.OnOpenMainMenu?.Invoke();
        uiManagerEvents.OnCloseNewGameMenu?.Invoke();
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
        uiManagerEvents.OnCloseNewGameMenu?.Invoke();
    }
}
