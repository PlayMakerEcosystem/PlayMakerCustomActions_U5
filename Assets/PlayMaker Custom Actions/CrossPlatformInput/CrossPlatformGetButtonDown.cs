// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// original actions from KellyRay: http://hutonggames.com/playmakerforum/index.php?topic=11224.msg52975#msg52975
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CrossPlatformInput")]
	[Tooltip("Sends an Event when a Button is pressed. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetButtonDown : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;
		
		[Tooltip("Event to send if the button is pressed.")]
		public FsmEvent sendEvent;
		
		[Tooltip("Set to True if the button is pressed.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
		
		public override void Reset()
		{
			buttonName = "Fire1";
			sendEvent = null;
			storeResult = null;
		}

		public override string ErrorCheck()
		{
			#if !CROSS_PLATFORM_INPUT
			return "Missing Cross Platform Input Asset:\nImport from Assets/Import Package/CrossPlatformInput";
			#endif
			
			return "";
		}
		
		public override void OnUpdate()
		{
			var buttonDown = false;
			#if CROSS_PLATFORM_INPUT
				buttonDown = CrossPlatformInputManager.GetButtonDown(buttonName.Value);
			#endif
			if (buttonDown)
			{
				Fsm.Event(sendEvent);
			}
			
			storeResult.Value = buttonDown;
		}
	}
}
