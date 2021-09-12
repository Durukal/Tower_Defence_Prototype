using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    private int health = 10;

    public int TotalHealth { get; set; }

    private void Start() {
        TotalHealth = health;
    }

    private void ReduceHealth(Enemy enemy) {
        TotalHealth--;
        if (TotalHealth <= 0) {
            TotalHealth = 0;
            //GameOver Logic
        }
    }

    private void OnEnable() {
        Enemy.OnEndReached += ReduceHealth;
    }

    private void OnDisable() {
        Enemy.OnEndReached -= ReduceHealth;
    }
}