// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Reverse a string")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12369.0")]
	public class StringReverse : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;


		[UIHint(UIHint.Variable)]
		public FsmString storeResult;


		public override void Reset()
		{
			stringVariable = null;
			storeResult = null;
	
		}

		public override void OnEnter()
		{
			storeResult.Value = Reverse(stringVariable.Value);
			Finish();
		}
		

		public string Reverse(string text)
		{
			if (text == null) return null;
			char[] charArray = text.ToCharArray();
			Array.Reverse( charArray );
			return new string( charArray );
	
		}
	}
}
