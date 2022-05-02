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

    private Dictionary<string, float> targetHealths = new Dictionary<string, float>();

    void Start(){
        targetHealths.Add("Player", maxPlayerHealth);
        targetHealths.Add("Base", maxBaseHealth);
    }
    void Update(){

        targetHealths["Player"] =  Mathf.Clamp(targetHealths["Player"] + regenRate * Time.deltaTime, 0 , maxPlayerHealth);
        targetHealths["Base"] =  Mathf.Clamp(targetHealths["Base"] + regenRate * Time.deltaTime, 0 , maxPlayerHealth);

        playerHealthSlider.value = targetHealths["Player"]/maxPlayerHealth;
        baseHealthSlider.value = targetHealths["Base"]/maxBaseHealth;
        
    }


    public void DamageTarget(string target, float damage){

        targetHealths[target] -= damage;
        if (targetHealths[target] <= 0){

            print("dEAD");

        }

    }

    void EndGame(){

        print("dEAD");

    }

}
