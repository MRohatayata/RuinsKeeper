using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // Singleton Instance
    public EnemySpawner enemySpawner;   // Düþman doðurucu referansý
    public int currentLevel = 1;        // Þu anki seviye

    private bool canTransitionToNextLevel = false; // Seviye geçiþ kontrolü

    private void Awake()
    {
        // Singleton yapý
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþiminde yok olmasýn
        }
        else
        {
            Destroy(gameObject); // Eðer baþka bir LevelManager varsa, bu yok edilir
        }
    }

    public void StartLevel(int levelIndex)
    {
        // Seviye baþlangýç kontrolü
        if (levelIndex < 1) levelIndex = 1; // Minimum seviye kontrolü
        currentLevel = levelIndex;

        // EnemySpawner referansý kontrolü
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner referansý atanmadý!");
            return;
        }

        Debug.Log("Seviye " + currentLevel + " baþlatýlýyor.");
        canTransitionToNextLevel = false; // Geçiþ engellendi
        enemySpawner.SpawnEnemies(currentLevel);
    }

    public void AllowLevelTransition()
    {
        // Eðer düþmanlar temizlenmiþse geçiþe izin ver
        if (enemySpawner.AreEnemiesCleared())
        {
            canTransitionToNextLevel = true;
            Debug.Log("Yeni seviyeye geçiþe izin verildi.");
        }
        else
        {
            Debug.Log("Tüm düþmanlar temizlenmeden geçiþe izin verilmez.");
        }
    }

    public void GoToNextLevel()
    {
        // Sadece geçiþe izin verilmiþse seviye deðiþtir
        if (canTransitionToNextLevel)
        {
            Debug.Log("Sonraki seviyeye geçiliyor...");
            StartLevel(currentLevel + 1);
        }
        else
        {
            Debug.Log("Yeni seviyeye geçiþe izin verilmedi!");
        }
    }
}
