using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image regularImage;
    public Image pressedImage;
    public TextMeshProUGUI buttonTMP;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (regularImage != null && pressedImage != null)
        {
            pressedImage.gameObject.SetActive(false);
            regularImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (regularImage != null && pressedImage != null)
        {
            pressedImage.gameObject.SetActive(true);
            regularImage.gameObject.SetActive(false);
        }
        else if (buttonTMP != null)
        {
            Vector3 position = buttonTMP.rectTransform.localPosition;
            position.y -= 4;
            buttonTMP.rectTransform.localPosition = position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (regularImage != null && pressedImage != null)
        {
            pressedImage.gameObject.SetActive(false);
            regularImage.gameObject.SetActive(true);
        }
        else if (buttonTMP != null)
        {
            Vector3 position = buttonTMP.rectTransform.localPosition;
            position.y += 4;
            buttonTMP.rectTransform.localPosition = position;
        }
    }
}
