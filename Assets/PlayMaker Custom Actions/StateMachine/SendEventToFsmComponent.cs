// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "eventTarget")]
	[ActionTarget(typeof(GameObject), "eventTarget")]
	[Tooltip("Sends an Event after an optional delay to an FsmComponent")]
	public class SendEventToFsmComponent : FsmStateAction
	{
		[Tooltip("The Fsm component to send event to")]
		[ObjectType(typeof(PlayMakerFSM))]
		public FsmObject target;

		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmString sendEvent;

		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		[Tooltip("Repeat every frame. Rarely needed, but can be useful when sending events to other FSMs.")]
		public bool everyFrame;

		private DelayedEvent delayedEvent;

		FsmEventTarget eventTarget;

		public override void Reset()
		{
			eventTarget = null;
			sendEvent = null;
			delay = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{

			eventTarget = new FsmEventTarget ();
			eventTarget.target = FsmEventTarget.EventTarget.FSMComponent;
			eventTarget.fsmComponent = target.Value as PlayMakerFSM;

			if (delay.Value < 0.001f)
			{
				
				Fsm.Event(eventTarget,sendEvent.Value);
				if (!everyFrame)
				{
					Finish();
				}
			}
			else
			{
				

				delayedEvent = Fsm.DelayedEvent(eventTarget, FsmEvent.FindEvent (sendEvent.Value), delay.Value);
			}
		}

		public override void OnUpdate()
		{
			if (!everyFrame)
			{
				if (DelayedEvent.WasSent(delayedEvent))
				{
					Finish();
				}
			}
			else
			{
				Fsm.Event(eventTarget, sendEvent.Value);
			}
		}

		#if UNITY_EDITOR

		public override float GetProgress()
		{
			if (delayedEvent != null)
				return Mathf.Min(delayedEvent.GetProgress());
			return 0f;
		}

		#endif
	}
}