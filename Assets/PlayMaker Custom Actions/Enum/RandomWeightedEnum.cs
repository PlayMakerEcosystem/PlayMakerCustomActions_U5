// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Enum)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Pick a random weighted Enum picked from an array of specified Enums.")]
	public class RandomWeightedEnum : FsmStateAction
	{
		[CompoundArray("Amount", "Enum", "Weighting")]
		public FsmEnum[] amount;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmEnum result;

		[Tooltip("Can hit the same enum twice in a row.")]
		public FsmBool Repeat;

		private int randomIndex;
		private int lastIndex = -1;

		public override void Reset()
		{
			amount = new FsmEnum[3];
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