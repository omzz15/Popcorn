using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private TextMeshProUGUI nightCount;
    private int level = 1;
    private int enemyCount = 3;
    private System.Random rand = new System.Random();

    void Start(){

        NewLevel();

    }
    public void EnemyDeath(){

        enemyCount --;
        if (enemyCount <= 0){

            level++;
            NewLevel();

        }

    }

    void NewLevel(){

        nightCount.text = "Night: " + level;

        //pls made this more robust in the future Om or Mari
        for (int i = 0 ; i < level * 3 ; i++){

            Instantiate(enemy1, spawnPositions[rand.Next(0, spawnPositions.Length)].position, Quaternion.identity);

        }
        enemyCount = level * 3;

    }
}
