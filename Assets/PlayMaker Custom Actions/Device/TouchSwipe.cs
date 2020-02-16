// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: touch

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Detect Touch Swipe")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12316.0")]
	public class TouchSwipe : FsmStateAction
	{
		[ActionSection("Setup")]
		[UIHint(UIHint.Variable)]
		public FsmInt fingerId;
		public enum SwipeDirection{
			Up,
			Down,
			Right,
			Left
		}
		
		public SwipeDirection swipeDir;

		[ActionSection("Output")]
		[Tooltip("Is currently swiping")]
		public FsmBool swiping;
		[Tooltip("Swip was successful")]
		public FsmBool sendEvent;
		[Tooltip("The last finger position of swip")]
		public FsmVector2 lastPosition;

		public override void Reset()
		{
			fingerId = new FsmInt { UseVariable = true } ;
			swiping = false;
			lastPosition = null;
			sendEvent = false;
		}


		public override void OnUpdate()
		{

			if (Input.touchCount == 0) 
				return;

			if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0){
				if (swiping.Value == false){
					swiping.Value = true;
					lastPosition.Value = Input.GetTouch(0).position;
					return;
				}
				else{
					if (!sendEvent.Value) {
							Vector2 direction = Input.GetTouch(0).position - lastPosition.Value;
							
							if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
								if (direction.x > 0) 
									swipeDir = SwipeDirection.Right;
								else
									swipeDir = SwipeDirection.Left;
							}
							else{
								if (direction.y > 0)
									swipeDir = SwipeDirection.Up;
								else
									swipeDir = SwipeDirection.Down;
							}
							
							sendEvent.Value = true;
					}
				}
			}
			else{
				swiping = false;
				sendEvent.Value = false;
			}

		}
		
	}
}

