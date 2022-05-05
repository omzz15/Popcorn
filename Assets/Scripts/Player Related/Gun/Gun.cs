using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //settings
    [Header("Transforms")]
    public Vector3 playerPosOffset;
    public Quaternion playerRotOffset;
    public Vector3 zoomingPlayerPosOffset;
    public Quaternion zoomingPlayerRotOffset;

    [Space]
    [Header("Objects")]
    public ParticleSystem flash;
    public Animator recoil;
    public AudioSource noise;

    [Header("Genaral Info")]
    public bool automatic;
    public Vector2 spreadAngle;
    public int bulletsPerShot;
    public float bulletDamage;

    [Space]
    [Header("Reload Info")]
    public int currentBullets;
    public int maxBullets;
    public float reloadTime;
    public float reloadWaitTime;

    [Space]
    [Header("Zoom Info")]
    public float zoomMagnification;
    public float crosshairMagnification;
    public Vector2 zoomSpreadDecrese;
    public float zoomSensitivityDivider;
    public float zoomSpeedMultiplyer;

    [Space]
    [Header("Bullet Info")]
    public float maxDistance;

    private Transform cameraTransform;
    private float timeSinceLastShot;
    private float timeSinceLastReload;

    private void Awake()
    {
        setVars();
        defineActions();
    }

    private void setVars()
    {
        Info.currentGun = this;
        
        transform.localPosition = playerPosOffset;
        transform.localRotation = playerRotOffset;
        cameraTransform = transform.parent.transform;
    }

    private void Update()
    {
        updateTimes();
        setScoping();
        reload();
        shoot();
    }

    private void updateTimes() {
        timeSinceLastReload += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;
    }

    public void resetTimeSinceLastShot() {
        timeSinceLastShot = 0;
    }

    public void resetTimeSinceLastRelaod() { 
        timeSinceLastReload = 0;
    }

    private bool shootKeyPressed()
    {
        return (Input.GetButtonDown("Shoot") || (automatic && Input.GetButton("Shoot")));
    }

    private bool doneShooting()
    {
        return !recoil.GetCurrentAnimatorStateInfo(0).IsName("PistolShoot");
    }

    private bool shouldShoot()
    {
        return currentBullets > 0 && doneShooting() && shootKeyPressed();
    }

    private bool canReload()
    {
        return (timeSinceLastShot >= reloadWaitTime && timeSinceLastReload >= reloadTime && currentBullets < maxBullets);
    }

    private void setScoping() {
        if (Input.GetButtonDown("Scope"))
            Info.SetScoping(true, true);
        else if (Input.GetButtonUp("Scope"))
            Info.SetScoping(false, true);
    }

    private void reload() {
        if (canReload()) {
            currentBullets++;
            resetTimeSinceLastRelaod();
        }
    }

    private void shoot() {
        if (shouldShoot()) {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                float randX = Random.Range(-.005f * spreadAngle.y, .005f * spreadAngle.y);
                float randY = Random.Range(-.005f * spreadAngle.x, .005f * spreadAngle.x);

                Vector3 spray = new Vector3(randX, randY, 0);

                RaycastHit hit;

                Debug.DrawRay(cameraTransform.position, transform.right + spray, Color.green, 10);
                if (Physics.Raycast(cameraTransform.position, transform.right + spray, out hit, maxDistance))
                {
                    if (hit.transform.gameObject.tag == "enemy")
                    {

                        hit.transform.gameObject.GetComponent<BasicEnemy>().TakeDamage(bulletDamage);

                    }

                }
            }

            flash.Clear();
            flash.Play();
            recoil.Play("PistolShoot");
            noise.Play();
            resetTimeSinceLastShot();
            currentBullets --;
        }
    }

    private void defineActions() {
        GameController.GetActionManager().AddAction(ActionManager.k_OnScoping, () => {
            transform.localPosition = zoomingPlayerPosOffset;
            transform.localRotation = zoomingPlayerRotOffset;
            spreadAngle.x -= zoomSpreadDecrese.x;
            spreadAngle.y -= zoomSpreadDecrese.y;
            
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnUnscoping, () => {
            transform.localPosition = playerPosOffset;
            transform.localRotation = playerRotOffset;
            spreadAngle.x += zoomSpreadDecrese.x;
            spreadAngle.y += zoomSpreadDecrese.y;
        });
    }
}
