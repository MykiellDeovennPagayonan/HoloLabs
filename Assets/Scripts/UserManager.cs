using UnityEngine;
using Firebase.Auth;

public class UserManager : MonoBehaviour
{
    public static UserManager instance;

    public string userID;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserID(string uid)
    {
        userID = uid;
    }

    public string getUserID()
    {
        return userID;
    }
}
