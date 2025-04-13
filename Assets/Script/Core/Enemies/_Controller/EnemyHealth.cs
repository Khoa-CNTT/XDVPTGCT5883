using System;
using dang;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static event UnityAction OnEnemyKilled;
    public static event UnityAction<EnemiesController> OnEnemyHit;

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

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         DealDamage(5f);
    //     }
    //     CalculateHealth();
    // }

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
        else
        {
            OnEnemyHit?.Invoke(enemiesController);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        healthBar.fillAmount = 1f;
    }

    private void Die()
    {
        ResetHealth();
        OnEnemyKilled?.Invoke();
        ObjectPooling.ReturnToPool(gameObject);
        isDead = true;
    }
}
