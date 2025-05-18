using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using EasyTransition;

public class ButtonToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image regularImage;
    public Image pressedImage;
    public TextMeshProUGUI buttonTMP;
    private Button button;
    [SerializeField] private string sceneName;
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float delay = 0.5f;


    void Start()
    {
        button = GetComponent<Button>();

        if (regularImage != null && pressedImage != null)
        {
            pressedImage.gameObject.SetActive(false);
            regularImage.gameObject.SetActive(true);
        }
        button.onClick.AddListener(() => TransitionManager.Instance().Transition(sceneName, transitionSettings, delay));
    }

    public void OnClick()
    {
        button.onClick.Invoke();
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
