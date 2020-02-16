// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Get the fixedDeltaTime during the fixedUpdate call")]
	public class GetFixedDeltaTime : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the fixed delta time")]
		public FsmFloat fixedDeltaTime;

		[Tooltip("Repeat every fixed frame.")]
		public bool everyFixedFrame;

		public override void Reset()
		{
			
			fixedDeltaTime = null;
			everyFixedFrame = false;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}
		
		public override void OnEnter()
		{
			executeAction();

			if(!everyFixedFrame)
			{
				Finish();
			}
		}

		public override void OnFixedUpdate()
		{
			executeAction();
		}

		void executeAction()
		{
			fixedDeltaTime.Value = Time.fixedDeltaTime;
		}
	}
}
