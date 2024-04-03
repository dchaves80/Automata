using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


[RequireComponent(typeof(MessageQueue))]
public class SellerController : MonoBehaviour, ISeller
{

    public enum ShowMode { None, Build, Obstructed, Animation }
    public enum Mode { None, PowerOff, Build, Ready }

    public GameObject Build;
    public GameObject Obstructed;
    public List<GameObject> AnimationSprite;
    public GameObject SellUIPrefab;
    [Range(0.1f, 1f)]
    public float AnimationSpeed;

    public HttpActions httpActions;
    public MessageQueue MQ;

    ScenarioRenderer scenarioRenderer;
    int orientation;
    int X;
    int Y;
    private Spatial spatial;
    private int CurrentFrame;
    private ShowMode showMode;
    private Mode mode;
    int AnimationFrames;
    private float acumulativeTimeAnimation;
    private float acumulativeTimeSell;
    

    void Start()
    {
        orientation = 1;
        AnimationFrames = AnimationSprite.Count;
        httpActions = GameObject.Find("WebClient").GetComponent<HttpActions>();
    }

    // Update is called once per frame
    void Update()
    {


        acumulativeTimeAnimation += Time.deltaTime;
        acumulativeTimeSell += Time.deltaTime;

        if (acumulativeTimeAnimation > AnimationSpeed)
        {
            if (showMode == ShowMode.Animation) Animate();
            acumulativeTimeAnimation = 0f;
        }

        if (acumulativeTimeSell > 1f) 
        {
            acumulativeTimeSell = 0f;
            if (spatial!=null && spatial.item!=null)Sell();
        }
        checkMessages();


    }

    void checkMessages() 
    {
        if (MQ.messages.Count > 0) { 
        
        if (MQ.messages[0].key == "sell") { 
        GameObject nreGO = Instantiate(SellUIPrefab);
        nreGO.transform.Find("sprite").gameObject.GetComponent<TextMesh>().text = MQ.messages[0].data;
        nreGO.transform.position = transform.position + new Vector3(0f, 0f, -0.2f);
            }
            MQ.messages.Remove(MQ.messages[0]);

        }
    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }


    private void Animate()
    {
        CurrentFrame++;
        CurrentFrame = CurrentFrame == AnimationFrames ? 0 : CurrentFrame;
        int PreviousFrame = CurrentFrame - 1;
        PreviousFrame = PreviousFrame == -1 ? AnimationFrames - 1 : PreviousFrame;
        AnimationSprite[PreviousFrame].SetActive(false);
        AnimationSprite[CurrentFrame].SetActive(true);

    }


    public void Rotate()
    {
        orientation++;
        orientation = orientation == 5 ? 1 : orientation;
        switch (orientation)
        {
            case 1: transform.rotation = Quaternion.AngleAxis(0f, Vector3.back); break;
            case 2: transform.rotation = Quaternion.AngleAxis(90f, Vector3.back); break;
            case 3: transform.rotation = Quaternion.AngleAxis(180f, Vector3.back); break;
            case 4: transform.rotation = Quaternion.AngleAxis(270f, Vector3.back); break;

        }
    }

    public void Sell()
    {
        //VENDER
        
        StartCoroutine(httpActions.SellItem(spatial.item.GetComponent<ItemBase>().Name,this.gameObject));
        Destroy(spatial.item);
        spatial.setFree();
    }

    public void SetCoordinates(int[] coordinates)
    {
        X = coordinates[0]; Y = coordinates[1];
    }

    public void SetScenarioRenderer(GameObject GO)
    {
        scenarioRenderer = GO.GetComponent<ScenarioRenderer>();
    }

    public void SetSpatial(Spatial spatial_parameter)
    {
        spatial = spatial_parameter;
    }

    public void SetToAnimation()
    {
        CurrentFrame = 0;
        SetToHide();
        showMode = ShowMode.Animation;
    }

    public void SetToBuild()
    {
        CurrentFrame = 0;
        SetToHide();
        showMode = ShowMode.Build;
        Build.SetActive(true);
        mode = Mode.Build;
    }

    public void SetToHide()
    {
        CurrentFrame = 0;
        showMode = ShowMode.None;
        Build.SetActive(false);
        Obstructed.SetActive(false);
        for (int a = 0; a < AnimationSprite.Count; a++)
        {
            AnimationSprite[a].SetActive(false);
        }
        mode = Mode.None;
        
    }

    public void SetToObstructed()
    {
        CurrentFrame = 0;
        SetToHide();
        showMode = ShowMode.Obstructed;
        Obstructed.SetActive(true);
        mode = Mode.Build;
    }



    public void SetToPlace()
    {
        SetToAnimation();
        mode = Mode.Ready;
    }

    // Use this for initialization

}
