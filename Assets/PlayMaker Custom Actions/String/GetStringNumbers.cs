// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using System;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get all numbers in the given String.")]
	public class GetStringNumbers : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The String to get only the numbers from.")]
		public FsmString startString;

		[Tooltip("Store the numbers in an int variable.")]
		public FsmInt storeNumbersAsInt;

		[Tooltip("Store the numbers in a float variable.")]
		public FsmFloat storeNumbersAsFloat;

		[Tooltip("Store the numbers in a String variable.")]
		public FsmString storeNumbersAsString;

		[Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;

		private string tmpString;

		public override void Reset()
		{
			startString = null;
			storeNumbersAsInt = new FsmInt() { UseVariable = true };
			storeNumbersAsFloat = new FsmFloat() { UseVariable = true };
			storeNumbersAsString = new FsmString() { UseVariable = true };
			everyFrame = false;
			tmpString = null;
		}

		public override void OnEnter()
		{
			DoGetNumbers();

			if(!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetNumbers();
		}

		void DoGetNumbers()
		{
			tmpString = new String(startString.Value.Where(Char.IsDigit).ToArray());

			if(!storeNumbersAsString.IsNone)
			{
				storeNumbersAsString.Value = tmpString;
			}

			if(!storeNumbersAsFloat.IsNone)
			{
				storeNumbersAsFloat.Value = float.Parse(tmpString);
			}

			if(!storeNumbersAsInt.IsNone)
			{
				storeNumbersAsInt.Value = int.Parse(tmpString);
			}

		}

	}

}
