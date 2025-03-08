using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image regularImage;
    public Image pressedImage;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        pressedImage.gameObject.SetActive(false);
        regularImage.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressedImage.gameObject.SetActive(true);
        regularImage.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressedImage.gameObject.SetActive(false);
        regularImage.gameObject.SetActive(true);
    }
}
