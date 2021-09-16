using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {
    [SerializeField]
    private int health = 10;

    public int TotalHealth { get; set; }
    public int CurrentWave { get; set; }
    public bool IsGameOver = false;

    private void Start() {
        TotalHealth = health;
        CurrentWave = 1;
    }

    private void ReduceHealth(Enemy enemy) {
        TotalHealth--;
        if (TotalHealth <= 0) {
            TotalHealth = 0;
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver() {
        IsGameOver = true;
        yield return new WaitForSeconds(3f);
        UIManager.Instance.ShowGameOverPanel();
        Time.timeScale = 0;
    }

    private void WaveCompleted() {
        CurrentWave++;
        if (CurrentWave > 30) {
            CurrentWave = 30;
            StartCoroutine(GameOver());
        }
    }

    private void OnEnable() {
        Enemy.OnEndReached += ReduceHealth;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable() {
        Enemy.OnEndReached -= ReduceHealth;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}