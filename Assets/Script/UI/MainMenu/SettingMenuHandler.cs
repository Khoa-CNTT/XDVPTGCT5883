using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuHandler : MonoBehaviour, IBaseUI
{
    public Button audioButton;
    public Button musicButton;
    private CanvasGroup canvasGroup;

    private bool isDimmedAudio = false;
    private bool isDimmedMusic = false;

    void OnEnable()
    {
        UIManagerEvents.OnOpenSettingMenu.AddListener(Open);
        UIManagerEvents.OnCloseSettingMenu.AddListener(Close);
    }

    void OnDisable()
    {
        UIManagerEvents.OnOpenSettingMenu.RemoveListener(Open);
        UIManagerEvents.OnCloseSettingMenu.RemoveListener(Close);
    }

    IEnumerator Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        yield return new WaitUntil(() => UIManager.Instance != null);
        UIManager.Instance.AudioButton.gameObject.GetComponent<Button>().onClick.AddListener(OnAudioButtonClick);
        UIManager.Instance.MusicButton.gameObject.GetComponent<Button>().onClick.AddListener(OnMusicButtonClick);
        UIManager.Instance.OnSettingBackButton.gameObject.GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
    }

    public void OnExitButtonClick()
    {
        UIManagerEvents.OnCloseSettingMenu?.Invoke();
    }

    public void OnAudioButtonClick()
    {
        isDimmedAudio = ToggleColor(audioButton, isDimmedAudio);
    }

    public void OnMusicButtonClick()
    {
        isDimmedMusic = ToggleColor(musicButton, isDimmedMusic);
    }

    private bool ToggleColor(Button button, bool isDimmed)
    {
        if (button == null) return isDimmed;

        Image image = button.GetComponent<Image>();
        if (image == null) return isDimmed;

        Color currentColor = image.color;

        if (isDimmed)
        {
            image.color = new Color(
                Mathf.Clamp01(currentColor.r * 2),
                Mathf.Clamp01(currentColor.g * 2),
                Mathf.Clamp01(currentColor.b * 2),
                currentColor.a
            );
        }
        else
        {
            image.color = new Color(
                currentColor.r / 2,
                currentColor.g / 2,
                currentColor.b / 2,
                currentColor.a
            );
        }

        return !isDimmed;
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
