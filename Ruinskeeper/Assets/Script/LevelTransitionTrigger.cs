using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Karakter platforma girdi.");
            LevelManager.Instance.AllowLevelTransition(); // Geçiþ kontrolü
            LevelManager.Instance.GoToNextLevel();       // Yeni seviyeye geçiþ
        }
    }

}
