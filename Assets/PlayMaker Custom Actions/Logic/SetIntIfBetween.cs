// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Set int (and bool) values if int is between min or max.")]
	[HelpUrl("")]
	public class SetIntIfBetween : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The first int variable.")]
		public FsmInt currentInt;

		[RequiredField]
		[Tooltip("Min int variable.")]
		public FsmInt minInt;

		[RequiredField]
        [Tooltip("Max int variable.")]
		public FsmInt maxInt;

		[ActionSection("Set int result")]

		[Tooltip("Set int if between min and max")]
		[TitleAttribute("Set int value")]
		public FsmInt intValueBetween;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt setInt;

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
			currentInt = 0;
			maxInt = 0;
			setInt = 0;
			intValueBetween = 0;
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

			if (currentInt.Value >= minInt.Value && currentInt.Value <= maxInt.Value)
			{
				setInt.Value = intValueBetween.Value;
				isInBetween.Value = true;
				isLess.Value = false;
				isGreater.Value = false;
				return;
			}

			if (currentInt.Value < minInt.Value)
			{

			
				isInBetween.Value = false;
				isLess.Value = true;
				isGreater.Value = false;
				return;
			}

			if (currentInt.Value > maxInt.Value)
			{   

				isInBetween.Value = false;
				isLess.Value = false;
				isGreater.Value = true;
			}

		}

	}
}

