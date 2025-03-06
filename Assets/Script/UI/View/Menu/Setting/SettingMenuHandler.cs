using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class SettingMenuHandler : MonoBehaviour
{
    public UIManagerEvents uiManagerEvents;
    public Button audioButton;
    public Button musicButton;

    private bool isDimmedAudio = false;
    private bool isDimmedMusic = false;

    public void OnExitButtonClick()
    {
        uiManagerEvents.OnCloseSettingMenu?.Invoke();
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
}
