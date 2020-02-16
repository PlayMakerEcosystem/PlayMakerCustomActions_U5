// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Concatenates two Strings with each other.")]
	public class StringAppend2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The String to add to.")]
		public FsmString _string;

		[RequiredField]
		[Tooltip("The String to add.")]
		public FsmString stringToAdd;

		[Tooltip("Add the String to the end of the first one instead of the beginning.")]
		public FsmBool addToEnd;

		[Tooltip("Wheter to run this action on every frame or only once.")]
		public FsmBool everyFrame;

		private string cachedInitString = "";

		public override void Reset()
		{
			_string = new FsmString() { UseVariable = true };
			stringToAdd = null;
			addToEnd = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			cachedInitString = _string.Value;

			DoAddString();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			if(everyFrame.Value) _string.Value = cachedInitString;

			DoAddString();
		}

		private void DoAddString()
		{
			if(addToEnd.Value)
				_string.Value = _string.Value + stringToAdd.Value;
			else
				_string.Value = stringToAdd.Value + _string.Value;
		}
	}
}
