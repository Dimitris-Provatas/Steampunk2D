using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    [Header("Scene References")]
    public CharacterController2D controller;

    [Header("Tuning")]
    public float runSpeed = 40f;
    public bool grapple = false;
    #endregion

    #region Private Variables
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool Show = false;
    #endregion

    void Update()
    {
        #region Inputs
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Jump case
        if (Input.GetButtonDown("Jump"))
            jump = true;

        // Grapple case
        if (Input.GetButtonDown("Grapple"))
            grapple = true;
        
        // Crouch case
        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
        #endregion
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
