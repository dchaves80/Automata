using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public RobotBehavior rb;
    public GameObject wait;


    public TextMesh Data_Line;
    public TextMesh Data_Acc;
    public TextMesh Data_Back;
    public TextMesh Data_Conditional;

    void Start()
    {
        wait.SetActive(false);
        GlobalVars.global_LockMapScroll = true;

    }

    private void OnMouseExit()
    {
        GlobalVars.global_LockMapScroll = false;
        rb.menuOpened = false;
        Destroy(this.gameObject);
        
        
    }

    public void SetRobot(RobotBehavior _rb) 
    {
        Option[] optionsList = this.GetComponentsInChildren<Option>();
        for (int i = 0;  i < optionsList.Length; i++)
        {
            optionsList[i].behavior = rb;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLogicInformation();
    }

    private void UpdateLogicInformation()
    {
        Data_Line.text = rb.orderIndex.ToString();
        Data_Acc.text = rb.acc.ToString();
        Data_Back.text = rb.bak.ToString();
        Data_Conditional.text = rb.conditionalVar == 1 ? "true" : "false";
        
    }
}
