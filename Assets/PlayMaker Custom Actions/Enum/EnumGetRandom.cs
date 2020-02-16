// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using System;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Enum)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get a Random item from an enum.")]
	public class EnumGetRandom : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum Variable to get a random Item from.")]
		public FsmEnum enumVariable;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			enumVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetRandomValue();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetRandomValue();
		}

		private void DoGetRandomValue()
		{
			List<object> allEnumItems = new List<object>();

			//get Type
			var enumType = enumVariable.Value.GetType();

			foreach(var singleItem in Enum.GetValues(enumType))
			{
				allEnumItems.Add(singleItem);
			}

			enumVariable.Value = (Enum)allEnumItems[UnityEngine.Random.Range(0, allEnumItems.Count)];
		}
	}
}

