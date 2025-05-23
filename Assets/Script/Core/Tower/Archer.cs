using System;
using dang;
using UnityEngine;

public class Archer : MonoBehaviour
{
    TowerController towerController;
    TowerProjectile towerProjectile;

    void Start()
    {
        towerController = GetComponentInParent<TowerController>();
        towerProjectile = GetComponentInParent<TowerProjectile>();
    }

    public void Shoot()
    {
        towerProjectile.Shoot();
    }

    public void ReturnToIdle()
    {
        Shoot();
        towerController.ReturnToIdle();
    }
}
