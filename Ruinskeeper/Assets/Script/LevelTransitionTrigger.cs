using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Karakter platforma girdi.");
            LevelManager.Instance.AllowLevelTransition(); // Ge�i� kontrol�
            LevelManager.Instance.GoToNextLevel();       // Yeni seviyeye ge�i�
        }
    }

}
