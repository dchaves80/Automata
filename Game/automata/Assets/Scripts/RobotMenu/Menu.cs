using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public RobotBehavior rb;
    public GameObject wait;
    void Start()
    {
        wait.SetActive(false);

    }

    private void OnMouseExit()
    {
        Destroy(this.gameObject);
        rb.menuOpened = false;
    }

    public void SetRobot(RobotBehavior _rb) 
    {
        Option[] optionsList = this.GetComponentsInChildren<Option>();
        for (int i = 0;  i < optionsList.Length; i++)
        {
            optionsList[i].behavior = rb;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
