// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Restarts the current FSM at the beginning (the Start State) after an optional delay. " +
			 "NOTE: Make sure to not cause any infinite loops by having a direct " +
			 "path from the Start State to this action without any delay in between or in this action.")]
	public class RestartThisFSM : FsmStateAction
	{
		[ActionSection("Optional")]
		[RequiredField]
		public FsmFloat delay;
		public bool realTime;

		private float startTime;
		private float timer;

		public override void Reset()
		{
			delay = 0.1f;
			realTime = false;
		}

		public override void OnEnter()
		{
			if(delay.Value <= 0) DoRestart();

			startTime = FsmTime.RealtimeSinceStartup;
			timer = 0f;
		}

		public override void OnUpdate()
		{
			if(realTime)
				timer = FsmTime.RealtimeSinceStartup - startTime;
			else
				timer += Time.deltaTime;

			if(timer >= delay.Value) DoRestart();
		}

		void DoRestart()
		{
			string startState = Fsm.StartState;
			Fsm.SetState(startState);
		}
	}
}
