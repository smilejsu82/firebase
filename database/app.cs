using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class User
{
    public string username;
    public string email;

    public User()
    {
    }

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}

public class App : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(reference);

        string userId = System.Guid.NewGuid().ToString();
        string name = "홍길동";
        string email = "hong@gmail.com";
        this.WriteNewUser(userId, name, email);
    }

    private void WriteNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonConvert.SerializeObject(user);
        Debug.Log(json);
        //mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
}
