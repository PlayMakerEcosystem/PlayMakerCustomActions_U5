// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Text;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.String)]
[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=13809.0")]
[Tooltip("Generate a random password by length")]
public class GenerateRandomPassword : FsmStateAction
{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Set length of password")]
		public FsmInt passwordSize;

		[ActionSection("Options")]
		public FsmBool useLCase;
		public FsmBool useUCase;
		public FsmBool useNumeric;
		public FsmBool useSpecial;

		[ActionSection("Output")]
		public FsmString result;

		private string buildString;

		public override void Reset()
		{
			passwordSize = 16;
			useLCase = false;
			useUCase = false;
			useNumeric = false;
			useSpecial = false;
			result = null;
			buildString = null;
		}


	public override void OnEnter()
	{
			buildString = null;
			
			if (useLCase.Value == true || useUCase.Value == true || useNumeric.Value == true || useSpecial.Value == true){
				
			if (useLCase.Value == true)
			buildString += "abcdefghijklmnopqrstuvwxyz";

			if (useUCase.Value == true)
			buildString += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			if (useNumeric.Value == true)
			buildString += "1234567890";

			if (useSpecial.Value == true)
			buildString += "*$-+?_&=!%{}/";

			}

			else{
				buildString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			}

		
			result.Value = CreatePassword(passwordSize.Value);

			Finish();
	}

		private string CreatePassword(int length)
		{
			
			StringBuilder res = new StringBuilder();
			while (0 < length--)
			{
				res.Append(buildString[Random.Range(1,buildString.Length)]);
			}
			return res.ToString();
		}
}

}

