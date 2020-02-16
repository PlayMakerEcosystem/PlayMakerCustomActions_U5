// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event after x frame. Useful if you want to loop states every x frame.")]
	public class NextFrameEventAdvance : FsmStateAction
	{
		[RequiredField]
		public FsmInt frameCount;
		public FsmEvent sendEvent;

		private int loop;

		public override void Reset()
		{
			frameCount = 1;
			sendEvent = null;
		}

		public override void OnEnter()
		{

			loop = 0;
		}

		public override void OnUpdate()
		{
			loop++;

			if (loop == frameCount.Value){

			Finish();

			Fsm.Event(sendEvent);
			}
		}


#if UNITY_EDITOR
        public override string AutoName()
        {
            return "Next Frame Event: " + sendEvent.Name;
        }
#endif
	}
}
