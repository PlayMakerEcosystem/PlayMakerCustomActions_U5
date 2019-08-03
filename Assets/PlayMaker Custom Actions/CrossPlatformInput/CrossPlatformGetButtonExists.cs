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
	[Tooltip("Gets if a specified Button exist. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetButtonExists : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if Button exists")]
		public FsmBool buttonExists;

		[Tooltip("Event sent if Button exists")]
		public FsmEvent buttonExistsEvent;

		[Tooltip("Event sent if Button do not exists")]
		public FsmEvent buttonDoNotExistsEvent;

		
		public override void Reset()
		{
			buttonName = "";
			buttonExists = null;
			buttonExistsEvent = null;
			buttonDoNotExistsEvent = null;

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
			DoGetButtonExists();

			Finish();
		}
		
		void DoGetButtonExists()
		{
			bool _exists;
			#if CROSS_PLATFORM_INPUT
			_exists = CrossPlatformInputManager.ButtonExists(buttonName.Value);
			#endif

			buttonExists.Value = _exists;
			Fsm.Event(_exists?buttonExistsEvent:buttonDoNotExistsEvent);
		}
	}
}

