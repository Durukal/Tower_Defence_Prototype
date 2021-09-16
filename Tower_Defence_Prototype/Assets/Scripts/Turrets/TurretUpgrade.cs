using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour {
    [Header("Upgrades")]
    [SerializeField] private float attackRangeIncremental;
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
    private Turret _turret;

    void Start() {
        _turretProjectile = GetComponent<TurretProjectile>();
        _turret = GetComponent<Turret>();
        UpgradeCost = upgradeInitialCost;

        SellPerc = sellPercentage;
        Level = 1;
    }

    public int GetSellValue() {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }

    public void UpgradeTurret() {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost) {
            if (_turretProjectile.Damage <= 100f) {
                _turretProjectile.Damage += damageIncremental;
            } else {
                _turretProjectile.Damage = 100f;
            }
        
            if (_turretProjectile.DelayPerShot >= 0f) {
                _turretProjectile.DelayPerShot -= delayReduce;
                if (_turretProjectile.DelayPerShot <= 0f) {
                    _turretProjectile.DelayPerShot = 0.1f;
                }
            }

            if (_turret.AttackRange < 5f) {
                UpdateAttackRange();
            }
            
            UpdateUpgrade();
        }
    }

    private void UpdateAttackRange() {
        _turret.AttackRange += attackRangeIncremental;
        _turret.GetComponent<CircleCollider2D>().radius = _turret.AttackRange;
    }

    private void UpdateUpgrade() {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }
}