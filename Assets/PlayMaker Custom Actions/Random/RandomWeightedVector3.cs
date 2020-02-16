// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Random")]
	[Tooltip("Pick a random weighted Vector3 picked from an array of Vector3's.")]
	public class RandomWeightedVector3 : FsmStateAction
	{
		[CompoundArray("Amount", "Vector3", "Weighting")]
		public FsmVector3[] amount;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 result;
		[Tooltip("Can hit the same number twice in a row")]
		public FsmBool Repeat;

		private int randomIndex;
		private int lastIndex = -1;

		public override void Reset()
		{
			amount = new FsmVector3[3];
			weights = new FsmFloat[] { 1, 1, 1 };
			result = null;
			Repeat = false;
		}
		public override void OnEnter()
		{
			PickRandom();

			Finish();
		}

		void PickRandom()
		{
			if(amount.Length == 0)
			{
				return;
			}

			if(Repeat.Value)
			{
				randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				result.Value = amount[randomIndex].Value;

			} else
			{
				do
				{
					randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				} while(randomIndex == lastIndex);

				lastIndex = randomIndex;
				result.Value = amount[randomIndex].Value;
			}
		}
	}
}