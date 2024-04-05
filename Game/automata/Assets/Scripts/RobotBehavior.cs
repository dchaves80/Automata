using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public List<RobotOrder> orders = new List<RobotOrder>();
    public int orderIndex = 0;
    public RobotOrder currentOrder = new RobotOrder { order = RobotOrder.orderType.nullOrder };
    public int orderProgression = 0;
    public bool power = false;
    public bool powerOff = false;
    public int X;
    public int Y;
    public ScenarioRenderer Scenario;
    public GameObject menuPrefab;
    public GameObject menuEntity;
    public bool menuOpened = false;
    public int orientation = 1;

    public bool MouseOver = false;
    public bool locked = false;
    public string program;
    public float MoveSpeed;
    public float TurnSpeed;
    public GameObject HoldItem;
    public Animator animator;

    //Control de Flujos
    [Range(1,2)]
    [DefaultValue(2)]
    public int conditionalVar;
    [Range(0,999)]
    public int acc;
    [Range(0,999)]
    public int bak;
    

    

    void Start()
    {
        //PRUEBA SPAWN
        /*
        
        int y = Scenario.height;
        int x = Scenario.width;

        
        int spawnX = UnityEngine.Random.Range(0, x - 2);
        int spawny = UnityEngine.Random.Range(0, y - 2);

        transform.position = Scenario.Coordinates[spawny][spawnX].tile.transform.position;
        transform.Translate(Vector3.back * 0.5f);
        */

        Scenario = GameObject.Find("ScenarioRender").GetComponent<ScenarioRenderer>();


    }



    // Update is called once per frame
    void Update()
    {

        //if (animator.GetBool("wipe")) animator.SetBool("wipe", false);

        

        if (locked == false)
        {
            CheckRightClick();
        }

        //PickOrder
        if (power == true)
        {

            if (currentOrder.order == RobotOrder.orderType.nullOrder)
            {
                if (orderIndex >= orders.Count)
                {
                    power = false;
                    orderIndex = 0;
                    currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder, conditional = 0};
                }
                else
                {
                    if (orders.Count > 0)
                    {
                        currentOrder = new RobotOrder(orders[orderIndex]);
                    }
                }
            }



            

            bool exec = false;
            
            switch (currentOrder.order)
            {
                case RobotOrder.orderType.begin:
                    exec = false;
                    if (currentOrder.conditional == 0){exec = true;}else{exec = (conditionalVar == currentOrder.conditional);}if (exec==true){begin();
            } else { NextOrder(); };
            break;
                case RobotOrder.orderType.forward:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {move(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.backward:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {move(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.turnleft:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {turn(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.turnright:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {turn(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.grab:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {Grab(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.drop:
                    exec = false; 
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {Drop(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.waititem:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {WaitItem(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.poweroff:
                    exec = false;
                    if (currentOrder.conditional == 0) { exec = true; } else { exec = (conditionalVar == currentOrder.conditional); }if (exec == true) {PowerOff(); } else { NextOrder(); }; break;
                case RobotOrder.orderType.poweron: PowerOn(); break;

            }
        }
    }

    private void NextOrder() 
    {
        currentOrder = new RobotOrder();
        orderIndex++;
    }
    

    private void CheckRightClick()
    {
        if (Input.GetMouseButtonUp(1) && menuOpened == false && MouseOver == true)
        {
            menuEntity = Instantiate<GameObject>(menuPrefab);
            menuEntity.transform.position = this.transform.position;
            menuEntity.transform.Translate(Vector3.back * 1.2f);
            menuEntity.GetComponent<Menu>().rb = this;
            menuEntity.GetComponent<Menu>().SetRobot(this);

        }
    }

    private void OnMouseEnter()
    {
        transform.Find("Chasis").gameObject.GetComponent<SpriteRenderer>().color = new Color() { a = 1, b = 0.8f, r = 0.8f, g = 1f };
        MouseOver = true;
    }

    private void OnMouseExit()
    {
        transform.Find("Chasis").gameObject.GetComponent<SpriteRenderer>().color = new Color() { a = 1, b = 1, r = 1f, g = 1f };
        MouseOver = false; ;
    }

    private void PowerOff() 
    {
        
       
        orderIndex = 0;
        currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
    }

    private void PowerOn() 
    {
        powerOff = false;
        power = true;
        orderIndex = 0;
        if (orders.Count > 0)
        {
            currentOrder = new  RobotOrder(orders[0]);
        }
        else 
        {
            powerOff = false;
            power = false;
            orderIndex = 0;
            currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder, conditional=0 };
        }

    }

    private void WaitItem() 
    {
        int tileX = X;
        int tileY = Y;
        switch (orientation)
        {
            case 1: tileY++; break;
            case 2: tileX++; break;
            case 3: tileY--; break;
            case 4: tileX--; break;
        }
        Spatial frontTile = Scenario.Coordinates[tileY][tileX];
        if (frontTile.item != null) 
        {
            currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
            orderIndex++;
        }
        if (powerOff == true)
        {
            powerOff = false;
            power = false;
            orderIndex = 0;
        }
    }
    private void Grab()
    {
        

        int tileX = X;
        int tileY = Y;
        switch (orientation)
        {
            case 1: tileY++; break;
            case 2: tileX++; break;
            case 3: tileY--; break;
            case 4: tileX--; break;
        }
        Spatial frontTile = Scenario.Coordinates[tileY][tileX];
        if (frontTile.item != null && HoldItem==null) 
        {
            animator.SetTrigger("TriggerWipe");
            HoldItem = frontTile.item;
            frontTile.setFree();
            HoldItem.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.1f);
            HoldItem.transform.rotation = new Quaternion(0f,0f, 0f, 0f);
            HoldItem.transform.parent = this.transform;
            currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
        }
        currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
        orderIndex++;
        orderProgression = 0;
        if (powerOff == true)
        {
            powerOff = false;
            power = false;
            orderIndex = 0;
        }

    }

    private void Drop() 
    {

        int tileX = X;
        int tileY = Y;
        switch (orientation)
        {
            case 1: tileY++; break;
            case 2: tileX++; break;
            case 3: tileY--; break;
            case 4: tileX--; break;
        }
        Spatial frontTile = Scenario.Coordinates[tileY][tileX];
        if (frontTile.item == null && HoldItem!=null)
        {

            if (frontTile.machine == null)
            {
                //En el caso de que NO exista una maquina cuando suelta el item
                animator.SetTrigger("TriggerWipe");
                frontTile.setItem(HoldItem);
                HoldItem = null;
                frontTile.item.transform.position = new Vector3(frontTile.tile.transform.position.x, frontTile.tile.transform.position.y, -0.1f);
                frontTile.item.transform.parent = null;
                frontTile.item.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else 
            {
                BaseMachine bm = frontTile.machine.GetComponent<BaseMachine>();
              
                bool control = false;

                if  (bm != null && bm.power==true && bm.ItemIn==null){
                    animator.SetTrigger("TriggerWipe");
                    //En el caso de que SI exista una maquina cuando suelta el item
                    bm.ItemIn = HoldItem;
                    bm.ItemIn.SetActive(false);
                    bm.ItemIn.transform.parent = bm.transform;
                    HoldItem = null;
                    
                    control=true;
                }

                SellerController seller = frontTile.machine.GetComponent<SellerController>();
                if (control == false && seller!=null) 
                {
                    animator.SetTrigger("TriggerWipe");
                    frontTile.setItem(HoldItem);
                    HoldItem = null;
                    frontTile.item.transform.position = new Vector3(frontTile.tile.transform.position.x, frontTile.tile.transform.position.y, -0.1f);
                    frontTile.item.transform.parent = null;
                    frontTile.item.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

                }

                



            }

        }
        currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
        orderIndex++;
        orderProgression = 0;
        if (powerOff == true)
        {
            powerOff = false;
            power = false;
            orderIndex = 0;
        }

    }



    private void begin()
    {
    
        orderProgression = 0;
        orderIndex = 0;
        currentOrder = new RobotOrder(orders[0]);
       
    }

    private void move()
    {
        bool stopOrder = false;
        //ChequearMovimiento Limites paredes y maquinas
        if (orderProgression == 0)
        {
            int newX = X;
            int newY = Y;
            if (currentOrder.order == RobotOrder.orderType.forward)
            {
                switch (orientation)
                {
                    case 1: newY++; break;
                    case 2: newX++; break;
                    case 3: newY--; break;
                    case 4: newX--; break;
                }
            }
            if (currentOrder.order == RobotOrder.orderType.backward)
            {
                switch (orientation)
                {
                    case 1: newY--; break;
                    case 2: newX--; break;
                    case 3: newY++; break;
                    case 4: newX++; break;
                }
            }

            Spatial newTile = Scenario.Coordinates[newY][newX];
            Spatial thisTile = Scenario.Coordinates[Y][X];



            if (!newTile.ocupied && newTile.passable)
            {
                thisTile.setFree();
                X = newX;
                Y = newY;
                newTile.setRobot(gameObject);

            }
            else
            {
                stopOrder = true;
                powerOff = true;
            }

        }

        if (orderProgression > (2 / MoveSpeed) - MoveSpeed || stopOrder == true)
        {
            orderProgression = 0;
            orderIndex++;
            currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
            if (powerOff == true)
            {
                powerOff = false;
                power = false;
                orderIndex = 0;
            }
        }
        else
        {
            orderProgression++;
            switch (currentOrder.order)
            {
                case RobotOrder.orderType.forward: transform.Translate(Vector3.up * MoveSpeed); break;
                case RobotOrder.orderType.backward: transform.Translate(Vector3.up * -MoveSpeed); break;
            }

        }


    }

 
    private void turn()
    {

        if (orderProgression > (90 / TurnSpeed))
        {
            orderProgression = 0;
            currentOrder = new RobotOrder() { order = RobotOrder.orderType.nullOrder };
            orderIndex++;

            //Arreglo y salto para que quede con el angulo justo
            switch (orientation)
            {
                case 1: transform.rotation = Quaternion.AngleAxis(0f, Vector3.back); break;
                case 2: transform.rotation = Quaternion.AngleAxis(90f, Vector3.back); break;
                case 3: transform.rotation = Quaternion.AngleAxis(180f, Vector3.back); break;
                case 4: transform.rotation = Quaternion.AngleAxis(270f, Vector3.back); break;
            }
            //fin de arreglo de angulo segun orientacion

            //Comienza secuencia de poweroff
            if (powerOff == true)
            {

                powerOff = false;
                power = false;
                orderIndex = 0;
            }
        }
        else
        {
            orderProgression++;
            switch (currentOrder.order)
            {
                case RobotOrder.orderType.turnleft: transform.Rotate(Vector3.forward * TurnSpeed); fixOrientation(-1); break;
                case RobotOrder.orderType.turnright: transform.Rotate(Vector3.forward * -TurnSpeed); fixOrientation(1); break;
            }
        }

    }

    private void fixOrientation(int o)
    {
        if (orderProgression == 1)
        {
            orientation += o;
            orientation = orientation < 1 ? 4 : orientation;
            orientation = orientation > 4 ? 1 : orientation;
        }
    }
}
