using System;
using dang;
using UnityEngine;

public class TowerShopViewPanel : MonoBehaviour
{
    [SerializeField] private TowerCard towerCardPrefabs;
    [SerializeField] private RectTransform towerPanelContainer;
    [SerializeField] private RectTransform towerShopPanel;

    [Header("Tower Settings")]
    [SerializeField] private TowerSettings towerSettings;

    private Node _currentNodeSelected;

    void Start()
    {
        CreateTurretCards();
    }

    private void CreateTurretCards()
    {
        var towerList = towerSettings.GetTowerData();

        for (int i = 0; i < towerList.Count; i++)
        {
            var newInstance = Instantiate(towerCardPrefabs, towerPanelContainer);
            TowerCard towerCard = newInstance.GetComponent<TowerCard>();
            towerCard.SetupTurretButton(towerSettings, i);
        }
    }

    public void CloseTowerShopPanel()
    {
        towerShopPanel.gameObject.SetActive(false);
    }

    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
    }

    private void PlaceTower(TowerData data)
    {
        if (_currentNodeSelected != null)
        {
            GameObject towerInstance = Instantiate(towerSettings.towerPrefabs);
            towerInstance.transform.localPosition = _currentNodeSelected.transform.position;
            towerInstance.transform.parent = _currentNodeSelected.transform;

            TowerController towerController = towerInstance.GetComponent<TowerController>();
            if (towerController == null)
            {
                Debug.LogError("TowerController not found on prefab!");
                return;
            }

            towerController.InjectData(data);
            _currentNodeSelected.SetTower(towerInstance);

            CloseTowerShopPanel();
        }
    }

    private void towerSold()
    {
        _currentNodeSelected = null;
    }

    void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
        Node.OnTowerSold += towerSold;
        TowerCard.OnplaceTower += PlaceTower;
    }

    void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
        Node.OnTowerSold -= towerSold;
        TowerCard.OnplaceTower -= PlaceTower;
    }
}
