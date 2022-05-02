using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider baseHealthSlider;

    public float regenRate = 1;
    public float maxPlayerHealth = 100;
    public float maxBaseHealth = 500;
    private float playerHealth = 100;
    private float baseHealth = 500;

    void Update(){

        playerHealth = Mathf.Clamp(playerHealth + regenRate * Time.deltaTime, 0 , maxPlayerHealth);
        baseHealth = Mathf.Clamp(baseHealth + regenRate * Time.deltaTime, 0 , maxBaseHealth);

    }
    public void DamagePlayer(float damage){

        playerHealth -= damage;
        if (playerHealth <= 0){

            //die
            EndGame();

        }

    }

    public void DamageBase(float damage){

        baseHealth -= damage;
        if (baseHealth <= 0){

            //die
            EndGame();

        }

    }

    void EndGame(){

        print("dEAD");

    }

}
