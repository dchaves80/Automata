using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class RobotOrder 
    {
        public enum orderType  {nullOrder, forward, backward, turnleft, turnright, begin, end, wait, grab, console, drop, poweroff,poweron,waititem }
        
        public orderType order;
        
    }
}