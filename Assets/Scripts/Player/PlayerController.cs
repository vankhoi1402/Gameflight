using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float torqueAmount = 100f;
    [SerializeField] private float force = 100f;
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnBeforeForce;
    

    private static PlayerController instance;
    public static PlayerController Instance=> instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
      rb = GetComponentInParent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
       float canFlight= PlayerManager.Instance.GetFuel();
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        if(canFlight<=0) return;
        this.LauderFly();
        this.LauderRotation();
        this.PlayerSound();
    }
    void LauderFly()
    {

        if (!PlayerInputHand.Instance.IsSpace) return; 
        rb.AddForce(force * transform.up * Time.fixedDeltaTime, ForceMode2D.Impulse);
        

        OnUpForce?.Invoke(this, EventArgs.Empty);
        
        


    }
    void LauderRotation()
    {
        
        float input = PlayerInputHand.Instance.Vector2.x; // A/D hoặc mũi tên trái/phải
        if (input != 0)
        {
            
            rb.AddTorque(-input * torqueAmount * Time.fixedDeltaTime, ForceMode2D.Force);
            
            if (input < 0)
            {
                OnLeftForce?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnRightForce?.Invoke(this, EventArgs.Empty);
            }
        }
        
    }
    private bool motorPlaying = false;
    private float stopDelayTimer = 0f;

    void PlayerSound()
    {
        float input = PlayerInputHand.Instance.Vector2.x;
        bool shouldPlay = PlayerInputHand.Instance.IsSpace || input != 0;

        if (shouldPlay)
        {
            stopDelayTimer = 0f; // reset timer
            if (!motorPlaying)
            {
                AudioManager.Instance.PlayMotor("thruster");
                motorPlaying = true;
            }
        }
        else
        {
            if (motorPlaying)
            {
                stopDelayTimer += Time.deltaTime;
                if (stopDelayTimer >= 0.15f) // ví dụ delay 0.15 giây
                {
                    AudioManager.Instance.StopMotor();
                    motorPlaying = false;
                }
            }
        }
    }



}
