using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public InputActionAsset inputActionAsset;
    private InputAction moveAction;
    public Transform OpponentTransform;
    public bool inputReady = false;
    public bool isAlive = true;
    private float defaultY = 7.901f;

    void Awake()
    {
        // Find the "Move" action within the "Player" action map
        moveAction = inputActionAsset.FindActionMap("Player").FindAction("Move");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        defaultY = transform.position.y;
    }

    public void resetAnimatorBools()
    {
        print("Resetting animator bools");
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("UpRight", false);
        animator.SetBool("UpLeft", false);
        animator.SetBool("DownRight", false);
        animator.SetBool("DownLeft", false);
        print("Setting inputReady to true");
        inputReady = true;
    }

    public void SetInputReadyTrue()
    {
        print("Setting inputReady to true");
        inputReady = true;
    }

    public void SetInputReadyFalse()
    {
        print("Setting inputReady to false");
        inputReady = false;
    }

    void animate(Vector2 movementInput)
    {
        // Convert to 8 cardinal directions
        Vector2 cardinal = Vector2.zero;
        if (movementInput.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            
            // Normalize angle to 0-360
            if (angle < 0) angle += 360;
            
            // Convert to 8 directions (45 degree segments)
            int direction = Mathf.RoundToInt(angle / 45f) % 8;

            switch (direction)
            {
                case 0: cardinal = Vector2.right; animator.SetBool("Right", true); break;
                case 1: cardinal = new Vector2(1, 1).normalized; animator.SetBool("UpRight", true); break;
                case 2: cardinal = Vector2.up; animator.SetBool("Up", true); break;
                case 3: cardinal = new Vector2(-1, 1).normalized; animator.SetBool("UpLeft", true); break;
                case 4: cardinal = Vector2.left; animator.SetBool("Left", true); break;
                case 5: cardinal = new Vector2(-1, -1).normalized; animator.SetBool("DownLeft", true); break;
                case 6: cardinal = Vector2.down; animator.SetBool("Down", true); break;
                case 7: cardinal = new Vector2(1, -1).normalized; animator.SetBool("DownRight", true); break;
            }
        }
    }

    void Update()
    {
        if (!isAlive) return;
        if (animator.enabled == false) isAlive = false;

        // if animation is playing, return
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return;
        }

        Vector3 direction = OpponentTransform.position - transform.position;
        direction.y = 0; // Keep rotation on horizontal plane
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100f);
        }
        transform.position = new Vector3(transform.position.x, defaultY, transform.position.z);
        
        if (!inputReady) return;
        // Process movementInput (e.g., apply to character movement)
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        animate(movementInput);
    }
}
