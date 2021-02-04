using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Firebase.Extensions;

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
    private DatabaseReference reference;
    public RectTransform content;
    private GameObject listItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.listItemPrefab = Resources.Load<GameObject>("ListItem");

        Debug.Log("listItemPrefab: " + listItemPrefab);

        this.reference = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log(this.reference);

        //string userId = System.Guid.NewGuid().ToString();
        //string name = "임꺽정";
        //string email = "lim@gmail.com";
        //this.WriteNewUser(userId, name, email);
        this.GetValueAsync();
    }

    private void WriteNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonConvert.SerializeObject(user);
        Debug.Log(json);
        this.reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    public void GetValueAsync()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log(task.Status);

            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...

                var json = snapshot.GetRawJsonValue();
                Debug.Log(json);

                var children = snapshot.Children;
                var enumerator = children.GetEnumerator();
                Dictionary<string, User> dic = new Dictionary<string, User>();

                while (enumerator.MoveNext()) {
                    var user = JsonConvert.DeserializeObject<User>(enumerator.Current.GetRawJsonValue());
                    var userId = enumerator.Current.Key;
                    Debug.Log(userId + " , " + user);

                    this.CreateListItem(userId, user.username, user.email);
                }
            }
        });

        
    }

    private void CreateListItem(string userId, string username, string email) {

        var go = Instantiate<GameObject>(this.listItemPrefab, this.content);
        var listItem = go.GetComponent<ListItem>();
        listItem.Init(userId, username, email);

    }
}
