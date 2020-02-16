// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: objectcompare, unityobject, object change

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of a Object variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class ObjectChanged : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Object variable to watch for a change.")]
		public FsmObject objectVariable;

        [Tooltip("Event to send if the variable changes.")]
		public FsmEvent changedEvent;

		[UIHint(UIHint.Variable)]
        [Tooltip("Set to True if the variable changes.")]
		public FsmBool storeResult;
		
		private Object previousValue;

		public override void Reset()
		{
			objectVariable = null;
			changedEvent = null;
			storeResult = null;
		}

		public override void OnEnter()
		{
			if (objectVariable.IsNone)
			{
				Finish();
				return;
			}
			
			previousValue = objectVariable.Value;
		}
		
		public override void OnUpdate()
		{
			storeResult.Value = false;
			
			if (objectVariable.Value != previousValue)
			{
				storeResult.Value = true;
				Fsm.Event(changedEvent);
			}
		}
	}
}


