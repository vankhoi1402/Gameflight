using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sound Data")]
public class SoundData : ScriptableObject
{
    public string id; // Tên định danh (vd: "Coin", "Explosion", "Theme")
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop;
}
