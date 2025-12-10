using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField]private float moveUpSpeed = 1f;
    [SerializeField]private float fadeSpeed = 1f;
    private TextMeshPro text;
    private Color color;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        color = text.color;
    }

    void Update()
    {
        // Bay lên
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        // Mờ dần
        color.a -= fadeSpeed * Time.deltaTime;
        text.color = color;

        // Biến mất
        if (color.a <= 0)
            Destroy(gameObject);
    }

    public void SetText(string value)
    {
        text.text = value;
    }
}
