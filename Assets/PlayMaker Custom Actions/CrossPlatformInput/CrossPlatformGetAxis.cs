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
	[Tooltip("Gets the value of the specified Input Axis and stores it in a Float Variable. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetAxis : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the axis. Set in the Unity Input Manager.")]
		public FsmString axisName;
		
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range.")]
		public FsmFloat multiplier;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a float variable.")]
		public FsmFloat store;
		
		[Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			axisName = "";
			multiplier = 1.0f;
			store = null;
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
			DoGetAxis();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetAxis();
		}
		
		void DoGetAxis()
		{
			var axisValue = 0f;
			#if CROSS_PLATFORM_INPUT
				axisValue = CrossPlatformInputManager.GetAxis(axisName.Value);
			#endif
			// if variable set to none, assume multiplier of 1
			if (!multiplier.IsNone)
			{
				axisValue *= multiplier.Value;
			}
			
			store.Value = axisValue;
		}
	}
}

