using UnityEngine;
using Firebase;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{
    void Start()
    {
        // Firebase ba�latma
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // Burada manuel olarak Database URL'sini ayarl�yoruz
            app.Options.DatabaseUrl = new Uri("https://yoruldum-7965b-default-rtdb.firebaseio.com/");

            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase ba�ar�yla ba�lat�ld�!");
            }
            else
            {
                Debug.LogError("Firebase ba�lat�lamad�: " + task.Result.ToString());
            }
        });
    }
}
