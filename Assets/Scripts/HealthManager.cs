using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject cam;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider baseHealthSlider;

    public float regenRate = 1;
    public float maxPlayerHealth = 100;
    public float maxBaseHealth = 500;

    private Dictionary<string, float> targetHealths = new Dictionary<string, float>();

    void Start(){
        targetHealths.Add("Player", maxPlayerHealth);
        targetHealths.Add("baseCore", maxBaseHealth);
    }
    void Update(){

        targetHealths["Player"] =  Mathf.Clamp(targetHealths["Player"] + regenRate * Time.deltaTime, 0 , maxPlayerHealth);
        targetHealths["baseCore"] =  Mathf.Clamp(targetHealths["baseCore"] + regenRate * Time.deltaTime, 0 , maxBaseHealth);

        playerHealthSlider.value = targetHealths["Player"]/maxPlayerHealth;
        baseHealthSlider.value = targetHealths["baseCore"]/maxBaseHealth;
        
    }


    public void DamageTarget(string target, float damage){

        targetHealths[target] -= damage;
        if (targetHealths[target] <= 0){

            EndGame();

        }

    }

    void EndGame(){

        Destroy(player);
        Instantiate(cam, player.transform.position, player.transform.rotation);
        deathUI.SetActive(true);

    }

}
