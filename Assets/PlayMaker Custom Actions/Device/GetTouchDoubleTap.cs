// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: touch

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Send event if user double taps")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12079.0")]
	public class GetTouchDoubleTap : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmInt fingerId;
		public TouchPhase touchPhase;
		public FsmEvent sendEvent;
		public FsmInt storeFingerId;
		public FsmFloat tapSpeed;
		private float lastTapTime;

		public override void Reset()
		{
			fingerId = new FsmInt { UseVariable = true } ;
			storeFingerId = null;
			tapSpeed= 0.5f;
		}

		public override void OnEnter()
		{
			lastTapTime = 0;

		}


		public override void OnUpdate()
		{
			if (Input.touchCount > 0)
			{
				foreach (var touch in Input.touches)
				{
					
					if (fingerId.IsNone || touch.fingerId == fingerId.Value)
					{
						if (touch.phase == touchPhase)
						{
							if((Time.time - lastTapTime) < tapSpeed.Value){
							storeFingerId.Value = touch.fingerId;
							Fsm.Event(sendEvent);
							
							}

							lastTapTime = Time.time;
						}
					}
				}
			}
		}
		
	}
}
