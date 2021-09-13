using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour {
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    public int UpgradeCost { get; set; }
    
    private TurretProjectile _turretProjectile;

    void Start() {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            UpgradeTurret();
        }
    }

    private void UpgradeTurret() {
        if (CurrencySystem.Instance.TotalCoins > UpgradeCost) {
            if (_turretProjectile.Damage <= 100f) {
                _turretProjectile.Damage += damageIncremental;
            } else {
                _turretProjectile.Damage = 100f;
            }
        
            if (_turretProjectile.DelayPerShot >= 0f) {
                _turretProjectile.DelayPerShot -= delayReduce;
            } else {
                _turretProjectile.DelayPerShot = 0f;
            }
            
            UpdateUpgrade();
        }
    }

    private void UpdateUpgrade() {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
    }
}