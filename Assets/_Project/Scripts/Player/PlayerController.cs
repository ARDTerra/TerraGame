using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

    [Header("Speed Numbers")] 
    public float speed;
    private float saveSpeed;
    public float rotationSpeed;
    private Vector2 move;

    [Header("Sprint Numbers")]
    public float sprintSpeed;
    public float Stamina, MaxStamina;
    public float AmountTaken;

    [Header("Other Numbers")]
    public float ditectionLength; // how far an object has to be before player can interact

    [Header("States")]
    public bool isHiding;
    public bool isSprinting; // checks if player is sprinting
    public bool isRefilling; // checks if player is refilling
    [SerializeField] private bool isMoving; // checks if player is moving

    [Header("UI")]
    public Slider StaminaBar;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        // sets stamina mechanic
        StaminaBar.maxValue = MaxStamina;
        Stamina = MaxStamina;
        StaminaBar.value = Stamina;

        saveSpeed = speed; // saves speed
    }

    // when player clicks interact button
    public void OnMove(InputAction.CallbackContext context) 
    {
        move = context.ReadValue<Vector2>(); // reads move value
        if (context.performed)
        {
            isMoving = true; // sets move to true
        }
        else
        {
            isMoving = false; // sets move to false
        }
    }

    // when player clicks the sprint button
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && isHiding == false) // checks if player clicking button and isnt hiding
        {
            saveSpeed = speed; // saves speed

            if (isMoving == true && isHiding == false && isRefilling == false) // checks players states
            {
                isSprinting = true; // sets sprinting state
                speed += sprintSpeed; // adds sprint speed
            }
            else
            {
                isSprinting = false; // sets spriting state
                speed = saveSpeed; // returns speed back to how it was
                return;
            }
        }

        if (context.canceled) // if button stop being pressed
        {
            if (isHiding == true) // checks if player is hiding
            {
                speed = 0; // sets speed to 0
                return;
            }

            isSprinting = false; // sets state
            speed = saveSpeed; // returns speed
            return;
        }
    }

    // when player clicks the interact button
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return; // checks if it isnt happening 

        //checks distance from any object in scene
        float interactRange = ditectionLength; // distance length
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange); // circle overlap\

        foreach (Collider collider in colliderArray) // for every object the circle finds that can be interacted with
        {
            // LOCKER INTERACT
            if (collider.TryGetComponent(out LockerIntract lockerInteract)) // if locker is being touched
            {
                if (isHiding == false) // and player isnt hiding
                {
                    PlayerHide();
                    this.transform.position = collider.transform.position; // moves player
                    lockerInteract.Interact(); // calls locker script
                    return;
                }

                if (isHiding == true) // if player is already hiding
                {
                    this.transform.position = lockerInteract.OutPos.transform.position; // lets player go
                    lockerInteract.Interact(); // calls locker script
                    PlayerHideLeave();
                    transform.position = new Vector3(transform.position.x, 1f, transform.position.z); // bumps player back up on floor
                    return;
                }
            }

            // STAMINA REFILL INTERACT
            if (collider.TryGetComponent(out StaminaRefillInteract staminaInteract)) // if refil station being touched
            {
                isRefilling = true; // sets states

                // stops states
                isSprinting = false;
                isMoving = false;

                // makes player face state
                var FacePos = new Vector3(staminaInteract.transform.position.x, this.transform.position.y, staminaInteract.transform.position.z);
                transform.LookAt(FacePos);

                anim.Play("RefillingTest"); // plays animation
            }

            // ITEM INTERACT
            if (collider.TryGetComponent(out ItemScript itemScript)) // if item is being touched
            {
                itemScript.Collect(); // calls item script
            }

            // LEAVE FLOOR INTERACT
            if (collider.TryGetComponent(out LeaveArea leaveArea)) // if floor leave is being touched
            {
                leaveArea.LeaveFloor(); // calls floor script
            }
        }
        return;
    }
    
    void PlayerHide() // PlayerHide function
    {
        isHiding = true; // sets state

        // return speed if sprinting and saves current speed
        if (isSprinting == true) speed = speed - sprintSpeed;
        saveSpeed = speed;
        speed = 0;

        // turns off collision and gravity
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
    }
    void PlayerHideLeave() // PlayerHideLeave function
    {
        // sets states
        isHiding = false;
        isSprinting = false;

        speed = saveSpeed; // returns speed

        // turns on collision and gravity
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().enabled = true;
    }   

    void StaminaRecover() // StaminaRecover function
    {
        Stamina = MaxStamina; // sets stamina to max
        StaminaBar.value = Stamina; // updates stamina bar
        isRefilling = false; // sets state
        speed = saveSpeed; // returns speed

    }

    private void Update()
    {
        if (Stamina > 0) // if stamina isnt 0
        {
            if (isSprinting == true && isMoving == true && isHiding == false && isRefilling == false) // checks all these states the player might be in
            {
                StaminaBar.value = Stamina; // sets stamina bar
                Stamina -= AmountTaken * Time.deltaTime; // removes stamina
                if (Stamina < 0) Stamina = 0; // makes sure the bar cant reach under 0
            }
        }
        else // if player isnt sprinting anymore
        {
            isSprinting = false; // sets state
            speed = saveSpeed; // returns speed
        }
    }

    void FixedUpdate()
    {
        MovePlayer(); // runs this later on the frame
    }

    public void MovePlayer() // MovePlayer function
    {
        if (isRefilling == true) return; // makes sure player cant move when refilling

        Vector3 movement = new Vector3(move.x, 0f, move.y); // vector movement

        if (movement != Vector3.zero) // if the movement isnt 0
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed); // sets rotation the player moving at
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World); // sets the player within the world
    }
}
