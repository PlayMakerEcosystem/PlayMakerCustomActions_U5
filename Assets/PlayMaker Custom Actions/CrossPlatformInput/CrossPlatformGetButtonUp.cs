// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// original actions from KellyRay: http://hutonggames.com/playmakerforum/index.php?topic=11224.msg52975#msg52975
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
#if CROSS_PLATFORM_INPUT 
using UnityStandardAssets.CrossPlatformInput;
#endif

#pragma warning disable 0162

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CrossPlatformInput")]
	[Tooltip("Sends an Event when a Button is released. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetButtonUp : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;
		
		[Tooltip("Event to send if the button is released.")]
		public FsmEvent sendEvent;
		
		[Tooltip("Set to True if the button is released.")]
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
			var buttonUp = false;

			#if CROSS_PLATFORM_INPUT
				buttonUp = CrossPlatformInputManager.GetButtonUp(buttonName.Value);
			#endif

			if (buttonUp)
			{
				Fsm.Event(sendEvent);
			}
			
			storeResult.Value = buttonUp;
		}
	}
}
