// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=9903

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Set float (and bool) values if it is between min or max floats.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9903")]
    public class SetFloatIfBetween : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The first float variable.")]
		public FsmFloat currentFloat;

		[RequiredField]
		[Tooltip("Min float variable.")]
		public FsmFloat minFloat;

		[RequiredField]
        [Tooltip("Maxfloat variable.")]
		public FsmFloat maxFloat;

		[ActionSection("Set floats result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat setFloat;
		[Tooltip("Set Float if between min and max")]
		public FsmFloat floatValueBetween;
		[Tooltip("Set Float if less than min float")]
		public FsmFloat valueLessThanMin;
		[Tooltip("Set Float if greater than max float")]
		public FsmFloat valueGreaterThanMax;

		[ActionSection("Set bools")]
		[UIHint(UIHint.Variable)]
		public FsmBool isInBetween;
		[UIHint(UIHint.Variable)]
		public FsmBool isLess;
		[UIHint(UIHint.Variable)]
		public FsmBool isGreater;

		[ActionSection("")]
		[Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
		public bool everyFrame;

		public override void Reset()
		{
			currentFloat = 0f;
			maxFloat = 0f;
			setFloat = 0f;
			floatValueBetween = 0f;
			valueLessThanMin = 0f;
			valueGreaterThanMax = 0f;
			isInBetween = false;
			isLess = false;
			isGreater = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoCompare();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompare();
		}

		void DoCompare()
		{

			if (currentFloat.Value >= minFloat.Value && currentFloat.Value <= maxFloat.Value)
			{
				setFloat.Value = floatValueBetween.Value;
				isInBetween.Value = true;
				isLess.Value = false;
				isGreater.Value = false;
				return;
			}

			if (currentFloat.Value < minFloat.Value)
			{

				setFloat.Value = valueLessThanMin.Value;
				isInBetween.Value = false;
				isLess.Value = true;
				isGreater.Value = false;
				return;
			}

			if (currentFloat.Value > maxFloat.Value)
			{   
				setFloat.Value = valueGreaterThanMax.Value;
				isInBetween.Value = false;
				isLess.Value = false;
				isGreater.Value = true;
			}

		}

	}
}
