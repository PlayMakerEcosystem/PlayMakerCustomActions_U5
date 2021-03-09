// (c) Copyright HutongGames, LLC 2010-2021. All rights reserved. 
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Djaydino / http://www.jinxtergames.com/
//keywords : is on platform event
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Application)]
    [Tooltip("Sends an Event if on the specified Platforms or not.")]
    public class RuntimePlatformEvent : FsmStateAction
    {
        [CompoundArray("Enum Switches", "Compare Enum Values", "Send")] 
        [MatchFieldType("enumVariable")]
        [ObjectType(typeof(RuntimePlatform))]
        public FsmEnum[] compareTo;      
        public FsmEvent[] sendEvent;
        public override void Reset()
        {
            compareTo = new FsmEnum[1];
            sendEvent = new FsmEvent[1];
        }

        public override void OnEnter()
        {
            DoEnumSwitch();
            Finish();
        }

        public override void OnUpdate()
        {
            DoEnumSwitch();
        }

        private void DoEnumSwitch()
        {

            for (int i = 0; i < compareTo.Length; i++)
            {
                if (Equals(Application.platform, compareTo[i].Value))
                {
                    Fsm.Event(sendEvent[i]);
                }
            }
        }
    }
}