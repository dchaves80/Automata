using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;


public class BaseMachine : MonoBehaviour, IBuildable
{
    // Start is called before the first frame update

    public enum Status { powerOff, obstructed, ok, working }

    
    public MachineConfiguration Configuration;
    public int animationIndex = 0;
    public float acumulativeTime;
    public Receipt CurrentReceipt;
    public int receiptIndex;
    bool HasAnimation;
    List<Transform> Animations;
    List<Transform> Progress;
    public int orientation;
    public int width;
    public int height;
    public bool buildable = true;
    public bool power = false;
    public int X;
    public int Y;
    public ScenarioRenderer ScenarioRendererObject;
    public Status CurrentStatus;
    public int work;

    //modificar si va a requerir de mas items por una lista
    public GameObject ItemIn = null;
    public GameObject ItemOut = null;

    public const string st_poweroff = "factory_status_poweroff";
    public const string st_statusok = "factory_status_ok";
    public const string st_obstructed = "factory_status_obstructed";
    public const string st_working = "factory_status_working";


    [SerializeField]
    private Spatial spatial;



    public virtual void Start()
    {

        Transform animationContainer = this.transform.Find("Animation");
        Transform progressBarContainer = this.transform.Find("ProgressBar");
        ChangeReceiptInternal(0);

        work = 0;

        int numberOfChildrens = animationContainer.childCount;
        
        if (numberOfChildrens > 0) 
        {

            Animations = new List<Transform>();
            HasAnimation = true;
            for (int a =0;a< numberOfChildrens;a++)
            {
                Animations.Add(animationContainer.GetChild(a));
            }
        }
        Progress = new List<Transform>();
        for (int a = 0; a < progressBarContainer.childCount; a++) 
        {
            Progress.Add(progressBarContainer.GetChild(a));
        }

        
    }

    

    // Update is called once per frame
    public virtual void Update()
    {
        acumulativeTime += Time.deltaTime;
        


        if (acumulativeTime > 0.25f) 
        {
            calculateAnimation();
            acumulativeTime = 0f;

        }
        if (power) CheckMaterials();
        if (CurrentStatus == Status.working) 
        {
            Fabricate();    
        }
    }


    private void Fabricate() 
    {
        work++;
        UpdateProgressBar();
        if (work == CurrentReceipt.ResourcesOut[0].GetComponent<ItemBase>().requiredWork) 
        {
            HideAllProgressBar();
            ChangeStatus(Status.ok);
            ActivateFactoryUI();
            ItemOut = Instantiate(CurrentReceipt.ResourcesOut[0]);
            int frontX = X;
            int frontY = Y;
            if (orientation == 1) frontY++;
            if (orientation == 2) frontX++;
            if (orientation == 3) frontY--;
            if (orientation == 4) frontX--;

            Spatial frontTile = ScenarioRendererObject.Coordinates[frontY][frontX];
            if (frontTile.passable == true && frontTile.ocupied == false)
            {
                GameObject newItem = Instantiate(CurrentReceipt.ResourcesOut[0]);
                Vector3 tilePosition = frontTile.tile.transform.position;
                newItem.transform.position = new Vector3(tilePosition.x, tilePosition.y, -0.1f);
                frontTile.item = newItem;
                ChangeStatus(Status.ok);
                HideAllProgressBar();
                work = 0;
            }
            else 
            {
                ChangeStatus(Status.obstructed);
            }


        }
    }

    private void HideAllProgressBar() 
    {
        for (int a=0;a<Progress.Count;a++)
        {
            Progress[a].gameObject.SetActive(false);
        }
    }

    private void UpdateProgressBar() 
    {
        float floatingProgress = (work * 100) / CurrentReceipt.ResourcesOut[0].GetComponent<ItemBase>().requiredWork;
        HideAllProgressBar();
        if (floatingProgress >= 0 && floatingProgress < 25) Progress[0].gameObject.SetActive(true);
        if (floatingProgress >= 25 && floatingProgress < 50) Progress[1].gameObject.SetActive(true);
        if (floatingProgress >= 50 && floatingProgress < 75) Progress[2].gameObject.SetActive(true);
        if (floatingProgress >= 75 && floatingProgress < 90) Progress[3].gameObject.SetActive(true);
        if (floatingProgress >= 95) Progress[4].gameObject.SetActive(true);
    }


    public void DeactivateFactoryUI() 
    {
        transform.Find("Receipt").GetComponent<BoxCollider2D>().enabled = false;
        transform.Find("SpritesStatus").GetComponent<BoxCollider2D>().enabled = false;

    }
    public void ActivateFactoryUI()
    {
        transform.Find("Receipt").GetComponent<BoxCollider2D>().enabled = true;
        transform.Find("SpritesStatus").GetComponent<BoxCollider2D>().enabled = true;

    }


