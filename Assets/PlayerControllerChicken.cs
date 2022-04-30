using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerChicken : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private int turnAngle = 25;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpStrength = 17f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform feet;
    public float playerSpeed = 4f;
    public float rotSpeed = 0.3f;
    private int sprint;

    Vector3 velocity = Vector3.zero;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Normal Rotate
        float rotate = Input.GetAxis("Horizontal")*Time.deltaTime * rotSpeed + transform.localEulerAngles.y; 

        transform.localRotation = Quaternion.Euler(0,  rotate, 0);

        //Is sprinting?
        if (Input.GetKey(KeyCode.LeftShift)){
            sprint = 3;
        }
        else 
        {
            sprint = 1;
        }

        //Player Movement
        Vector3 move = Input.GetAxis("Vertical") *transform.forward;


        if (!Physics.Raycast(feet.position, Vector3.down, 0.3f, ground)){

            velocity.y += gravity * Time.deltaTime * Time.deltaTime /2;

        }
        controller.Move(move * Time.deltaTime * playerSpeed * sprint + velocity);


    }
}
