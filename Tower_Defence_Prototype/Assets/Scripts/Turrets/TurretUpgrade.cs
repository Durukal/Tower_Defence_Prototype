using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour {
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    [Header("Sell")]
    [Range(0,1)]
    [SerializeField] private float sellPercentage;

    public float SellPerc { get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }
    
    private TurretProjectile _turretProjectile;

    void Start() {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;

        SellPerc = sellPercentage;
        Level = 1;
    }

    public int GetSellValue() {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }
    public void UpgradeTurret() {
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
        Level++;
    }
}