using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    #region Scene References
    [Header("Scene References")]
    public GameManager gm;
    public PlayerMovement PlayerMovement;
    public GameObject crosshair;
    
    public GameObject hook;
    public Rigidbody2D playerRigidBody;
    public GameObject hookedObj;
    #endregion

    #region Tuning
    [Header("Tuning")]
    public float hookTravelSpeed;
    public float playerTravelSpeed;
    public float maxDistance;
    public static bool fired;
    public bool hooked;
    #endregion

    #region Private Variables
    float currentDistance;
    float mass;
    bool targetCalculated = false;
    Vector3 targetPos;
    Vector3 xScale;
    #endregion

    void Start()
    {
        // Rope Initialization
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.positionCount = 0;

        // Store player mass
        mass = playerRigidBody.mass;
    }

    void Update()
    {
        if (fired && hooked)
            if (Input.GetButtonDown("Jump"))
                ReturnHook();
    }

    void FixedUpdate()
    {
        #region Player Grapple
        if (PlayerMovement.grapple)
        {
            // Check if the player just pressed the button and calculate everything
            if (!targetCalculated && targetPos != crosshair.transform.position)
            {
                targetPos = crosshair.transform.position;
                targetCalculated = true;
                fired = true;
            }

            // Instantiate the rope
            if (fired)
            {
                LineRenderer rope = hook.GetComponent<LineRenderer>();
                rope.positionCount = 2;
                rope.SetPosition(0, transform.position);
                rope.SetPosition(1, hook.transform.position);
            }

            // Start moving the hook and handle the "max distance without hooking" case
            if (fired && !hooked)
            {
                hook.transform.parent = null;
                hook.transform.Translate(targetPos * Time.deltaTime * hookTravelSpeed);
                currentDistance = Vector2.Distance(transform.position, hook.transform.position);

                if (currentDistance >= maxDistance)
                    ReturnHook();
            }

            // Handle the "hook hit" case and move the player
            if (hooked && fired)
            {
                playerRigidBody.mass = 0.001f;
                hook.transform.parent = hookedObj.transform;
                transform.position = Vector2.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.fixedDeltaTime);
                float distanceToHook = Vector2.Distance(transform.position, hook.transform.position);

                if (distanceToHook < 2.5f)
                    ReturnHook();
            }
        }
        #endregion
    }

    // Reset everything once the player reaches the hooked object or if the hook reaches its max distance
    void ReturnHook()
    {
        hook.transform.rotation = transform.rotation;
        hook.transform.position = transform.position;
        hook.transform.parent = this.transform;

        PlayerMovement.grapple = false;
        playerRigidBody.mass = mass;

        fired = false;
        targetCalculated = false;
        hooked = false;
        hookedObj = null;

        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.positionCount = 0;
    }
}
