using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] public AudioSource motorSource;

    [Header("Sound Data")]
    [SerializeField] private List<SoundData> sounds;

    private Dictionary<string, SoundData> soundDict;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float motorVolume = 1f;

    private void Awake()
    {
        // 🔒 Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 🔍 Chuyển list thành dictionary
        soundDict = sounds.ToDictionary(s => s.id, s => s);
    }
    private void Start()
    {
       // this.PlayMusic("music");
    }

    // 🎵 Phát nhạc nền
    public void PlayMusic(string id)
    {
        if (soundDict.TryGetValue(id, out SoundData data))
        {
            musicSource.clip = data.clip;
            musicSource.volume = data.volume * musicVolume * masterVolume;
            musicSource.loop = data.loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Music with id '{id}' not found!");
        }
    }
    public void PlayMotor(string id)
    {
        if (soundDict.TryGetValue(id, out SoundData data))
        {
            // Chỉ đổi clip khi khác clip cũ
            if (motorSource.clip != data.clip)
            {
                motorSource.clip = data.clip;
                motorSource.loop = data.loop;
            }

            // Chỉ Play khi chưa phát
            if (!motorSource.isPlaying)
            {
                motorSource.volume = data.volume * motorVolume * masterVolume;
                motorSource.Play();
                Debug.Log($"isPlaying={motorSource.isPlaying}, clip={motorSource.clip?.name}");
            }
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Motor with id '{id}' not found!");
        }
    }
    public void StopMotor()
    {
        
            StartCoroutine(WaitMotor());
    }
    IEnumerator WaitMotor()
    {
        if (motorSource.isPlaying)
        {
            yield return null;
            motorSource.Stop();
        }
    }
    // 🔊 Phát hiệu ứng SFX
    public void PlaySFX(string id)
    {
        if (soundDict.TryGetValue(id, out SoundData data))
        {
            float vol = Mathf.Clamp01(data.volume * sfxVolume * masterVolume);
            sfxSource.PlayOneShot(data.clip, vol);
        }
        else
        {
            Debug.LogWarning($"[AudioManager] SFX with id '{id}' not found!");
        }
    }

    // 🎚️ Cập nhật âm lượng khi chỉnh slider
    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        UpdateVolumes();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        UpdateVolumes();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        if (musicSource)
            musicSource.volume = (musicSource.clip ? soundDict[musicSource.clip.name].volume : 1f) * musicVolume * masterVolume;
    }
}
