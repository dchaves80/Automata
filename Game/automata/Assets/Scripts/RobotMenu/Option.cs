using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    // Start is called before the first frame update
    public RobotOrder.orderType orderType;
    public SpriteRenderer pointRender;
    public RobotBehavior behavior;
    public Menu menu=null;
    public GameObject AssetCodeEditor;
    void Start()
    {
        pointRender = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        pointRender.enabled = true;
    }

    private void OnMouseExit()
    {
        pointRender.enabled = false;
    }

    

    private void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0) && behavior.orderProgression == 0)
        {
            behavior.currentOrder = new RobotOrder() { order = orderType };
            switch (orderType)
            {
                case RobotOrder.orderType.console: OpenConsole(); break;
                case RobotOrder.orderType.poweroff: behavior.powerOff = true; break;
                default: behavior.power = true; behavior.powerOff = true; break;

            }
        }
        else
        {
            switch (orderType)
            {
                case RobotOrder.orderType.poweroff: behavior.powerOff = true; break;

            }
        }
    }

    private void OpenConsole() 
    {
        GameObject GOCe = Instantiate(AssetCodeEditor);
        CodeEditor cec = GOCe.GetComponent<CodeEditor>();
        cec.Robot = behavior.gameObject;
        cec.init();
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (menu == null) 
        {
            menu = transform.GetComponentInParent<Menu>();
        }
    }
}
