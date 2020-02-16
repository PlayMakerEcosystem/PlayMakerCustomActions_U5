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
	[Tooltip("Sets multiple values of the specified types in another FSM, then sends an Event to it after an optional delay (combines 'Send Event' and 'Set Fsm Variable' for convenience & efficiency).")]
	public class SendEventSetMulti : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		[CompoundArray("Variable Amount", "Variable Name", "Set Value")]
		[Tooltip("The name of the variable in the target FSM.")]
		public FsmString[] variableName;

		[Tooltip("The new value for the specified variable.")]
		public FsmVar[] setValue;

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

		private GameObject cachedGameObject;
		private string cachedFsmName;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			eventTarget = null;
			variableName = new FsmString[2];
			setValue = null;
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
			var go = Fsm.GetOwnerDefaultTarget(eventTarget.gameObject);

			if(go == null) return;

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

			for(int i = 0; i < variableName.Length; i++)
			{
				//ignore values that are not set/defined
				setValue[i].UpdateValue();
				if(setValue[i].IsNone || string.IsNullOrEmpty(variableName[i].Value)) continue;

				if(targetFsm.FsmVariables.Contains(variableName[i].Value))
				{
					targetVariable = targetFsm.FsmVariables.GetVariable(variableName[i].Value);
				} else
				{
					LogError(targetFsm.name + " doesn't contain variable " + variableName[i].Value);
					return;
				}
				setValue[i].UpdateValue();
				setValue[i].ApplyValueTo(targetVariable);
			}
		}
	}
}
