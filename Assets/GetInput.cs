using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

namespace CustomInputSystem
{
    //[DefaultExecutionOrder(-12)]
    public class GetInput : MonoBehaviour
    {
        #region OLD
        // static public bool Fire;
        // static public bool Jump;
        // 
        // 
        // // imposta da PlayerController se finisce salto
        // // per evitare che il salto ricominci
        // static public bool ForceStopJump;
        // 
        // 
        // public bool testFire, testJump;
        // 
        // 
        // 
        // public bool TouchInput = true;
        // 
        // 
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Tab))
        //         TouchInput = !TouchInput;
        // 
        // 
        //     testFire = Fire;
        //     testJump = Jump;
        // 
        // 
        //     if (!TouchInput)
        //     {
        //         Fire = Input.GetKeyDown(KeyCode.W);
        //         Jump = Input.GetKey(KeyCode.Space);
        //     }
        // 
        //     // va lasciato prima del controllo successivo
        //     if (!Jump && ForceStopJump)
        //     { ForceStopJump = false; }
        // 
        // 
        //     if (Jump && ForceStopJump)
        //     { Jump = false; }
        // }
        // 
        // 
        // public void PressFire() { Fire = true; }
        // 
        // public void JumpIn()
        // { Jump = true; }
        // 
        // public void JumpOut()
        // { Jump = false; ForceStopJump = false; }
        // 
        // void LateUpdate() { if (Fire) Fire = false; }
        #endregion


        #region INPUT_TOUCH
        static bool jumpPush, firePush;
        static bool jumpHold, fireHold;
        static bool jumpPull, firePull;


        #region JUMP
        // toccando lo schermo si attivano sia push che old
        // dopo un frame push ritorna false e quando rilasci
        // diventano entrambi falsi
        static public bool Touch_Jump
        { set { jumpPush = jumpHold = value; } }
        
        static public bool Leave_Jump
        {
            get
            {
                if (!jumpPull) return jumpPull;
                else
                { jumpPull = false; return true; }
            }
            set { jumpPull = value; }
        }
        #endregion

        #region FIRE
        // toccando lo schermo si attivano sia push che old
        // dopo un frame push ritorna false e quando rilasci
        // diventano entrambi falsi
        static public bool Touch_Fire
        { set { firePush = fireHold = value; } }

        static public bool Leave_Fire
        {
            get
            {
                if (!firePull) return firePull;
                else
                { firePull = false; return true; }
            }
            set { firePull = value; }
        }
        #endregion

        #endregion


        
        // PREMI

        static public bool Press_Jump
        { get => Input.GetKeyDown(KeyCode.Space); }
        //{ get => JumpPush() || Input.GetKeyDown(KeyCode.Space); }
        static public bool Press_Fire
        { get => Input.GetKeyDown(KeyCode.W); }
        //{ get => FirePush() || Input.GetKeyDown(KeyCode.W); }



        // MANTIENI

        static public bool Hold_Jump
        { get => jumpHold || Input.GetKey(KeyCode.Space); }
        static public bool Hold_Fire
        { get => fireHold || Input.GetKey(KeyCode.W); }



        // RILASCIA

        static public bool Release_Jump
        { get => Leave_Jump || Input.GetKeyUp(KeyCode.Space); }
        static public bool Release_Fire
        { get => Leave_Fire || Input.GetKeyUp(KeyCode.W); }
    }
}