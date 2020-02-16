// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Random")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Receive a random int value around a specified range. If the range is set to 5 and the starting float is 24, the result can be anything between 19 and 29. Useful to get variations from fixed values like attack damage, drop chances and combination success rates.")]
	public class RandomIntAroundRange : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The int value the range wraps around.")]
		public FsmInt startInt;

		[RequiredField]
		[Tooltip("The range to go up or down from the start value. Imagine a circle: The circumference is the range and any radius in between can be the result.")]
		public FsmInt range;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The random endresult inside the specified range.")]
		public FsmInt result;

		[Tooltip("If true, goes only up from the start value, not below (In the example: between 24 - 29 instead of 19 - 29).")]
		public bool unsigned;

		[Tooltip("Repeat this action every frame until state change.")]
		public bool everyFrame;

		private int min;
		private int max;

		public override void Reset()
		{
			startInt = null;
			range = null;
			result = null;
			unsigned = false;
			everyFrame = false;
			min = 0;
			max = 1;
		}

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetRandomFromRange();
			if(!everyFrame)
				Finish();
		}

		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoGetRandomFromRange();
		}

		public void DoGetRandomFromRange()
		{
			if(!unsigned)
			{
				min = startInt.Value - range.Value;
				max = startInt.Value + range.Value;
				result.Value = Random.Range(min, max);
			} else
			{
				min = startInt.Value;
				max = startInt.Value + range.Value;
				result.Value = Random.Range(min, max);
			}
		}
	}
}
