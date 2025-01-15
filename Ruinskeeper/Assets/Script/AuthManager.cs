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
    public TMP_Text feedbackText;  // Geri bildirim verebilmek için bir Text alaný

    FirebaseAuth auth;

    void Start()
    {
        // Firebase Authentication baþlat
        auth = FirebaseAuth.DefaultInstance;
    }

    // Kayýt olma fonksiyonu
    public void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Email ve þifre boþ olamaz.";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Kayýt baþarýsýz: " + task.Exception.Message;
                Debug.LogError("Kayýt baþarýsýz: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            feedbackText.text = "Kayýt baþarýlý: " + newUser.Email;
            Debug.LogFormat("Yeni kullanýcý oluþturuldu: {0} ({1})", newUser.Email, newUser.UserId);
        });
    }


    // Giriþ yapma fonksiyonu
    public void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Email ve þifre boþ olamaz.";
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Giriþ baþarýsýz: " + task.Exception.Message;
                Debug.LogError("Giriþ baþarýsýz: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;
            feedbackText.text = "Giriþ baþarýlý: " + user.Email;
            Debug.LogFormat("Kullanýcý giriþ yaptý: {0} ({1})", user.Email, user.UserId);
            SceneManager.LoadScene("Game");

        });
    }

}
