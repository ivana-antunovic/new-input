using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float crouchSpeed = 5f;


    private Vector2 lookInput;

    public bool crouch;

   



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
        Debug.Log(moveInput);
    }

    private void Move()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

       // Vector3 velocity = new Vector3(moveInput.x, 0, moveInput.y);
        rigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
       if(context.started)
       {
         Shoot();
       }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
         if(context.started)
         {
            crouch = true;
         }

         if(context.started)
         {
            crouch = false;
         }

         Crouch();
    }

    private void Crouch()
    {
        if (crouch)
        {
            moveSpeed = crouchSpeed;
            transform.localScale = new Vector3(1, 0.5f, 1);
            transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.y);
        }

        if(!crouch)
        {
            moveSpeed = 10f;
            transform.localScale = Vector3.one;
        }
    }


    private void Shoot()
    {
        Debug.Log($"Shoot");
    }

    private void Look()
    {
        transform.Rotate(Vector3.up, lookInput.x);
    }

    public void OnLook(InputAction.CallbackContext context)

    {
        lookInput = context.ReadValue<Vector2>();
        Debug.Log(lookInput);
    }

    public void OnInteract()
    {

    }

    private void Interact()
    {

    }



    private void Update()
    {
        Move();
        Look();
        Crouch();
        Interact();
        
    }
}
