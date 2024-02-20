using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public InputField email, password;
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });

    }

    public void onClickLogin()
    {
        string emailString = email.text;
        string passwordString = password.text;

        Debug.Log("Logging in");
        auth.SignInWithEmailAndPasswordAsync(emailString, passwordString).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            UserManager.instance.SetUserID(result.User.UserId);
        });

    }

    public void onClickRegister()
    {
        string emailString = email.text;
        string passwordString = password.text;

        Debug.Log("Registering in");
        auth.CreateUserWithEmailAndPasswordAsync(emailString, passwordString).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            UserManager.instance.SetUserID(result.User.UserId);
        });

    }

    public void GetUserId()
    {
        string userID = UserManager.instance.getUserID();
        Debug.Log(userID);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("DatabaseTest");
    }
}
