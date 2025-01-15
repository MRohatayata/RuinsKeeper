using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // Singleton Instance
    public EnemySpawner enemySpawner;   // D��man do�urucu referans�
    public int currentLevel = 1;        // �u anki seviye

    private bool canTransitionToNextLevel = false; // Seviye ge�i� kontrol�

    private void Awake()
    {
        // Singleton yap�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�iminde yok olmas�n
        }
        else
        {
            Destroy(gameObject); // E�er ba�ka bir LevelManager varsa, bu yok edilir
        }
    }

    public void StartLevel(int levelIndex)
    {
        // Seviye ba�lang�� kontrol�
        if (levelIndex < 1) levelIndex = 1; // Minimum seviye kontrol�
        currentLevel = levelIndex;

        // EnemySpawner referans� kontrol�
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner referans� atanmad�!");
            return;
        }

        Debug.Log("Seviye " + currentLevel + " ba�lat�l�yor.");
        canTransitionToNextLevel = false; // Ge�i� engellendi
        enemySpawner.SpawnEnemies(currentLevel);
    }

    public void AllowLevelTransition()
    {
        // E�er d��manlar temizlenmi�se ge�i�e izin ver
        if (enemySpawner.AreEnemiesCleared())
        {
            canTransitionToNextLevel = true;
            Debug.Log("Yeni seviyeye ge�i�e izin verildi.");
        }
        else
        {
            Debug.Log("T�m d��manlar temizlenmeden ge�i�e izin verilmez.");
        }
    }

    public void GoToNextLevel()
    {
        // Sadece ge�i�e izin verilmi�se seviye de�i�tir
        if (canTransitionToNextLevel)
        {
            Debug.Log("Sonraki seviyeye ge�iliyor...");
            StartLevel(currentLevel + 1);
        }
        else
        {
            Debug.Log("Yeni seviyeye ge�i�e izin verilmedi!");
        }
    }
}
