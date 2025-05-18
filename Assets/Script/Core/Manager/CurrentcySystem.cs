using System;
using dang;
using UnityEngine;

public class CurrentcySystem : Singleton<CurrentcySystem>
{
    [SerializeField] private int coinTest;
    private string CUTTENCY_SAVE_KEY = "MYGAME_CURRENCY";

    public int TotalCoins { get; set; }

    void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }

    void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }

    void Start()
    {
        PlayerPrefs.DeleteKey(CUTTENCY_SAVE_KEY);
        Loadcoins();
    }

    private void Loadcoins()
    {
        TotalCoins = PlayerPrefs.GetInt(CUTTENCY_SAVE_KEY, coinTest);
    }

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(CUTTENCY_SAVE_KEY, TotalCoins);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CUTTENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
        }
    }

    private void AddCoins(EnemiesController enemy)
    {
        AddCoins(1);
    }
}
