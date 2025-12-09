using UnityEngine;

public class PlayerInputHand : MonoBehaviour
{   
    private static PlayerInputHand instance;
    public static PlayerInputHand Instance=>instance;
    private Vector2 vector2;
    public Vector2 Vector2=> vector2;
    private bool isSpace;
    public bool IsSpace=> isSpace;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        this.GetMovementInput();
        this.GetKeySpace();
    }
    private Vector2 GetMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D hoặc mũi tên trái/phải
        float moveY = Input.GetAxisRaw("Vertical");   // W/S hoặc mũi tên lên/xuống
        vector2= new Vector2(moveX, moveY).normalized;
        return vector2;
    }
    private bool GetKeySpace()
    {
        if (Input.GetKey(KeyCode.Space)) return isSpace= true;
        return isSpace= false;
    }

}
