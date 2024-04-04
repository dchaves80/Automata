using System;
using System.Buffers;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Spatial 
    {
        public int x;
        public int y;
        public bool ocupied;
        public bool passable;
        public GameObject owner;
        public GameObject tile;
        public GameObject item;
        public GameObject machine;


        public void setFree() 
        {
            ocupied = false;
            passable = true;
            owner = null;
            item = null;
            machine = null;
        }

        public void setItem(GameObject item) 
        {
            ocupied = true;
            passable = false;
            this.item = item;
        }

        public void setMachine(GameObject machine)
        {
            ocupied = true;
            passable = false;
            this.machine = machine;
        }

        

        public void setRobot(GameObject robot) 
        {
            ocupied = true;
            passable = false;
            this.owner = robot;
        }

    }
}