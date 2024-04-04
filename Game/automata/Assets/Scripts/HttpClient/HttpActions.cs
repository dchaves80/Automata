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
            obj.GetComponent<MessageQueue>().messages.Add(new Message() { key="sell",  data=dataResult});
        }
        


    }
}
