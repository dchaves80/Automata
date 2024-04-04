using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CodeEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Robot;
    public TMP_InputField FieldCode;
    public TMP_InputField FieldRobotName;
    void Start()
    {
  
       


    }

    public void init() 
    {
       /* FieldCode = GameObject.Find("InputFieldCode").GetComponent<TMP_InputField>();
        FieldRobotName = GameObject.Find("InputFieldRobotName").GetComponent<TMP_InputField>();*/
        Robot.GetComponent<RobotBehavior>().powerOff = true;
        Robot.GetComponent<RobotBehavior>().locked = true;
        FieldCode.text = Robot.GetComponent<RobotBehavior>().program;
        Robot.GetComponent<RobotBehavior>().currentOrder = new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.nullOrder };
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    public void SaveCode()
    {
        if (CheckForEntities())
        {
            List<String> list = FieldCode.text.Split('\n').ToList();
            bool error = false;
            List<int> lineserror = new List<int>();
            List<string> listerror = new List<string>();
            for (int a = 0; a < list.Count; a++)
            {

                switch (list[a].ToUpper().Trim())
                {
                    case "MF": error = false; break;
                    case "MB": error = false; break;
                    case "TR": error = false; break;
                    case "TL": error = false; break;
                    case "GR": error = false; break;
                    case "DR": error = false; break;
                    case "BG": error = false; break;
                    case "WI": error = false; break;    
                    case "TO": error = false; break;
                    default:error = true; break;

                }

                if (error == true) { lineserror.Add(a + 1); listerror.Add("Unknown Command!");  }
            }
            //not errors in code
            if (lineserror.Count == 0)
            {

                RobotBehavior RB = Robot.GetComponent<RobotBehavior>();
                RB.orders.Clear();
                RB.program = FieldCode.text;

                for (int a = 0; a < list.Count; a++)
                {

                    switch (list[a].ToUpper().Trim())
                    {
                        case "MF": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.forward }); break;
                        case "MB": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.backward }); break;
                        case "TR": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.turnright }); break;
                        case "TL": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.turnleft }); break;
                        case "GR": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.grab }); break;
                        case "DR": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.drop }); break;
                        case "BG": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.begin }); break;
                        case "WI": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.waititem }); break;
                        case "TO": RB.orders.Add(new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.poweroff }); break;
                    }


                }

                


            }
            else 
            {
                
            }
        }

      

    }

    public void Exit()
    {
        Robot.GetComponent<RobotBehavior>().currentOrder = new Assets.Scripts.RobotOrder() { order = Assets.Scripts.RobotOrder.orderType.nullOrder };
    
        Robot.GetComponent<RobotBehavior>().locked = false;
        Robot.GetComponent<RobotBehavior>().powerOff = false;
        Destroy(this.gameObject);
    }

    private bool CheckForEntities()
    {
        if (FieldCode == true && FieldRobotName == true) return true;

        return false;
    }
}
