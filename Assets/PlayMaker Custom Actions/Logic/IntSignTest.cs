// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends Events based on the sign of an Integer variable.")]
	public class IntSignTest : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The int variable to test.")]
		public FsmInt intValue;

		[Tooltip("Event to send if the int variable is positive.")]
		public FsmEvent isPositive;

		[Tooltip("Event to send if the int variable is negative.")]
		public FsmEvent isNegative;

		[Tooltip("Repeat every frame. Useful if the variable is changing and you're waiting for a particular result.")]
		public bool everyFrame;

		public override void Reset()
		{
			intValue = 0;
			isPositive = null;
			isNegative = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSignTest();

			if(!everyFrame) Finish();
		}

		public override void OnUpdate()
		{
			DoSignTest();
		}

		void DoSignTest()
		{
			Fsm.Event(intValue.Value < 0 ? isNegative : isPositive);
		}

		public override string ErrorCheck()
		{
			if(FsmEvent.IsNullOrEmpty(isPositive) &&
				FsmEvent.IsNullOrEmpty(isNegative))
				return "Action sends no events!";
			return "";
		}
	}
}