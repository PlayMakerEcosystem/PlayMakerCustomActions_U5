// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "eventTarget")]
	[ActionTarget(typeof(GameObject), "eventTarget")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends an Event to multiple GameObjects. (useful if you don't want to broadcast an Event or don't want several similar 'Send Event'-Actions). Sends the Event only to the first FSM Component on that GameObject.")]
	public class SendEventMulti : FsmStateAction
	{
		[Tooltip("Where to send the event. Specify the Amount in the first line.")]
		public FsmGameObject[] eventTarget;

		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;

		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		[Tooltip("Repeat every frame. Rarely needed, but can be useful when sending events to other FSMs.")]
		public bool everyFrame;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			eventTarget = new FsmGameObject[3];
			sendEvent = null;
			delay = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			//Go through all GameObjects in the Array
			foreach(var i in eventTarget)
			{

				//get the FSM-Component of the current GO
				var comp = i.Value.GetComponent<PlayMakerFSM>();

				if(delay.Value < 0.001f)
				{
					comp.Fsm.Event(sendEvent);
					if(!everyFrame)
					{
						Finish();
					}
				} else
				{
					comp.Fsm.DelayedEvent(sendEvent, delay.Value);
				}
			}

		}

		public override void OnUpdate()
		{
			if(!everyFrame)
			{
				if(DelayedEvent.WasSent(delayedEvent))
				{
					Finish();
				}
			} else
			{
				foreach(var i in eventTarget)
				{
					var comp = i.Value.GetComponent<PlayMakerFSM>();
					comp.Fsm.Event(sendEvent);
				}

			}
		}
	}
}
