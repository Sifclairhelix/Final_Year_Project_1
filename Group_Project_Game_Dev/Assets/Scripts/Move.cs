using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    
    [SerializeField] private Transform target;

    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float endsVelocity = -10.0f;
    public float minFall = -1.5f;
    public float runSpeed = 10;
    public float walkSpeed = 4;
    private float verSpeed;

    private float moveSpeed;

    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        verSpeed = minFall;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        float horspeed = Input.GetAxis("Horizontal");
        float verspeed = Input.GetAxis("Vertical");

        if (horspeed != 0 || verspeed != 0)
        {
            movement.x = horspeed * moveSpeed;
            movement.z = verspeed * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            movement = target.TransformDirection(movement);
        }

        // switch run or walk
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        // in ground?
        if (character.isGrounded)
        {
            // jump
            if (Input.GetButtonDown("Jump"))
            {
                verSpeed = jumpSpeed;
            }
            else
            {
                verSpeed = minFall;
            }
        }
        else
        {
            verSpeed += gravity * 3 * Time.deltaTime;
            // max down speed
            if (verSpeed < endsVelocity)
            {
                verSpeed = endsVelocity;
            }
        }

        movement.y = verSpeed;
        movement *= Time.deltaTime;
        character.Move(movement);
    }
}

