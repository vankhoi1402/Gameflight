using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    // Singleton
    public static PlayerManager Instance { get; private set; }

    [Header("Resources")]
    [SerializeField] private float fuelAmount = 100;
    

    public float Fuel { get; private set; }
    public float currentFuel { get; private set; }


    // Sự kiện: bắn ra mỗi khi resource thay đổi
    public event Action<float> OnFuelChanged;
    public event EventHandler Ondie;



    private void Awake()
    {
        // Thiết lập Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // tránh trùng

        // Khởi tạo giá trị ban đầu
        Fuel = fuelAmount;
        
    }

    // ===== Fuel =====
    public void AddFuel(float amount)
    {
        Fuel = Mathf.Clamp(Fuel + amount, 0, fuelAmount);
        Debug.Log("Fuel: " + Fuel);

        // Bắn event để UI / script khác cập nhật
        OnFuelChanged?.Invoke(Fuel);
    }
    private void Update()
    {
        this.ConsumeFuel();
        this.GetCurrentFuel();
    }
    private float ConsumeFuel()
    {
        if (PlayerInputHand.Instance.Vector2.x == 0 && !PlayerInputHand.Instance.IsSpace) return 0;
        float fuelConsumptionAmount = 10f;
        return Fuel -= fuelConsumptionAmount * Time.deltaTime;
    }
    private float GetCurrentFuel()
    {
        currentFuel = Fuel / fuelAmount;
        return currentFuel;
    }
    public void LauderDie()
    {

        Ondie.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }






}
