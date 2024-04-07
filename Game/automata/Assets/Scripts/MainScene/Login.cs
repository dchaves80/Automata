using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MessageQueue))]


public class Login : MonoBehaviour
{

    public Text user;
    public Text password;
    public Text message;
    public HttpActions httpActions;
    public MessageQueue MQ;
    public Button buttonEnter;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MQ.messages != null && MQ.messages.Count == 2) 
        {
            CheckMessageQueue();
        }
    }

    public void CheckMessageQueue() 
    {
            Color c = MQ.messages[0].boolData==true?Color.green:Color.red;
            message.color = c;
            message.text = MQ.messages[1].stringData;
            MQ.messages.Clear();
            buttonEnter.interactable = true;
    }

    public void SendLogin() 
    {
        buttonEnter.interactable = false;
        StartCoroutine(httpActions.Login(user.text,password.text,gameObject));
    }

    public void ExitGame()
    {
        Application.Quit();
    }


   
}
