using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager> {
    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI HealthText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;
    [SerializeField] private TextMeshProUGUI gameOverHealthText;
    private int KillCount = 0;
    
    private Node _currentNodeSelected;

    private void Update() {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        HealthText.text = LevelManager.Instance.TotalHealth.ToString();
        currentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave}";
        killCountText.text = $"Kills {KillCount}";
    }

    public void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        gameOverHealthText.text = LevelManager.Instance.TotalHealth.ToString();
    }
    
    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void CloseTurretShopPanel() {
        turretShopPanel.SetActive(false);
    }

    public void CloseNodeUIPanel() {
        _currentNodeSelected.CloseAttackRangeSprite();
        nodeUIPanel.SetActive(false);
    }
    
    public void UpgradeTurret() {
        _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
        UpdateUpgradeText();
        UpdateTurretLevelText();
        UpdateSellValue();
    }

    public void SellTurret() {
        _currentNodeSelected.SellTurret();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }
    private void ShowNodeUI() {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateTurretLevelText();
        UpdateSellValue();
    }

    private void UpdateUpgradeText() {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }
    public void UpdateKillCountText() {
        KillCount++;
        killCountText.text = $"Kills {KillCount}";
    }

    private void UpdateTurretLevelText() {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }

    private void UpdateSellValue() {
        int sellAmount = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }
    
    private void NodeSelected(Node nodeSelected) {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty()) {
            turretShopPanel.SetActive(true);
        } else {
            ShowNodeUI();
        }
    }
    
    private void OnEnable() {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable() {
        Node.OnNodeSelected -= NodeSelected;
    }
}
