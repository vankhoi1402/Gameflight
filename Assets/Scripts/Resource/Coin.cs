using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    [SerializeField] private int value = 10;

    public void OnCollected(PlayerManager manager)
    {
        GameManager.Instance.addScore(value);
        AudioManager.Instance.PlaySFX("coin");
        Destroy(gameObject);
    }
}
