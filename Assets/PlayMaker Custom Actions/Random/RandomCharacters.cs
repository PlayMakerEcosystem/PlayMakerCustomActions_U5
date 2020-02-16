// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Random")]
	[Tooltip("Returns a random set of characters in given range(s). Optionally includes further ranges or lets you define the range yourself. Useful for cryptography or all kinds of random behavior.")]
	public class RandomCharacters : FsmStateAction
	{
		[Tooltip("Define a custom range of characters that should be picked from. Keep in mind that the following options are additive to the custom range (e.g. when 'Include Numbers' is enabled it adds all numbers to the custom range).")]
		public FsmString customRange;
		
		[Tooltip("Wheter to also include the lower-case variants.")]
		public FsmBool includeUpperCaseLetters;
		
		[Tooltip("Wheter to also include the lower-case variants.")]
		public FsmBool includeLowerCaseLetters;
		
		[Tooltip("Wheter to also include numbers (0-9).")]
		public FsmBool includeNumbers;
		
		[Tooltip("Wheter to also include commonly used special characters ('$', '%', '#', '@', '!', '*', '?', ';', ',', ':', '.', '^', '&').")]
		public FsmBool includeSpecialCharacters;
		
		[Tooltip("Wheter to also include umlauts (Ä, Ö, Ü and ß). Takes 'Inlude Lower Case' into account.")]
		public FsmBool includeUmlauts;
		
		[Tooltip("Defines how many random characters should be added to the resulting string.")]
		public FsmInt resultLength;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmString result;

		[Tooltip("Wheter to repeat this action on every frame or only once.")]
		public FsmBool everyFrame;

		private GameObject go;

		public override void Reset()
		{
			customRange = new FsmString {UseVariable = true};
			includeUpperCaseLetters = true;
			includeLowerCaseLetters = false;
			includeNumbers = false;
			includeSpecialCharacters = false;
			includeUmlauts = false;
			resultLength = 1;
			result = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetRandomCharacter();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			GetRandomCharacter();
		}

		private void GetRandomCharacter()
		{
			string chars = "";
			if (!customRange.IsNone) chars = customRange.Value;
			
			if (includeUpperCaseLetters.Value) chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			if (includeLowerCaseLetters.Value) chars += "abcdefghijklmnopqrstuvwxyz";
			if (includeNumbers.Value) chars += "1234567890";
			if (includeSpecialCharacters.Value) chars += "$%#@!*?;,:.^&";
			if (includeUmlauts.Value)
			{
				chars += "ÄÖÜß";
				if (includeLowerCaseLetters.Value) chars += "äöü";
			}

			System.Random rand = new System.Random();
			string tmpResult = "";

			if (!string.IsNullOrEmpty(chars))
			{
				for (int i = 0; i < resultLength.Value; i++)
				{
					int num = rand.Next(0, chars.Length - 1);
					tmpResult += chars[num];
				}
			}
			else
			{
				Log("No range of characters specified!");
			}
			
			result.Value = tmpResult;
		}

		public override string ErrorCheck()
		{
			if ((includeUpperCaseLetters.IsNone || !includeUpperCaseLetters.Value)
			 && (includeLowerCaseLetters.IsNone || !includeLowerCaseLetters.Value)
			 && (includeNumbers.IsNone || !includeNumbers.Value)
			 && (includeSpecialCharacters.IsNone || !includeSpecialCharacters.Value)
			 && (includeUmlauts.IsNone || !includeUmlauts.Value)
			 && (customRange.IsNone || string.IsNullOrEmpty(customRange.Value)))
			{
				return "No range of characters specified!";
			}

			return "";
		}
	}
}
