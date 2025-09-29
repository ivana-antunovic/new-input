using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // trenutno player moze uzeti objekat gdje god se nalazio, krenula sam raditi metodu za neki pickup range ali odjednom mi
    //se unity crashao tako da je cijeli ekran bio u sarenim crtama i onda se sve zacrnilo. zatim se pojavila kockica sa logom
    //unityja i crvenim usklicnikom. ugl nakon te epizode sam odlucila da player ima neku telekinezu i moze manipulirati predmetima
    //iz daljine
    private Rigidbody rigidbody;

    [SerializeField] private GameObject selectedObject;
    [SerializeField] private Vector3 pickedUpPosition;
    [SerializeField] private float pickupRange = 2f;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 10f;

    private Vector2 lookInput;

    private bool isTrigger;
    int counter = 0;

    private bool isCrounching;
    private bool isPickedUp;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isTrigger = true;
        }

        if (context.canceled)
        {
            isTrigger = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCrounching = true;
        }

        if (context.canceled)
        {
            isCrounching = false;
        }

        Crouch();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
        {
           PickUpObject();
        }
    }



    private void Move()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        // Vector3 velocity = new Vector3(moveInput.x, 0, moveInput.y);
        rigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    private void Look()
    {
        transform.Rotate(Vector3.up, lookInput.x);
    }

    private void Attack()
    {


        if (isTrigger)
        {
            counter++;
            Debug.Log($"I attacked {counter} times");
        }
    }

    private void Crouch()
    {
        if (isCrounching)
        {
            moveSpeed = 5.5f;
            transform.localScale = new Vector3(1, 0.5f, 1);
            transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
        }

        if (!isCrounching)
        {
            moveSpeed = 10;
            transform.localScale = Vector3.one;
        }
    }

    private void PickUpObject()
    {
        if (!isPickedUp && selectedObject != null)
        {
            
            isPickedUp = true;
            selectedObject.transform.SetParent(transform);
            selectedObject.transform.localPosition = pickedUpPosition;

            Rigidbody objRigidbody = selectedObject.GetComponent<Rigidbody>();
            if (objRigidbody != null)
            {
                objRigidbody.isKinematic = true;
            }
        }
        else if (isPickedUp)
        {
           
            isPickedUp = false;
            selectedObject.transform.SetParent(null);

            Rigidbody objRigidbody = selectedObject.GetComponent<Rigidbody>();
            if (objRigidbody != null)
            {
                objRigidbody.isKinematic = false;
            }
        }
    }

   


    private void Update()
    {
        Move();
        Look();
        Attack();
        // Crouch();
       

    }
}