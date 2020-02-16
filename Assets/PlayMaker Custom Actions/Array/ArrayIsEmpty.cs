// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Send an event based on the length of an array.")]
	public class ArrayIsEmpty : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable.")]
		public FsmArray array;

		[Tooltip("Event to send if the array is empty.")]
		public FsmEvent isEmpty;

		[Tooltip("Event to send if the array not is empty.")]
		public FsmEvent isNotEmpty;

		public override void Reset()
		{
			array = null;
		}

		public override void OnEnter()
		{
			Fsm.Event(array.Length == 0 ? isEmpty : isNotEmpty);
			Finish();
		}
	}
}