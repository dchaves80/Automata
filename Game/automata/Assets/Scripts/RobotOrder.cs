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

        [SerializeField]
        public int conditional;

        public RobotOrder(RobotOrder robotOrder) 
        {
            order = robotOrder.order;
            conditional = robotOrder.conditional;
        }
        
        public RobotOrder() 
        {
            conditional = 0;
            order = orderType.nullOrder;
        }

        /// <summary>
        /// conditional variables
        /// </summary>
        /// <param name="ot"></param>
        /// <param name="conditional">0=null, 1=true, 2=false</param>
        public RobotOrder (orderType ot, int conditional) 
        {

        }


    }
}