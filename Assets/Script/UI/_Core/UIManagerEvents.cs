using System;
using UnityEngine;
using UnityEngine.Events;

public static class UIManagerEvents
{
    // Main Menu
    public static readonly UnityEvent OnOpenMainMenu = new UnityEvent();
    public static readonly UnityEvent OnCloseMainMenu = new UnityEvent();

    // Setting Menu
    public static readonly UnityEvent OnOpenSettingMenu = new UnityEvent();
    public static readonly UnityEvent OnCloseSettingMenu = new UnityEvent();

    // newGame Menu
    public static readonly UnityEvent OnOpenNewGameMenu = new UnityEvent();
    public static readonly UnityEvent OnCloseNewGameMenu = new UnityEvent();
}
