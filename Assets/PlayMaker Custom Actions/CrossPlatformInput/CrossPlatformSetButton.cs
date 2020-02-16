// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
#if CROSS_PLATFORM_INPUT 
using UnityStandardAssets.CrossPlatformInput;
#endif

#pragma warning disable 0162

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CrossPlatformInput")]
	[Tooltip("Sets the value of the specified Input Button. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformSetButton : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;
		
		[Tooltip("Button value")]
		public FsmBool value;

		[Tooltip("Event Sent if button not found")]
		public FsmEvent buttonNotFound;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			buttonName = "";
			value = null;
			buttonNotFound = null;
			everyFrame = true;
		}

		public override string ErrorCheck()
		{
			#if !CROSS_PLATFORM_INPUT
			return "Missing Cross Platform Input Asset:\nImport from Assets/Import Package/CrossPlatformInput";
			#endif
			
			return "";
		}
		
		public override void OnEnter()
		{
			#if CROSS_PLATFORM_INPUT
			if (!CrossPlatformInputManager.ButtonExists(buttonName.Value))
			{
				Fsm.Event(buttonNotFound);
				Finish();
				return;
			}
			#endif

			DoSetButton();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetButton();
		}
		
		void DoSetButton()
		{
			#if CROSS_PLATFORM_INPUT
				if (value.Value){
					CrossPlatformInputManager.SetButtonUp(buttonName.Value);
				}
				else{
					CrossPlatformInputManager.SetButtonDown(buttonName.Value);
				}
			#endif

		}
	}
}

