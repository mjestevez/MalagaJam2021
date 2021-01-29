using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    public bool isKeyboardControl;

    Rigidbody2D rb;
    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.zero;
    }

    void Update()
    {
        CheckControllers();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        rb.velocity = direction * speed;
    }

    void CheckControllers()
    {
        float x = 0;
        float y = 0;
        
        if(isKeyboardControl)
        {
            x = CheckKeyboardHorizontalAxis();
            y = CheckKeyboardVerticalAxis();
            
        }
        else
        {
            x = CheckHorizontalAxis();
            y = CheckVerticalAxis();
        }
        
        direction = new Vector2(x, y);
    }

    #region Controller
    static float CheckVerticalAxis()
    {
        if(Gamepad.current==null)
            return 0;

        return Gamepad.current.leftStick.y.ReadValue();
    }

    static float CheckHorizontalAxis()
    {
        if(Gamepad.current==null)
            return 0;
        
        return Gamepad.current.leftStick.x.ReadValue();
    }
    #endregion

    static int CheckKeyboardVerticalAxis()
    {
        if(Keyboard.current.wKey.isPressed)
            return 1;
        
        if(Keyboard.current.sKey.isPressed)
            return -1;

        return 0;
        
    }

    static int CheckKeyboardHorizontalAxis()
    {
     
        if(Keyboard.current.dKey.isPressed)
            return 1;
        
        if(Keyboard.current.aKey.isPressed)
            return -1;

        return 0;
    }
}
