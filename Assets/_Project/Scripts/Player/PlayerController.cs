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
    [SerializeField] private bool isMoving;

    [Header("Sprint Numbers")]
    public float sprintSpeed;
    public float Stamina, MaxStamina;
    public float AmountTaken;
    public bool isSprinting;
    public bool isRefilling;

    [Header("Other Numbers")]
    public float ditectionLength;

    [Header("States")]
    public bool isHiding;

    [Header("UI")]
    public Slider StaminaBar;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        StaminaBar.maxValue = MaxStamina;
        Stamina = MaxStamina;
        StaminaBar.value = Stamina;

        saveSpeed = speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        if (context.performed)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && isHiding == false)
        {
            saveSpeed = speed;

            if (isMoving == true && isHiding == false && isRefilling == false)
            {
                isSprinting = true;
                speed += sprintSpeed;
            }
            else
            {
                isSprinting = false;
                speed = saveSpeed;
                return;
            }
        }

        if (context.canceled)
        {
            if (isHiding == true)
            {
                speed = 0;
                return;
            }

            isSprinting = false;
            speed = saveSpeed;
            return;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        print("Being Pressed");

        float interactRange = ditectionLength;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            //LOCKER INTERACT
            if (collider.TryGetComponent(out LockerIntract lockerInteract))
            {
                if (isHiding == false)
                {
                    PlayerHide();
                    this.transform.position = collider.transform.position;
                    lockerInteract.Interact();
                    return;
                }

                if (isHiding == true)
                {
                    this.transform.position = lockerInteract.OutPos.transform.position;
                    lockerInteract.Interact();
                    Debug.Log("Left");
                    PlayerHideLeave();
                    transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
                    return;
                }
            }

            //STAMINA REFILL INTERACT
            if (collider.TryGetComponent(out StaminaRefillInteract staminaInteract))
            {
                isRefilling = true;

                isSprinting = false;
                isMoving = false;

                var FacePos = new Vector3(staminaInteract.transform.position.x, this.transform.position.y, staminaInteract.transform.position.z);
                transform.LookAt(FacePos);

                anim.Play("RefillingTest");
            }

            //ITEM INTERACT
            if (collider.TryGetComponent(out ItemScript itemScript))
            {
                itemScript.Collect();
                print("Found Item");
            }

            //LEAVE FLOOR INTERACT
            if (collider.TryGetComponent(out LeaveArea leaveArea))
            {
                leaveArea.LeaveFloor();
            }
        }
        return;
    }
    
    void PlayerHide()
    {
        isHiding = true;
        if (isSprinting == true) speed = speed - sprintSpeed;

        saveSpeed = speed;
        speed = 0;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
    }
    void PlayerHideLeave()
    {
        isHiding = false;
        isSprinting = false;

        if (isSprinting == true) speed = speed - sprintSpeed;

        speed = saveSpeed;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().enabled = true;
    }   

    void StaminaRecover()
    {
        Stamina = MaxStamina;
        StaminaBar.value = Stamina;
        isRefilling = false;
        speed = saveSpeed;

    }

    private void Update()
    {
        if (Stamina > 0)
        {
            if (isSprinting == true && isMoving == true && isHiding == false && isRefilling == false)
            {
                StaminaBar.value = Stamina;
                Stamina -= AmountTaken * Time.deltaTime;
                if (Stamina < 0) Stamina = 0;
            }
        }
        else
        {
            isSprinting = false;
            speed = saveSpeed;
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        if (isRefilling == true) return;

        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
