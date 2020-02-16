// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Enum)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Gets all Items in an Enum and saves it in an array.")]
	public class GetEnumAsArray : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum Variable to get all Items from.")]
		public FsmEnum enumVariable;

		[UIHint(UIHint.Variable)]
		[MatchElementType("enumVariable")]
		[Tooltip("Save all Enum Items in an Enum Array of the same type.")]
		public FsmArray saveAsEnumArray;

		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.String)]
		[Tooltip("Optionally save them as a String-Array.")]
		public FsmArray saveAsStringArray;

		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally save them in a separated String-Variable.")]
		public FsmString saveAsString;

		private string[] sArray;

		public override void Reset()
		{
			enumVariable = null;
			saveAsEnumArray = null;
			saveAsStringArray = null;
			saveAsString = null;
		}

		public override void OnEnter()
		{
			DoGetEnumValues();
		}

		void DoGetEnumValues()
		{
			//reset results
			saveAsEnumArray.Resize(0);
			Array.Clear(saveAsEnumArray.Values, 0, saveAsEnumArray.Length);
			saveAsStringArray.Resize(0);
			Array.Clear(saveAsStringArray.Values, 0, saveAsStringArray.Length);
			saveAsString.Value = "";

			//get Types
			var enumType = enumVariable.Value.GetType();
			var arrayType = saveAsEnumArray.GetType();

			//check if Types match
			if(enumType != arrayType)
			{
				Debug.LogWarning("Array and Enum are not of the same Enum-Type. Please match them in the Variables-Editor.");
			}

			foreach(var singleItem in Enum.GetValues(enumType))
			{
				saveAsEnumArray.Resize(saveAsEnumArray.Length + 1);
				saveAsEnumArray.Set(saveAsEnumArray.Length - 1, singleItem);

				saveAsStringArray.Resize(saveAsStringArray.Length + 1);
				saveAsStringArray.Set(saveAsStringArray.Length - 1, singleItem.ToString());

				saveAsString.Value += singleItem.ToString() + ", ";
			}
			saveAsString.Value = saveAsString.Value.TrimEnd(", ".ToCharArray());
		}
	}
}