using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    [SerializeField] private int value = 10;
    public GameObject popupTextPrefab;   // kéo prefab vào

    public void OnCollected(PlayerManager manager)
    {
        GameManager.Instance.addScore(value);
        AudioManager.Instance.PlaySFX("coin");
        // tạo popup
        GameObject popup = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);

        // set nội dung
        PopupText popupScript = popup.GetComponentInChildren<PopupText>();
        if (popupScript != null)
        {
            string displayValue = $"+{value}";

            // Khởi tạo: Truyền nội dung và vị trí World Space để bắt đầu hiệu ứng
            // Lưu ý: Hàm Initialize này phải được định nghĩa trong script PopupText của bạn.
            popupScript.SetText(displayValue);
        }
        //popup.GetComponent<PopupText>().SetText("+hsagdhjasdhs"+value);
        Destroy(gameObject);
    }
}
