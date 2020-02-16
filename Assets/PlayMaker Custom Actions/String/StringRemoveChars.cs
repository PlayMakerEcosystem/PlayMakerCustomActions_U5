// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Removes any specified Characters from the given String.")]
	public class StringRemoveChars : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The String to replace each char of.")]
		public FsmString startString;

		[RequiredField]
		[Tooltip("Insert any char that should be removed from the Start String.")]
		public FsmString charsToRemove;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String in a variable. Can be the same as the Start String to override it or a new one to keep them separate and be able to revert these changes.")]
		public FsmString storeResult;

		[Tooltip("If set replaces chars with space instead of removing them.")]
		public FsmBool leaveEmpty;

		[Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;

		private char[] tmpChar;
		private string stringToModify;

		public override void Reset()
		{
			startString = null;
			charsToRemove = null;
			storeResult = null;
			leaveEmpty = false;
			everyFrame = false;
			tmpChar = null;
			stringToModify = null;
		}

		public override void OnEnter()
		{
			DoRemove();

			if(!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoRemove();
		}

		void DoRemove()
		{
			//Get all chars from toCharArray in a String
			tmpChar = charsToRemove.Value.ToCharArray();
			stringToModify = startString.Value;
			for(int i = 0; i < stringToModify.Length; i++)
			{
				foreach(char c in tmpChar)
				{
					if(leaveEmpty.Value)
					{
						stringToModify = stringToModify.Replace(c.ToString(), " ");
					} else
					{
						stringToModify = stringToModify.Replace(c.ToString(), string.Empty);
					}
				}
			}
			storeResult.Value = stringToModify;
		}

	}

}
