using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField]private GameObject healthBarPrefab;
    [SerializeField]private Transform barPosition;

    [SerializeField]private float initialHealth = 100f;
    [SerializeField]private float maxHealth = 100f;

    public int killCount { get; set; }
    public float CurrentHealth { get; set; }
    
    private Image _healthBar;
    private Enemy _enemy;

    void Start() {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        
        _enemy = GetComponent<Enemy>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            DealDamage(50f);
        }

        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar() {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        newBar.transform.localScale = new Vector3(10f, 10f, 10f);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived) {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0) {
            CurrentHealth = 0;
            Die();
        } else {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    public void ResetHealth() {
        CurrentHealth = initialHealth;
        _healthBar.fillAmount = 1f;
    }

    private void Die() {
        UIManager.Instance.UpdateKillCountText();
        OnEnemyKilled?.Invoke(_enemy);
    }

    
}