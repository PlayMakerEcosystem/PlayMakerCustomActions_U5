// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Split Float into Ints.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11775.0")]
	public class SplitFloatToInt : FsmStateAction
	{
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		[Tooltip("Store the result in a Int variable.")]
		public FsmInt wholeNumber;

		[Tooltip("Store the result in a Int variable.")]
		public FsmInt decimalNumber;

		public FsmBool everyFrame;

		private int decimalCount;

		public override void Reset()
		{
			floatVariable = null;
			wholeNumber = null;
			decimalNumber = null;
			everyFrame = false;
		}


		public override void OnEnter()
		{
			DoAction();
			
			if (!everyFrame.Value)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoAction();
		}	


		void DoAction()
		{

			double number = floatVariable.Value;
			decimal number2 = (decimal) floatVariable.Value;
			long intPart = (long) number;

			wholeNumber.Value = (int) intPart;
			decimalCount = GetDecimals (number2);

			decimal precision = (number2 - wholeNumber.Value);
			 precision = (decimal)((double)precision * Math.Pow(10, decimalCount));
			decimalNumber.Value = (int) precision;

		

		}


		private int GetDecimals(decimal d, int i = 0)
		{
			decimal multiplied = (decimal)((double)d * Math.Pow(10, i));
			if (Math.Round(multiplied) == multiplied)
				return i;
			return GetDecimals(d, i+1);
		}
	}
}
