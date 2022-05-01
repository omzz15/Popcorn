using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform feet;
    [SerializeField] private Transform head;
    [SerializeField] private Transform[] headPositions;
    public float playerSpeed = 4f;
    public float rotSpeed = 0.3f;
    private float sprint;

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
            sprint = 2;
        }
        else 
        {
            sprint = 1;
        }
        //Is Crouching?
        if (Input.GetKey(KeyCode.LeftControl)){
            head.position = headPositions[1].position;
            sprint = 0.5f;
        }
        else 
        {
            head.position = headPositions[0].position;
        }

        //Player Movement
        Vector3 move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;


        if (!Physics.Raycast(feet.position, Vector3.down, 0.3f, ground)){

            velocity.y += gravity * Time.deltaTime * Time.deltaTime /2;

        }
        controller.Move(move * Time.deltaTime * playerSpeed * sprint + velocity);


    }
}
