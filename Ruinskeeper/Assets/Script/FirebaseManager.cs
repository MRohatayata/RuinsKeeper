using UnityEngine;
using Firebase;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{
    void Start()
    {
        // Firebase başlatma
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // Burada manuel olarak Database URL'sini ayarlıyoruz
            app.Options.DatabaseUrl = new Uri("https://yoruldum-7965b-default-rtdb.firebaseio.com/");

            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase başarıyla başlatıldı!");
            }
            else
            {
                Debug.LogError("Firebase başlatılamadı: " + task.Result.ToString());
            }
        });
    }
}
