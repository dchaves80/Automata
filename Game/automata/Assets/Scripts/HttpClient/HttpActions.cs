using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class HttpActions : MonoBehaviour
{
    // Start is called before the first frame update
    public string url;
    public string controller;
    public string action;



    public IEnumerator Login(string username, string password, GameObject obj)
    {
        UnityWebRequest request = UnityWebRequest.Get($"{url}/{controller}/{action}?user={username}&password={password}");
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();


        if (request.isNetworkError || request.isHttpError)
        {
            obj.GetComponent<MessageQueue>().messages.Add(new Message() { key = "login", boolData = false });
            obj.GetComponent<MessageQueue>().messages.Add(new Message() { key = "loginMessage", stringData = "Incorrect user or password" });
        }
        else
        {
            response_Login p = JsonUtility.FromJson<response_Login>(request.downloadHandler.text);
            

            if (p.error.has == false) 
            {
                obj.GetComponent<MessageQueue>().messages.Add(new Message() { key = "login", boolData = true });
                obj.GetComponent<MessageQueue>().messages.Add(new Message() { key = "loginMessage", stringData = "Welcome" }); 
            } else 
            {
                obj.GetComponent<MessageQueue>().messages.Add(new Message() { key = "login", boolData = false });
                obj.GetComponent<MessageQueue>().messages.Add(new Message() { key = "loginMessage", stringData = "Incorrect user or password" });
            }
        }

    }

    public IEnumerator SellItem(string resourceName, GameObject obj) 
    {
        
        UnityWebRequest request = UnityWebRequest.Get($"{url}/{controller}/{action}?resource={resourceName}");
        request.SetRequestHeader( "Content-Type", "application/json" );
        yield return request.SendWebRequest();
        
        
            
        
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else 
        {
            response_Price p = JsonUtility.FromJson<response_Price>( request.downloadHandler.text);
            string dataResult = "0";
            if (p.records.Count > 0) 
            {
                dataResult = p.records[0].price.ToString();
            }
            obj.GetComponent<MessageQueue>().messages.Add(new Message() { key="sell",  intData=int.Parse(dataResult)});
        }
        


    }
}
