using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;


public class AuthManager : MonoBehaviour
{

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text feedbackText;  // Geri bildirim verebilmek i�in bir Text alan�

    FirebaseAuth auth;

    void Start()
    {
        // Firebase Authentication ba�lat
        auth = FirebaseAuth.DefaultInstance;
    }

    // Kay�t olma fonksiyonu
    public void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Email ve �ifre bo� olamaz.";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Kay�t ba�ar�s�z: " + task.Exception.Message;
                Debug.LogError("Kay�t ba�ar�s�z: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            feedbackText.text = "Kay�t ba�ar�l�: " + newUser.Email;
            Debug.LogFormat("Yeni kullan�c� olu�turuldu: {0} ({1})", newUser.Email, newUser.UserId);
        });
    }


    // Giri� yapma fonksiyonu
    public void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Email ve �ifre bo� olamaz.";
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Giri� ba�ar�s�z: " + task.Exception.Message;
                Debug.LogError("Giri� ba�ar�s�z: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;
            feedbackText.text = "Giri� ba�ar�l�: " + user.Email;
            Debug.LogFormat("Kullan�c� giri� yapt�: {0} ({1})", user.Email, user.UserId);
            SceneManager.LoadScene("Game");

        });
    }

}
