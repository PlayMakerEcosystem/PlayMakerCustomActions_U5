// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Tests a String against Multiple to see if it is within one of them.")]
	public class StringContainsSwitch : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;
		[CompoundArray("String Switches", "Compare String", "Send Event")]
		public FsmString[] compareTo;
		public FsmEvent[] sendEvent;
		[HutongGames.PlayMaker.Tooltip("Event to raise if no matches are found")]
		public FsmEvent NoMatchEvent;
		public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			compareTo = new FsmString[1];
			sendEvent = new FsmEvent[1];
			NoMatchEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStringContainsSwitch();

			if(!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoStringContainsSwitch();
		}

		void DoStringContainsSwitch()
		{
			for(int i = 0; i < compareTo.Length; i++)
			{
				if(stringVariable.Value.Contains(compareTo[i].Value))
				{
					Fsm.Event(sendEvent[i]);
					return;
				}

			}
			if(NoMatchEvent != null)
			{
				Fsm.Event(NoMatchEvent);
				return;
			}
		}
	}
}
