// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Returns the letter at the given Integer position (must be between 1 and 26).")]
	public class ConvertIntToLetter : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The int variable that serves as the position to get the letter of.")]
		public FsmInt intValue;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting letter.")]
		public FsmString result;

		[Tooltip("Whether the resulting letter should be in uppercase format instead of lowercase.")]
		public FsmBool returnUppercase;

		[Tooltip("Repeat every frame. Useful if the variable is changing and you're waiting for a particular result.")]
		public bool everyFrame;

		public override void Reset()
		{
			intValue = 1;
			result = null;
			returnUppercase = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvert();

			if(!everyFrame) Finish();
		}

		public override void OnUpdate()
		{
			DoConvert();
		}

		private void DoConvert()
		{
			if (intValue.Value < 1 || intValue.Value > 26)
			{
				LogError("Given Integer is out of range (" + intValue.Value + ")!");
				return;
			}
			
			result.Value = ((System.Char)((returnUppercase.Value ? 64 : 96) + intValue.Value)).ToString();
		}
	}
}