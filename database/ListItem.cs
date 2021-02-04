using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    public Text txtUserId;
    public Text txtUserName;
    public Text txtEmail;

    public void Init(string userId, string userName, string email)
    {
        this.txtUserId.text = userId;
        this.txtUserName.text = userName;
        this.txtEmail.text = email;
    }
}
