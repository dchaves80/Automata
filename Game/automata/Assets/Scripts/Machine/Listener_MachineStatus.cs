using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener_MachineStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnMouseUp()
    {
        transform.parent.gameObject.SendMessage("toggleMachinePower");
    }
}
