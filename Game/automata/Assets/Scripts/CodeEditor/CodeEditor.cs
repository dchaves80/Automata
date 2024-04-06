using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
                string commandLine = list[a];

                List<string> parameters = commandLine.Split(' ').ToList();

                string command = "";

                //si comienza con operarios condicionales + y -
                if (parameters[0].StartsWith("+") == true || parameters[0].StartsWith("-") == true)
                {
                    command = parameters[1];

                }
                else 
                {
                    command = parameters[0];
                }
                
                

                switch (command.Trim().ToUpper())
                {
                    /*Actions*/
                    case "MF": error = false; break;
                    case "MB": error = false; break;
                    case "TR": error = false; break;
                    case "TL": error = false; break;
                    case "GR": error = false; break;
                    case "DR": error = false; break;
                    case "BG": error = false; break;
                    case "WI": error = false; break;    
                    case "TO": error = false; break;

                        
                    
                    /*Logic*/

                    /*CLEARS*/
                    case "CLACC": error = false;break;
                    case "CLBAK": error = false; break;

                    /*TRANSFER VALUES*/
                    case "MOV": error = false; break;
                    case "SWP": error = false; break;

                    /*ARITHMETIC*/
                    case "ADD": error = false; break;
                    case "MUL": error = false; break;
                    case "SUB": error = false; break;
                    /*CONDITIONALS*/
                    case "EQ": error = false; break;
                    case "GT": error = false; break;
                    case "LT": error = false; break;
                    case "NE": error = false; break;
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

                    string commandLine = list[a];

                    List<string> parameters = commandLine.Split(' ').ToList();

                    string command = "";
                    int ConditionalAttribute = 0;
                    int ParameterValue = 0;

                    if (parameters[0].StartsWith("+") == true || parameters[0].StartsWith("-") == true)
                    {
                        ConditionalAttribute = parameters[0].StartsWith("+") ? 1 : 2;
                        command = parameters[1];
                        ParameterValue = parameters.Count == 3 ? int.Parse(parameters[2]) : 0;

                    }
                    else
                    {
                        command = parameters[0];
                        ParameterValue = parameters.Count == 2 ? int.Parse(parameters[1]) : 0;
                    }

                    switch (command.ToUpper().Trim())
                    {
                        /*actions*/
                        case "MF": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.forward,ConditionalAttribute)); break;
                        case "MB": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.backward,ConditionalAttribute) ); break;
                        case "TR": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.turnleft, ConditionalAttribute)); break;
                        case "TL": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.turnleft, ConditionalAttribute) { }); break;
                        case "GR": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.grab, ConditionalAttribute) { }); break;
                        case "DR": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.drop, ConditionalAttribute)); break;
                        case "BG": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.begin, ConditionalAttribute)); break;
                        case "WI": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.waititem, ConditionalAttribute)); break;
                        case "TO": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.poweroff, ConditionalAttribute)); break;
                        /*logic*/
                        case "CLACC": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_clacc, ConditionalAttribute));break;
                        case "CLBAK": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_clbak, ConditionalAttribute)); break;
                        case "MOV": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_mov, ConditionalAttribute, ParameterValue)); break;
                        case "SWP": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_swp, ConditionalAttribute)); break;
                        case "ADD": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_add, ConditionalAttribute)); break;
                        case "MUL": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_mul, ConditionalAttribute)); break;
                        case "SUB": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_sub, ConditionalAttribute)); break;
                        case "EQ": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_eq, ConditionalAttribute)); break;
                        case "GT": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_gt, ConditionalAttribute)); break;
                        case "LT": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_lt, ConditionalAttribute)); break;
                        case "NE": RB.orders.Add(new Assets.Scripts.RobotOrder(Assets.Scripts.RobotOrder.orderType.logic_ne, ConditionalAttribute)); break;



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
