// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// 1.1

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Int split into Float.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11775.0")]
		public class SplitIntToFloat : FsmStateAction
	{
		[ActionSection("Input")]
		[RequiredField]
		public FsmInt wholeNumber;
		[RequiredField]
		public FsmInt decimalNumber;

		[ActionSection("Output")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Int variable.")]
		public FsmFloat floatVariable;


		[ActionSection("Options")]
		[Tooltip("How many leading zeros in the decimal - example: 3 would mean .000xxx")]
		public FsmInt leadingZeros;
		public FsmBool everyFrame;


		public override void Reset()
		{
			floatVariable = null;
			wholeNumber = null;
			decimalNumber = null;
			everyFrame = false;
			leadingZeros = 0;
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
			int tempInt = decimalNumber.Value;

			int n = (int)Math.Floor(Math.Log10(tempInt) + 1);

			float tamp = (float)tempInt / ((float)Math.Pow(10, n));
		
			n = leadingZeros.Value;

			floatVariable.Value = (float)wholeNumber.Value + (tamp/ (float)Math.Pow(10, n));


		}


	
	}
}

