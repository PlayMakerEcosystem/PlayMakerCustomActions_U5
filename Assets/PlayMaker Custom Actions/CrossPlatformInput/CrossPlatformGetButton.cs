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
	[Tooltip("Gets the pressed state of the specified Button and stores it in a Bool Variable. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetButton : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;		
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			buttonName = "Fire1";
			storeResult = null;
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
			DoGetButton();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetButton();
		}
		
		void DoGetButton()
		{
			#if CROSS_PLATFORM_INPUT
				storeResult.Value = CrossPlatformInputManager.GetButton(buttonName.Value);
			#endif
		}
	}
}


