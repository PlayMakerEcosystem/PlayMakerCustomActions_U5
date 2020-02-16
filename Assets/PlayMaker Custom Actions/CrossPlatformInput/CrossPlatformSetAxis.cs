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
	[Tooltip("Sets the value of the specified Input Axis. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformSetAxis : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the axis. Set in the Unity Input Manager.")]
		public FsmString axisName;
		
		[Tooltip("Axis values are in the range -1 to 1.")]
		[HasFloatSlider(-1f,1f)]
		public FsmFloat value;

		[Tooltip("Event Sent if axis not found")]
		public FsmEvent axisNotFound;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			axisName = "";
			value = null;
			axisNotFound = null;
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
			if (!CrossPlatformInputManager.AxisExists(axisName.Value))
			{
				Fsm.Event(axisNotFound);
				Finish();
				return;
			}
			#endif

			DoSetAxis();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetAxis();
		}
		
		void DoSetAxis()
		{
			#if CROSS_PLATFORM_INPUT
			CrossPlatformInputManager.SetAxis(axisName.Value,value.Value);
			#endif

		}
	}
}

