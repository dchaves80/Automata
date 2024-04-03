using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int serverValue;
    public int requiredWork;
    public Sprite sprite;
    public string Name;
    void Start()
    {
        SpriteRenderer itemSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        itemSprite.sprite = sprite;
    }

    public void AssignToSpatial(Spatial s) 
    {
        if (s.item == null) 
        {
            s.setItem(this.gameObject);
            Vector3 pos = s.tile.transform.position;
            this.transform.position = new Vector3(pos.x,pos.y,-0.2f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
