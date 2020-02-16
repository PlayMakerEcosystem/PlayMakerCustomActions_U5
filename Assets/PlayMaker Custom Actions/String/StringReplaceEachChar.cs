// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Replace each char in a given String with the specified one. Useful to obfuscate confidential or not yet unlocked text. Optionally removes spaces.")]
	public class StringReplaceEachChar : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The String to replace each char of.")]
		public FsmString startString;

		[RequiredField]
		[Tooltip("The String that should replace every char of the Start String")]
		public FsmString replaceWith;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String in a variable. Can be the same as the Start String to override it or a new one to keep them separate and be able to revert these changes.")]
		public FsmString storeResult;

		[Tooltip("If set gets rid of spaces if the string consists of several words.")]
		public FsmBool removeEmptySpace;

		[Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;

		//private string result;

		public override void Reset()
		{
			startString = null;
			replaceWith = null;
			storeResult = null;
			removeEmptySpace = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoBuildString();

			if(!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoBuildString();
		}

		void DoBuildString()
		{
			var tmp = new Regex("\\S").Replace(startString.Value, replaceWith.Value);

			if(removeEmptySpace.Value)
			{
				tmp = Regex.Replace(tmp, " ", string.Empty); //alt.: @"^\s*$[\r\n]*"
			}

			storeResult.Value = tmp;
		}

	}
}
