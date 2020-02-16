// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Screen)]
	[Tooltip("Set the visibility of the mouse cursor")]
	public class SetCursorVisible : FsmStateAction 
	{

		[Tooltip("The visible value to set the cursor with")]
		public FsmBool visible;

		[Tooltip("If True will revert to the original value when exiting state")]
		public FsmBool resetOnExit;

		[Tooltip("Run every frame")]
		public bool everyframe;

		bool _orig;


		public override void Reset()
		{
			visible = null;
			resetOnExit = false;
			everyframe = false;
		}
			
		public override void OnEnter ()
		{
			if (resetOnExit.Value) {
				_orig = Cursor.visible;
			}

			SetCursorVisibility(visible.Value);

			if (!everyframe) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			SetCursorVisibility(visible.Value);
		}

		public override void OnExit ()
		{
			if (resetOnExit.Value) {
				SetCursorVisibility(_orig);
			}
		}

		void SetCursorVisibility(bool visible)
		{
			Cursor.visible = visible;
		}
	}
}