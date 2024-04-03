using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener_MachineReceipt : MonoBehaviour
{
    public void OnMouseUp()
    {
        transform.parent.gameObject.SendMessage("ChangeReceipt");
    }

}
