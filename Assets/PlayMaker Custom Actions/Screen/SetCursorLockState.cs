// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Screen)]
	[Tooltip("set the lock mode of the mouse cursor")]
	public class SetCursorLockState : FsmStateAction 
	{

		[Tooltip("The LockMode to set the cursor at")]
		[ObjectType(typeof(CursorLockMode))]
		public FsmEnum lockMode;

		[Tooltip("If True will revert to the original value when exiting state")]
		public FsmBool resetOnExit;

		[Tooltip("Run every frame")]
		public bool everyframe;

		CursorLockMode _orig;


		public override void Reset()
		{
			lockMode = null;
			resetOnExit = false;
			everyframe = false;
		}
			
		public override void OnEnter ()
		{
			if (resetOnExit.Value) {
				_orig = Cursor.lockState;
			}

			SetCursorMode((CursorLockMode)lockMode.Value);

			if (!everyframe) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			SetCursorMode((CursorLockMode)lockMode.Value);
		}

		public override void OnExit ()
		{
			if (resetOnExit.Value) {
				SetCursorMode(_orig);
			}
		}

		void SetCursorMode(CursorLockMode mode)
		{
			Cursor.lockState = mode;
		}
	}
}