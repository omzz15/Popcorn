using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
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
        enemyCount = 0;
        //lvl 1
        for (int i = 0 ; i < level*2 + 3 ; i++){

            Instantiate(enemy1, spawnPositions[rand.Next(0, spawnPositions.Length)].position, Quaternion.identity);
            enemyCount++;

        }
        //lvl 2
        for (int i = 0 ; i < level - 1 ; i++){

            Instantiate(enemy2, spawnPositions[rand.Next(0, spawnPositions.Length)].position, Quaternion.identity);
            enemyCount++;
        }
        //lvl 3
        for (int i = 0 ; i < Math.Floor( (decimal)(level/5)) ; i++){

            Instantiate(enemy3, spawnPositions[rand.Next(0, spawnPositions.Length)].position, Quaternion.identity);
            enemyCount++;
        }

    }
}
