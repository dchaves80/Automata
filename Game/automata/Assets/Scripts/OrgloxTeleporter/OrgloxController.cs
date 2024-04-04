using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEditor;


public class OrgloxController : MonoBehaviour, IBuildable
{
    
    
    public enum ShowMode { None, Build, Obstructed, PowerOff, Animation }
    public enum Mode {None, PowerOff, Build, Ready }

    // Start is called before the first frame update
    [SerializeReference]
    public GameObject PowerOff;
    public GameObject Build;
    public GameObject Obstructed;
    public GameObject Resource;
    public List<GameObject> AnimationSprite;
    public ShowMode showMode;
    public Mode mode;
    
    [Range(0.1f, 1f)]
    public float AnimationSpeed;


    private float acumulativeTimeAnimation;
    private float acumulativeTimeSpawn;

    

    //for test
    public bool test;
    public bool switchPowerOff;
    public bool switchAnimation;
    public bool switchObstructed;
    public bool switchBuild;
    public ScenarioRenderer scenarioRenderer;

    [ReadOnlyAttribute]
    [SerializeField]
    private int AnimationFrames;
    [ReadOnlyAttribute]
    [SerializeField]
    private int CurrentFrame;
    private int x;
    private int y;
    private int orientation;
    [SerializeField]
    private Spatial spatial;

    void Start()
    {
        AnimationFrames = AnimationSprite.Count;
        CurrentFrame = 0;
        orientation = 1;
        mode= Mode.None;



    }

    

    // Update is called once per frame
    void Update()
    {
        /*For test prupous*/
        if (test)
        {
            if (switchAnimation) { switchAnimation = false;SetToAnimation(); }
            if (switchObstructed) {  switchObstructed = false; SetToObstructed(); }
            if (switchBuild) { switchBuild = false; SetToBuild(); }
            if (switchPowerOff) { switchPowerOff = false; SetToPowerOff(); }

        }

        acumulativeTimeAnimation+=Time.deltaTime;
        acumulativeTimeSpawn+=Time.deltaTime;

        if (acumulativeTimeAnimation > AnimationSpeed) 
        { 
            if (showMode == ShowMode.Animation) Animate();
            acumulativeTimeAnimation = 0f;
        }

        if (acumulativeTimeSpawn > 5)
        {
            acumulativeTimeSpawn = 0;
            SpawnItem();
        }
        

        
        
    }

    private void SpawnItem() 
    {
        if (spatial.item == null && mode==Mode.Ready) 
        {
            GameObject item = Instantiate(Resource);
            item.SendMessage("AssignToSpatial", spatial);
            mode=Mode.Ready;
        }
    }

    private void Animate() 
    {
        CurrentFrame++; 
        CurrentFrame=CurrentFrame==AnimationFrames?0:CurrentFrame;
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

    public void SetToObstructed()
    {
        CurrentFrame = 0;
        SetToHide();
        showMode = ShowMode.Obstructed;
        Obstructed.SetActive(true);
        mode = Mode.Build;
    }

    public void SetToPowerOff() 
    {
        CurrentFrame = 0;
        SetToHide();
        showMode = ShowMode.PowerOff;
        PowerOff.SetActive(true);
        mode= Mode.PowerOff;
    }


    public void SetToHide() 
    {
        CurrentFrame = 0;
        showMode = ShowMode.None;
        PowerOff.SetActive(false);
        Build.SetActive(false);
        Obstructed.SetActive(false);
        for (int a=0;a<AnimationSprite.Count;a++)
        {
            AnimationSprite[a].SetActive(false);
        }
        mode = Mode.None;

    }

    public void Initialize()
    {
        throw new NotImplementedException();
    }

    public void SetToPlace()
    {
        SetToAnimation();
        mode = Mode.Ready;
    }
 


    public void SetScenarioRenderer(GameObject GO)
    {
        scenarioRenderer = GO.GetComponent<ScenarioRenderer>();
    }

    public void SetCoordinates(int[] coordinates)
    {
        x = coordinates[0]; y = coordinates[1];
    }

    public void SetSpatial(Spatial spatial_parameter)
    {
        spatial = spatial_parameter;
    }
}
