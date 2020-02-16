// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author: Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sets a String value based on the value of another String Variable. Optionally send a No-Match-Event.")]
	public class StringCompareSetMulti : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringToCompare;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringToSet;

		[CompoundArray("String Switches", "Compare To", "Set")]
		public FsmString[] compare;
		public FsmString[] set;

		[Tooltip("Event to raise if no matching Strings found.")]
		public FsmEvent NoMatchEvent;

		public FsmBool everyFrame;

		public override void Reset()
		{
			stringToCompare = null;
			stringToSet = null;
			compare = new FsmString[0];
			set = new FsmString[0];
			NoMatchEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStringSwitch();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			DoStringSwitch();
		}

		void DoStringSwitch()
		{
			if(stringToCompare.IsNone) return;

			bool hasFound = false;

			for(int i = 0; i < compare.Length; i++)
			{
				if(stringToCompare.Value == compare[i].Value)
				{
					stringToSet.Value = set[i].Value;
					hasFound = true;
				}

			}

			if(NoMatchEvent != null && hasFound) Fsm.Event(NoMatchEvent);
		}
	}
}
