// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "eventTarget")]
	[ActionTarget(typeof(GameObject), "eventTarget")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sets a value of the specified type in another FSM, then sends an Event to it after an optional delay (combines 'Send Event' and 'Set Fsm Variable' for convenience & efficiency).")]
	public class SendEventSetValue : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		[Tooltip("The name of the variable in the target FSM.")]
		public FsmString variableName;

		[Tooltip("The new value for the specified variable.")]
		public FsmVar setValue;

		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;

		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		[Tooltip("Enables the GameObject and FSM if they are disabled before sending the event.")]
		public FsmBool enable;

		[Tooltip("Repeat every frame. Rarely needed, but can be useful when sending events to other FSMs.")]
		public bool everyFrame;

		private PlayMakerFSM targetFsm;
		private NamedVariable targetVariable;
		private INamedVariable sourceVariable;

		private GameObject cachedGameObject;
		private string cachedFsmName;
		private string cachedVariableName;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			eventTarget = null;
			setValue = new FsmVar();
			sendEvent = null;
			delay = null;
			enable = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetFsmVariable();

			if(delay.Value < 0.001f)
			{
				Fsm.Event(eventTarget, sendEvent);
				if(!everyFrame) Finish();
			} else
				delayedEvent = Fsm.DelayedEvent(eventTarget, sendEvent, delay.Value);
		}

		public override void OnUpdate()
		{
			DoSetFsmVariable();

			if(!everyFrame)
			{
				if(DelayedEvent.WasSent(delayedEvent)) Finish();
			} else
				Fsm.Event(eventTarget, sendEvent);
		}

		private void DoSetFsmVariable()
		{
			setValue.UpdateValue();
			if(setValue.IsNone || string.IsNullOrEmpty(variableName.Value)) return;

			var go = Fsm.GetOwnerDefaultTarget(eventTarget.gameObject);

			if(!go) return;

			if(enable.Value)
			{
				go.SetActive(true);
				if(eventTarget.fsmComponent != null)
					eventTarget.fsmComponent.enabled = true;
			}

			string fsmName = eventTarget.fsmName.Value;

			if(go != cachedGameObject || fsmName != cachedFsmName)
			{
				targetFsm = ActionHelpers.GetGameObjectFsm(go, fsmName);

				if(targetFsm == null) return;

				cachedGameObject = go;
				cachedFsmName = fsmName;
			}

			if(variableName.Value != cachedVariableName)
			{
				setValue.UpdateValue();
				targetVariable = targetFsm.FsmVariables.FindVariable(setValue.Type, variableName.Value);
				cachedVariableName = variableName.Value;
			}

			if(targetVariable == null)
			{
				LogWarning("Missing Variable: " + variableName.Value);
				return;
			}
			setValue.UpdateValue();
			setValue.ApplyValueTo(targetVariable);
		}

#if UNITY_EDITOR
		public override string AutoName()
		{
			return ("Set FSM Variable: " + ActionHelpers.GetValueLabel(variableName));
		}
#endif
	}
}
