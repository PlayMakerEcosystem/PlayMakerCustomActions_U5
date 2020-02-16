// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Pinch")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12145.0")]
	public class PinchFloat : FsmStateAction
{


		[ActionSection("Setup")]
		public FsmFloat speed;
		public FsmFloat targetFloat;
		public FsmBool isActive;

		[ActionSection("clamp")]
		[RequiredField]
		[Tooltip("Minimum Value. Cannot go below that number")]
		public FsmFloat minSizeClamp;
		[RequiredField]
		[Tooltip("Maximum Value. Cannot go above that number")]
		public FsmFloat maxSizeClamp;
		public FsmBool invert;

		[ActionSection("Events")]
		[Tooltip("Disable action and send event")]
		public FsmBool disableAction;
		public FsmEvent sendEvent;


		public override void Reset()
		{
			isActive = false;
			targetFloat = 0.5f;
			speed =  null;
			minSizeClamp = 0.1f;
			maxSizeClamp = 179.9f;
			disableAction  = false;
			invert = false;
	
		}
	
		public override void OnEnter ()
		{

		}

	
		public override void OnUpdate()
		{
			isActive.Value = false;

			if (Input.touchCount == 2)
			{
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
				

				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				isActive.Value = true;

				if (invert.Value == true){

					targetFloat.Value -= deltaMagnitudeDiff * speed.Value;
				}

				else {
					targetFloat.Value += deltaMagnitudeDiff * speed.Value;
				}
					targetFloat.Value = Mathf.Clamp(targetFloat.Value, minSizeClamp.Value, maxSizeClamp.Value);


			}

			if (disableAction.Value == true)
			{
				isActive.Value = false;
				Fsm.Event(sendEvent);
			}
		}

	}
}

