using System;
using dang;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTowerSold;
    public TowerController TowerController { get; private set; }
    [SerializeField] private GameObject attackRangeSprite;
    private float _rangeSize;
    private Vector3 _rangeOriginalSize;

    void Start()
    {
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize = attackRangeSprite.transform.localScale;
    }

    public void SetTower(GameObject towerInstance)
    {
        TowerController = towerInstance.GetComponent<TowerController>();
    }

    public bool IsEmpty()
    {
        return TowerController == null;
    }

    public void CloseAttackRangeSprite()
    {
        attackRangeSprite.SetActive(false);
    }

    public void SelectTower()
    {
        OnNodeSelected?.Invoke(this);
        if (!IsEmpty())
        {
            ShowTowerInfo();
        }
    }

    public void SellTower()
    {
        if (!IsEmpty())
        {
            CurrentcySystem.Instance.AddCoins(TowerController.TowerUpgrade.GetSellValue());
            Destroy(TowerController.gameObject);
            TowerController = null;
            attackRangeSprite.SetActive(false);
            OnTowerSold?.Invoke();
        }
    }

    private void ShowTowerInfo()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = _rangeOriginalSize * TowerController.AttackRange / (_rangeSize / 1.50f);
    }
}
