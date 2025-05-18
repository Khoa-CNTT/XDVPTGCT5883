using System;
using dang;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static event UnityAction<EnemiesController> OnEnemyKilled;

    [SerializeField] private GameObject HealthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    public Image healthBar;
    public bool isDead = false;
    private EnemiesController enemiesController;

    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        enemiesController = GetComponent<EnemiesController>();
    }

    public void CalculateHealth()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(HealthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(barPosition);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        healthBar = container.FillAmoutImage;
    }

    public void DealDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    private void ShowHealthBar(bool show)
    {
        if (barPosition != null)
            barPosition.gameObject.SetActive(show);
    }

    public void ResetHealth()
    {
        if (healthBar == null) return;

        CurrentHealth = initialHealth;
        healthBar.fillAmount = 1f;
        ShowHealthBar(true);
    }

    private void Die()
    {
        ShowHealthBar(false);
        OnEnemyKilled?.Invoke(this.enemiesController);
        enemiesController.Dead();
    }
}