    private void CheckMaterials() 
    {
        if (ItemIn != null) 
        {
            string nameRequiredItem = CurrentReceipt.ResourcesIn[0].GetComponent<ItemBase>().Name;
            string nameInItem = ItemIn.GetComponent<ItemBase>().Name;
            if (nameRequiredItem == nameInItem) 
            {
                
                ChangeStatus(Status.working);
                Destroy(ItemIn);
                ItemIn = null;
            }
        }
    }

    private void ChangeStatus(Status status) 
    {
        CurrentStatus = status;

        switch (status) 
        {
            case Status.working:changeStatusSprite(st_working); break;
            case Status.ok: changeStatusSprite(st_statusok); break;
            case Status.powerOff: changeStatusSprite(st_poweroff); break;
            case Status.obstructed: changeStatusSprite(st_obstructed); break;

        }
        
        
    }

    private void calculateAnimation()
    {
        if (HasAnimation) { 
        
        for (int a = 0; a < Animations.Count; a++) 
        {
            if (animationIndex==a) { Animations[a].gameObject.SetActive(true); } else { Animations[a].gameObject.SetActive(false); }
        }
            animationIndex++;
            if (animationIndex == Animations.Count) animationIndex = 0;
        }
    }
    
    public void Rotate() 
    {
        orientation++;
        orientation = orientation==5 ? 1 : orientation;
        switch (orientation) 
        {
            case 1: transform.rotation = Quaternion.AngleAxis(0f, Vector3.back); break;
            case 2: transform.rotation = Quaternion.AngleAxis(90f, Vector3.back); break;
            case 3: transform.rotation = Quaternion.AngleAxis(180f, Vector3.back); break;
            case 4: transform.rotation = Quaternion.AngleAxis(270f, Vector3.back); break;
        
        }
        
    }


    public void SetToHide() 
    {
        transform.Find("BuildSprite").gameObject.SetActive(false);
        transform.Find("ObstructedBuild").gameObject.SetActive(false);
    }

    public void SetToBuild() 
    {
        SetToHide();
        transform.Find("BuildSprite").gameObject.SetActive(true);
        transform.Find("ObstructedBuild").gameObject.SetActive(false);
    }

    public void SetToObstructed() 
    {
        transform.Find("BuildSprite").gameObject.SetActive(false);
        transform.Find("ObstructedBuild").gameObject.SetActive(true);
    }

    public void toggleMachinePower() 
    {
        power = power==true?false:true;
        if (power)
        {
            this.transform.Find("Animation").gameObject.SetActive(true);
            this.transform.Find("power_off_animation_sprite").gameObject.SetActive(false);
            changeStatusSprite(st_statusok);
            CurrentStatus = Status.ok;
            
            
        }
        else 
        {
            this.transform.Find("Animation").gameObject.SetActive(false);
            this.transform.Find("power_off_animation_sprite").gameObject.SetActive(true);
            changeStatusSprite(st_poweroff);
            CurrentStatus = Status.powerOff;
        }
    }

    private void ChangeReceiptInternal(int index) 
    {

        CurrentReceipt = Configuration.receiptList[index];
        SpriteRenderer sr = transform.Find("Receipt").Find("spriteReceipt").gameObject.GetComponent<SpriteRenderer>();
        Sprite spr = CurrentReceipt.ResourcesOut[0].transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
        sr.sprite = spr;
       

        
    }

    public void ChangeReceipt() 
    {
        receiptIndex++;
        if (receiptIndex == Configuration.receiptList.Count) receiptIndex = 0;
        ChangeReceiptInternal(receiptIndex);
    }

    private void changeStatusSprite(string name) 
    {
        for (int a = 0; a < transform.Find("SpritesStatus").childCount; a++) 
        {
            transform.Find("SpritesStatus").GetChild(a).gameObject.SetActive(false);
        }
        transform.Find("SpritesStatus").Find(name).gameObject.SetActive(true);
        
    }

    

    public void SetToAnimation()
    {
        throw new NotImplementedException();
    }

    public void Initialize()
    {
        throw new NotImplementedException();
    }

    public void SetToPlace()
    {
        transform.Find("BuildSprite").gameObject.SetActive(false);
        transform.Find("MainSprite").gameObject.SetActive(true);
        transform.Find("power_off_animation_sprite").gameObject.SetActive(true);
        transform.Find("SpritesStatus").Find("factory_status_poweroff").gameObject.SetActive(true);
        transform.Find("SpritesStatus").GetComponent<BoxCollider2D>().enabled = true;
        transform.Find("Receipt").gameObject.SetActive(true);
        transform.Find("Receipt").GetComponent<BoxCollider2D>().enabled = true;
     
    }

    

    public void SetScenarioRenderer(GameObject GO)
    {
        ScenarioRendererObject = GO.GetComponent<ScenarioRenderer>();
    }

    public void SetCoordinates(int[] coordinates)
    {
        X = coordinates[0]; Y = coordinates[1];
    }

    public void SetSpatial(Spatial spatial_parameter)
    {
        
        spatial = spatial_parameter;
    }
}
