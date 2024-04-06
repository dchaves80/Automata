using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class RobotOrder 
    {
        /// <summary>
        ///nullOrder, = Not an order, used for control<br/>
        ///logic_clacc, = put acc to 0<br/>
        ///logic_clbak, = put bak to 0<br/>
        ///logic_mov, = put[value] into acc = mov[value]<br/>
        ///logic_swp, = swap values between Acc and Bak<br/>
        ///<br/>
        ///logic_add, = sum between Acc and Bak and stores in Acc<br/>
        ///logic_mul, = multiplication between Acc and Bak and stores in Acc<br/>
        ///logic_sub, = substraction between Acc and Bak and stores in Acc<br/>
        ///<br/>
        ///logic_eq, = check if Acc equals to Bak and set conditionalVar in robot<br/>
        ///logic_gt, = check if Acc is great than Bak and set conditionalVarin robot <br/>
        ///logic_lt, = check if Acc is less than Bak and set conditionalVar in robot<br/>
        ///logic_ne, = check if Acc is not equal with Bak and set conditionalVar in robot<br/>
        /// </summary>
        public enum orderType  {
            /*Actions*/
            nullOrder, 
            forward, 
            backward, 
            turnleft, 
            turnright, 
            begin, 
            end, 
            wait, 
            grab, 
            console, 
            drop, 
            poweroff,
            poweron,
            waititem,
            /*logic*/
            logic_clacc,
            logic_clbak,
            logic_mov,
            logic_swp,
            logic_add,
            logic_mul,
            logic_sub,
            logic_eq,
            logic_gt,
            logic_lt,
            logic_ne
        }
        public orderType order;
        [SerializeField]
        public int conditional;
        public int parameter_value;

        public RobotOrder(RobotOrder robotOrder) 
        {
            order = robotOrder.order;
            conditional = robotOrder.conditional;
            parameter_value = robotOrder.parameter_value;
        }
        
        public RobotOrder() 
        {
            conditional = 0;
            parameter_value = 0;
            order = orderType.nullOrder;
        }

        /// <summary>
        /// conditional variables
        /// </summary>
        /// <param name="ot"></param>
        /// <param name="conditional">0=null, 1=true, 2=false</param>
        public RobotOrder (orderType ot, int conditional=0, int parameter_value=0) 
        {
            this.conditional = conditional;
            this.order = ot;
            this.parameter_value = parameter_value;
        }


    }
}