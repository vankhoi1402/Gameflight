using UnityEngine;



public class Fuel : MonoBehaviour, ICollectible
{
    [SerializeField] private float amount = 100f;

    public void OnCollected(PlayerManager manager )
    {
        manager.AddFuel(amount);
        AudioManager.Instance.PlaySFX("fuel");
        Destroy(gameObject);
    }
}
