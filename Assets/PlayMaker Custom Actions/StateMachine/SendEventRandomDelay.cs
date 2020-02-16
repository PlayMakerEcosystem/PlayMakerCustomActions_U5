// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "eventTarget")]
	[ActionTarget(typeof(GameObject), "eventTarget")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends an Event after a random delay. NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	public class SendEventRandomDelay : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;

		[Tooltip("Start delay in seconds.")]
		public FsmFloat delayMin;

		[Tooltip("End delay in seconds.")]
		public FsmFloat delayMax;

		[Tooltip("Wheter the delay is game time independent.")]
		public FsmBool realTime;

		private DelayedEvent delayedEvent;
		private float startTime;
		private float timer;
		private float time;

		public override void Reset()
		{
			eventTarget = null;
			sendEvent = null;
			delayMin = 0f;
			delayMax = 1f;
			realTime = true;
		}

		public override void OnEnter()
		{
			time = Random.Range(delayMin.Value, delayMax.Value);

			if(time < 0.001f)
			{
				Fsm.Event(eventTarget, sendEvent);
				Finish();
			} else
			{
				startTime = FsmTime.RealtimeSinceStartup;
				timer = 0f;
				delayedEvent = Fsm.DelayedEvent(eventTarget, sendEvent, time);
			}
		}

		public override void OnUpdate()
		{
			if(realTime.Value)
			{
				timer = FsmTime.RealtimeSinceStartup - startTime;
			} else
			{
				timer += Time.deltaTime;
			}

			if(timer >= time)
			{
				Fsm.Event(eventTarget, sendEvent);
			}

			if (DelayedEvent.WasSent(delayedEvent))
			{
				Finish();
			}

		}
	}
}