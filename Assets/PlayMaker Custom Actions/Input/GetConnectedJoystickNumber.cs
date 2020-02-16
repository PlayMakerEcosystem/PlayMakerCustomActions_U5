// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Joystick

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("How many joysticks are connected")]
	[HelpUrl("")]
	public class GetConnectedJoystickNumber : FsmStateAction
	{
	
		[ActionSection("Input")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the amount of Joysticks")]
		public FsmInt joystickQuantity;
		

		[ActionSection("Event")]
		[Tooltip("Event to send when there are no Joysticks.")]
		public FsmEvent emptyEvent;
		[Tooltip("Event to send when there more than Joystick connected.")]
		public FsmEvent finishedEvent;
		public FsmBool everyFrame;

		public override void Reset()
		{
			emptyEvent = null;
			finishedEvent = null;
			joystickQuantity = 0;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			CheckJoystick();

			if (!everyFrame.Value)
				Finish();
		}
		
		public override void OnUpdate()
		{
			CheckJoystick();
		}		


		void CheckJoystick()
		{

			joystickQuantity.Value = Input.GetJoystickNames().Length;

			if(joystickQuantity.Value == 0 & everyFrame.Value == false)
			{
				Fsm.Event(emptyEvent);
				return;
			}

			else if (everyFrame.Value == false) {
				Fsm.Event(finishedEvent);
				return;
			}

		}


	
	}
}

