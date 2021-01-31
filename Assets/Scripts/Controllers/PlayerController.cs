using System.Collections.Generic;
using KrillAudio.Krilloud;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int playerID;
    [SerializeField] float speed;
    public Vector2 lastFaceDirection;
    public FieldOfView fov;
    public FieldOfView baseFov;
    public List<GameObject> positions;
        
    bool isKeyboardControl;
    Rigidbody2D rb;
    Vector2 direction;
    static Gamepad device;
    KLAudioSource audioSource;

    public float RbForce => rb.velocity.magnitude;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.zero;
        audioSource = GetComponent<KLAudioSource>();
    }

    void Update()
    {
        GetControllerType();
        CheckControllers();
    }

    void GetControllerType()
    {
        if(Gamepad.all.Count > playerID)
        {
            device = Gamepad.all[playerID];
            isKeyboardControl = false;
        }
        else
            isKeyboardControl = true;
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        rb.velocity = direction * speed;
        
        if(rb.velocity.magnitude!=0)
            audioSource.Play();
        else
            audioSource.Stop();
            
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
        
        UpdateLastFaceDirection(x, y);

    }

    #region Controller
    static float CheckVerticalAxis()
    {
        return device.leftStick.y.ReadValue();
    }

    static float CheckHorizontalAxis()
    {
        return device.leftStick.x.ReadValue();
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
    
    void UpdateLastFaceDirection(float x, float y)
    {
        if(x != 0 || y != 0)
        {
            if(Mathf.Abs(x) > Mathf.Abs(y)) 
                lastFaceDirection = x > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
            if(Mathf.Abs(x) < Mathf.Abs(y)) 
                lastFaceDirection = y > 0 ? new Vector2(0, 1) : new Vector2(0, -1);
        }

        foreach(var pos in positions)
        {
            pos.SetActive(false);
        }

        if(lastFaceDirection.x == 0 && lastFaceDirection.y < 0)
            positions[0].SetActive(true);
        else if(lastFaceDirection.x == 0 && lastFaceDirection.y > 0)
            positions[1].SetActive(true);
        else if(lastFaceDirection.x > 0 && lastFaceDirection.y == 0)
            positions[2].SetActive(true);
        else if(lastFaceDirection.x < 0 && lastFaceDirection.y == 0)
            positions[3].SetActive(true);
        else
            positions[0].SetActive(true);
        
    }
}
