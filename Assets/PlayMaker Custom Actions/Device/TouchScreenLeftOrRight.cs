// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// v1.1


using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events based on Touch left or right of screen.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11574.0")]
	public class TouchScreenLeftOrRight : FsmStateAction
	{
		public FsmInt fingerId;
		public TouchPhase touchPhase;
		[ActionSection("Event")]
		public FsmEvent sendEventLeft;
		public FsmEvent sendEventRight;
		[ActionSection("Finger Id")]
		[UIHint(UIHint.Variable)]
		public FsmInt storeFingerId;
		public bool useMouse;
		
		public override void Reset()
		{
			fingerId = new FsmInt { UseVariable = true } ;
			storeFingerId = null;
			sendEventLeft = null;
			sendEventRight = null;
		}
		
		
		public override void OnUpdate()
		{
	
			if (useMouse == true){
				MouseInput();
			}

			else{
				TounchInput();
			}
		}

		void TounchInput()
		{
			if (Input.touchCount == 1)
			{
				var touch = Input.GetTouch(0);
				if (touch.position.x < Screen.width/2)
				{
					if (touch.phase == touchPhase)
					{
						storeFingerId.Value = touch.fingerId;
						Fsm.Event(sendEventLeft);
					}
				}
				else if (touch.position.x > Screen.width/2)
				{
					if (touch.phase == touchPhase)
					{
						storeFingerId.Value = touch.fingerId;
						Fsm.Event(sendEventRight);
					}
				}
			}
		}

		void MouseInput()
		{
			if (Input.GetMouseButtonDown(0))
			{
				
				if (Input.mousePosition.x < Screen.width/2)
				{

						Fsm.Event(sendEventLeft);
					
				}
				else if (Input.mousePosition.x > Screen.width/2)
				{

						Fsm.Event(sendEventRight);
					}
				}
			}

	}
}
