using UnityEngine;

using Unity.Cinemachine;
using MyGame.UI.Core;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	[Header("Level Settings")]
	[Tooltip("Danh sách prefab của các level (Level1, Level2, ...)")]
	public GameObject[] levelPrefabs;
	[Tooltip("Object rỗng chứa level đang hoạt động")]
	public Transform levelParent;

	[Header("Player Settings")]
	[Tooltip("Prefab của player")]
	public GameObject playerPrefab;
	private GameObject playerInstance;

	[Header("Camera Settings")]
	[Tooltip("Camera Cinemachine theo dõi player")]
	public CinemachineCamera virtualCamera;

	[Header("Gameplay Settings")]
	public int currentLevelIndex = 0;
	private bool isLoading = false;
	

	private static LevelManager instance;
	public static LevelManager Instance => instance;

	[SerializeField]private UIScreen UILevel;


	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	private void Start()
	{
		LoadLevel(currentLevelIndex);
	}

	// ==============================
	// LOAD LEVEL
	// ==============================
	public void LoadLevel(int index)
	{
		if (isLoading) return;
		isLoading = true;

		currentLevelIndex = Mathf.Clamp(index, 0, levelPrefabs.Length - 1);
		Debug.Log($"🔄 Loading Level {currentLevelIndex}...");

		// Xóa player cũ
		if (playerInstance != null)
		{
			Destroy(playerInstance);
			playerInstance = null;
			Debug.Log("Da xoa player");
        }

		// Xóa level cũ
		foreach (Transform child in levelParent)
		{
			Destroy(child.gameObject);
		}

        // Gọi coroutine để chờ frame kế
        StartCoroutine(SpawnAfterDestroy());
    }
    
    private IEnumerator SpawnAfterDestroy()
	{
        yield return null;
        // Tạo level mới
        if (currentLevelIndex < 0 || currentLevelIndex >= levelPrefabs.Length)
		{
			Debug.LogError("❌ Level index không hợp lệ!");
			isLoading = false;
			
		}

		GameObject level = Instantiate(levelPrefabs[currentLevelIndex], levelParent);
		
		Debug.Log($"✅ Level {currentLevelIndex} đã được tạo!");

		// Tìm SpawnPoint
		Transform spawnPoint = level.transform.Find("SpawnPoint");
		if (spawnPoint == null)
		{
			Debug.LogError("❌ Không tìm thấy SpawnPoint trong level " + currentLevelIndex);
			isLoading = false;
			
		}

		// Spawn player tại SpawnPoint
		playerInstance = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        UIManager.Instance.ShowScreen(UILevel);
        Debug.Log($"✅ Player spawn tại {spawnPoint.position}");

		// Cập nhật camera follow
		if (virtualCamera != null)
		{
			virtualCamera.Follow = playerInstance.transform;
			virtualCamera.LookAt = playerInstance.transform;
		}

		Time.timeScale = 1f;
		isLoading = false;
		
	}

	// ==============================
	// KIỂM TRA LEVEL HOÀN THÀNH
	// ==============================
	public void LevelComplete()
	{
		Debug.Log($"🎉 Level {currentLevelIndex} hoàn thành!");
		 // Đợi 1.5s rồi sang màn kế tiếp
	}

	// ==============================
	// QUA LEVEL TIẾP THEO
	// ==============================
	public void NextLevel()
	{
		int next = currentLevelIndex + 1;
		if (next < levelPrefabs.Length)
		{
			
            LoadLevel(next);
			UIManager.Instance.HideAll();
		}
		else
		{
			Debug.Log("🏁 Đã hoàn thành tất cả các level!");
			// Có thể hiển thị UI thắng chung cuộc tại đây
		}
	}

	// ==============================
	// LOAD LẠI LEVEL HIỆN TẠI
	// ==============================
	public void ReloadCurrentLevel()
	{
		LoadLevel(currentLevelIndex);
	}

	// ==============================
	// HỆ THỐNG ĐIỂM (tùy chọn)
	// ==============================
	

	public void ResetScore()
	{
		
	}
}
