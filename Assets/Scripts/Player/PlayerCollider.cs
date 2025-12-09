using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerManager playerStats;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerManager>();
        if (playerStats == null)
            Debug.LogError("❌ Không tìm thấy PlayerManager trên " + gameObject.name);
        else
            Debug.Log("✅ PlayerManager đã được gán thành công!");
    }

    // 🟢 Va chạm xuyên qua (Item, Enemy Trigger...)
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other.gameObject);
    }

    // 🧱 Va chạm vật lý thật (Ground, Wall, Obstacle...)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    // 🎯 Xử lý chung cho cả 2 loại va chạm
    private void HandleCollision(GameObject other)
    {
        // Nếu vật thể là vật phẩm thu thập
        if (other.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.OnCollected(playerStats);
            return;
        }

        // Nếu vật thể gây sát thương
        if (other.TryGetComponent<IDamageSource>(out var damageSource))
        {
            damageSource.OnHit(playerStats);
            PlayerManager.Instance.LauderDie();
            AudioManager.Instance.PlaySFX("crash");
            return;
        }

        
    }

}
