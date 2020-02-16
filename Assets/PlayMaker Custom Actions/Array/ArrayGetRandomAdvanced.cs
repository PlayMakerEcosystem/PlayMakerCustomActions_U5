// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a Random item from an Array. Optionally disallow to get the same result twice in a row.")]
	public class ArrayGetRandomAdvanced : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to use.")]
		public FsmArray array;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value in a variable.")]
		[MatchElementType("array")]
		public FsmVar storeValue;

		[Tooltip("Can return the same item twice in a row.")]
		public FsmBool repeat;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		int prevItem;

		public override void Reset()
		{
			array = null;
			storeValue = null;
			repeat = false;
			everyFrame = false;
		}

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetRandomValue();

			if(!everyFrame) Finish();

		}

		public override void OnUpdate()
		{
			DoGetRandomValue();

		}

		private void DoGetRandomValue()
		{
			if(storeValue.IsNone) return;

			int currItem = Random.Range(0, array.Length);

			if(!repeat.Value)
			{
				while(currItem == prevItem)
				{
					currItem = Random.Range(0, array.Length);
				}
			}

			storeValue.SetValue(array.Get(currItem));
			prevItem = currItem;
		}


	}
}

