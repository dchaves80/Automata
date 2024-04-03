using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorRegister : MonoBehaviour
{
    // Start is called before the first frame update
    public Spatial spatialData;
    public ScenarioRenderer scenarioRenderer;

    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D myCollider;
    private bool MouseIn;

    public void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }


    public void OnMouseUp()
    {
        if (scenarioRenderer.buildMode == true && spatialData.ocupied == false && spatialData.passable==true)
        {
            scenarioRenderer.buildMode = false;
            /*
            scenarioRenderer.GameObjectInstantiation.transform.Find("BuildSprite").gameObject.SetActive(false);
            scenarioRenderer.GameObjectInstantiation.transform.Find("MainSprite").gameObject.SetActive(true);
            scenarioRenderer.GameObjectInstantiation.transform.Find("power_off_animation_sprite").gameObject.SetActive(true);
            scenarioRenderer.GameObjectInstantiation.transform.Find("SpritesStatus").Find("factory_status_poweroff").gameObject.SetActive(true);
            scenarioRenderer.GameObjectInstantiation.transform.Find("SpritesStatus").GetComponent<BoxCollider2D>().enabled = true;
            scenarioRenderer.GameObjectInstantiation.transform.Find("Receipt").gameObject.SetActive(true);
            scenarioRenderer.GameObjectInstantiation.transform.Find("Receipt").GetComponent<BoxCollider2D>().enabled = true;
            scenarioRenderer.GameObjectInstantiation.GetComponent<BaseMachine>().X = this.spatialData.x;
            scenarioRenderer.GameObjectInstantiation.GetComponent<BaseMachine>().Y = this.spatialData.y;
            
            */
            scenarioRenderer.GameObjectInstantiation.SendMessage("SetToPlace", new int[] { this.spatialData.x, this.spatialData.y });
            scenarioRenderer.GameObjectInstantiation.SendMessage("SetScenarioRenderer", scenarioRenderer.gameObject);
            scenarioRenderer.GameObjectInstantiation.SendMessage("SetSpatial",this.spatialData);
            scenarioRenderer.GameObjectInstantiation.SendMessage("SetCoordinates", new int[] { this.spatialData.x, this.spatialData.y });
            mySpriteRenderer.color = Color.white;
            this.spatialData.machine = scenarioRenderer.GameObjectInstantiation;
            this.spatialData.ocupied = true;
            this.spatialData.passable = false;
            scenarioRenderer.GameObjectInstantiation = null;
            MouseIn = false;

        }
    }

    public void OnMouseEnter()
    {
        if (scenarioRenderer.buildMode == true && spatialData.passable == true)
        {

            MouseIn = true;

    scenarioRenderer.GameObjectInstantiation.transform.position = new Vector3(transform.position.x, transform.position.y, -0.01f);
            if (!spatialData.ocupied)
            {
                scenarioRenderer.GameObjectInstantiation.SendMessage("SetToBuild");
            }
            else
            {
                scenarioRenderer.GameObjectInstantiation.SendMessage("SetToObstructed");
            }

        }
    }

    public void OnMouseExit()
    {
        if (scenarioRenderer.buildMode == true)
        {
            MouseIn = false;
            mySpriteRenderer.color = Color.white;
        }
    }


    public void Update()
    {
        if (MouseIn) switch (scenarioRenderer.buildMode)
        {
            case true:
                CheckRotationAction();
                break;
        }
    }


    private void CheckRotationAction()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            scenarioRenderer.GameObjectInstantiation.SendMessage("Rotate");
        }
    }
}
