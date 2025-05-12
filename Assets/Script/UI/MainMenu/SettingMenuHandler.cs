using UnityEngine;
using UnityEngine.UI;

public class SettingMenuHandler : MonoBehaviour, IBaseUI
{
    public Button audioButton;
    public Button musicButton;
    private CanvasGroup canvasGroup;

    private bool isDimmedAudio = false;
    private bool isDimmedMusic = false;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        UIManagerEvents.Instance?.OnOpenSettingMenu.AddListener(Open);
        UIManagerEvents.Instance?.OnCloseSettingMenu.AddListener(Close);
    }

    public void OnExitButtonClick()
    {
        UIManagerEvents.Instance?.OnCloseSettingMenu?.Invoke();
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
