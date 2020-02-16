// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Removes a Strings from another.")]
	public class StringRemove : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The String to remove from.")]
		public FsmString _string;

		[RequiredField]
		[Tooltip("The String to remove.")]
		public FsmString stringToRemove;

		[Tooltip("Remove the String to the end of the first one instead of the beginning.")]
		public FsmBool removeFromEnd;

		[Tooltip("Wheter to run this action on every frame or only once.")]
		public FsmBool everyFrame;

		private string cachedInitString = "";

		public override void Reset()
		{
			_string = new FsmString() { UseVariable = true };
			stringToRemove = null;
			removeFromEnd = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			cachedInitString = _string.Value;

			DoRemoveString();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			if(everyFrame.Value) _string.Value = cachedInitString;

			DoRemoveString();
		}

		private void DoRemoveString()
		{
			string str = _string.Value;
			string strRemove = stringToRemove.Value;

			if(removeFromEnd.Value)
			{
				int sublength = str.Length - strRemove.Length;
				string substr = str.Substring(sublength);
				if(substr == strRemove)
					_string.Value = str.Substring(0, sublength);
			} else
			{
				if(strRemove.Length > str.Length) return;

				string substr = str.Substring(0, strRemove.Length);

				if(substr == strRemove)
					_string.Value = str.Substring(strRemove.Length);
			}
		}
	}
}
