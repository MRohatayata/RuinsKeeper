using UnityEngine;
using Firebase;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{
    void Start()
    {
        // Firebase baþlatma
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // Burada manuel olarak Database URL'sini ayarlýyoruz
            app.Options.DatabaseUrl = new Uri("https://yoruldum-7965b-default-rtdb.firebaseio.com/");

            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase baþarýyla baþlatýldý!");
            }
            else
            {
                Debug.LogError("Firebase baþlatýlamadý: " + task.Result.ToString());
            }
        });
    }
}
