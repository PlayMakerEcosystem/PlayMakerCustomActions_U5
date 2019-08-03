// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
#if CROSS_PLATFORM_INPUT 
using UnityStandardAssets.CrossPlatformInput;
#endif

#pragma warning disable 0162

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CrossPlatformInput")]
	[Tooltip("Gets if a specified Input Axis exists. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetAxisExists : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the axis.")]
		public FsmString axisName;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if axis exists")]
		public FsmBool axisExists;

		[Tooltip("Event sent if axis exists")]
		public FsmEvent axisExistsEvent;

		[Tooltip("Event sent if axis do not exists")]
		public FsmEvent axisDoNotExistsEvent;

		
		public override void Reset()
		{
			axisName = "";
			axisExists = null;
			axisExistsEvent = null;
			axisDoNotExistsEvent = null;

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
			DoGetAxisExists();

			Finish();
		}
		
		void DoGetAxisExists()
		{
			bool _exists;
			#if CROSS_PLATFORM_INPUT
				_exists = CrossPlatformInputManager.AxisExists(axisName.Value);
			#endif

			axisExists.Value = _exists;
			Fsm.Event(_exists?axisExistsEvent:axisDoNotExistsEvent);
		}
	}
}

