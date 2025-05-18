using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("UI Elements")]
    [Space(10)]
    [Header("Main Menu")]
    [SerializeField] public Button StartGameButton;
    [Space(10)]

    [Header("New Game")]
    [SerializeField] public List<Button> SaveSlotsButton;
    [SerializeField] public Button OnNewGameBackButton;
    [Space(10)]

    [Header("Setting Menu")]
    [SerializeField] public Button SettingButton;
    [SerializeField] public Button AudioButton;
    [SerializeField] public Button MusicButton;
    [SerializeField] public Button OnSettingBackButton;

    private void Start()
    {
        CloseAllScene();
        Init();
    }

    private void Init()
    {
        if (StartGameButton != null)
            StartGameButton.interactable = true;

        if (OnNewGameBackButton != null)
            OnNewGameBackButton.interactable = true;

        if (SettingButton != null)
            SettingButton.interactable = true;

        if (AudioButton != null)
            AudioButton.interactable = true;

        if (MusicButton != null)
            MusicButton.interactable = true;

        if (OnSettingBackButton != null)
            OnSettingBackButton.interactable = true;

        if (SaveSlotsButton != null)
        {
            foreach (var btn in SaveSlotsButton)
            {
                if (btn != null) btn.interactable = true;
            }
        }

        UIManagerEvents.OnOpenMainMenu?.Invoke();
    }

    public void CloseAllScene()
    {
        UIManagerEvents.OnCloseMainMenu?.Invoke();
        UIManagerEvents.OnCloseSettingMenu?.Invoke();
        UIManagerEvents.OnCloseNewGameMenu?.Invoke();
    }
}
