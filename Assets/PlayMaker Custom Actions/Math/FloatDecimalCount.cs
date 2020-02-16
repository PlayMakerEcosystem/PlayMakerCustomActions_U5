// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Count Float decimals into an Int")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11776.0")]
	public class FloatDecimalCount : FsmStateAction
	{
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		[Tooltip("Count Result")]
		public FsmInt decimalNumberCount;


		private int decimalCount;

		public override void Reset()
		{
			floatVariable = null;
			decimalNumberCount = null;

		}
		

		public override void OnEnter()
		{

			decimal number = (decimal) floatVariable.Value;

			decimalCount = GetDecimals (number);

			decimalNumberCount.Value = decimalCount;

			Finish();

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
