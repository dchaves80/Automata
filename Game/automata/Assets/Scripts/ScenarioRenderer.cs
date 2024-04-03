using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScenarioRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    public int width;
    public int height;
    public GameObject SpriteFloor;
    public GameObject SpriteWall;
    public GameObject FloorContainer;
    public GameObject item_ironOre;
    public bool buildMode = false;
    public Camera mainCamera;
    public GameObject BuildContainer;
    

    public GameObject PrefabToBuild;
    public GameObject GameObjectInstantiation;

    [Serialize]
    public List<List<Spatial>> Coordinates;
    void Start()
    {
        Coordinates = new List<List<Spatial>>();
        for (int a = 0; a < height; a++) 
        {
            Coordinates.Add(new List<Spatial>());
            for (int b = 0; b < width; b++)
            {

                GameObject GO;
                Spatial TempS;
                // GO.transform.localScale = new Vector3(10, 10, 1);

                if (a!=0 && a!=height-1 && b!=0 && b!=width-1)
                {
                    GO = Instantiate<GameObject>(SpriteFloor, new Vector3(b * 2, a * 2, 0), Quaternion.identity);
                    TempS = new Spatial() { owner = null, ocupied = false, x = b , y = a , tile = GO, passable = true };
                    GO.GetComponent<FloorRegister>().scenarioRenderer = this;
                    
                    /*Comment this IF yo eliminate random item drop*/
                    /*if (UnityEngine.Random.Range(1, 10) == 4) 
                    {
                        GameObject itm = Instantiate<GameObject>(item_ironOre);
                        itm.GetComponent<ItemBase>().AssignToSpatial(TempS);
                    }*/
                }
                else 
                {
                    GO = Instantiate<GameObject>(SpriteWall, new Vector3(b * 2, a * 2, 0), Quaternion.identity);
                    TempS = new Spatial() { owner = null, ocupied = false, x = b , y = a , tile = GO, passable = false };
                    GO.GetComponent<FloorRegister>().scenarioRenderer = this;
                }
                
                Coordinates[Coordinates.Count - 1].Add(TempS);
                GO.GetComponent<FloorRegister>().spatialData = TempS; 
                GO.transform.SetParent(FloorContainer.transform, true);

            }
        }
        
    }

    // Update is called once per frame

    public void toggleBuildMode(GameObject prefab)
    {
        buildMode = buildMode ? false : true;
        if (buildMode == true)
        {
            PrefabToBuild = prefab;
            GameObjectInstantiation = Instantiate(PrefabToBuild);

        }
        else 
        {
            Destroy(GameObjectInstantiation);
        }
    }

    void Update()
    {
        
    }
}
