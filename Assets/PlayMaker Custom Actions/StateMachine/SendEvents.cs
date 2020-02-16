// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs",
						"Assets/PlayMaker Custom Actions/StateMachine/Editor/SendEventsInspector.cs"
					  ]
}
EcoMetaEnd
---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends an Event to multiple targets.")]
	public class SendEvents : FsmStateActionAdvanced
	{
		[Tooltip("Where to send the event to.\n\n"
			      + "Self: only send the even to this FSM\n\n"
			      + "GameObject: Sends the event to every FSM on the referenced GameObjects (also to all of their children when selected)\n\n"
			      + "GameObjectFSM: Only send the event to specific FSMs on those GameObjects\n\n"
			      + "Broadcast All: Send the event to every FSM in the current scene\n\n"
			      + "Host FSM: When this is a SubFSM, send the event to the host that contains this FSM\n\n"
			      + "Sub FSM: Send the event to a specific/every SubFSM in this FSM.")]
		public FsmEventTarget.EventTarget eventTarget;

		[Tooltip("The GameObject(s) to send the event to.")]
		public FsmGameObject[] gameObjects;

		[Tooltip("The PlayMakerFSM-Components to send the event to.")]
		[ObjectType(typeof(PlayMakerFSM))]
		public FsmObject[] fsmComponents;

		//options
		[Tooltip("Wheter you want to also include all children of the specified GameObject(s).")]
		public FsmBool sendToChildren;

		[Tooltip("Optionally specify the name of the FSM to only send the event to any FSM with that name.")]
		public FsmString FSMName;

		[Tooltip("Wheter to exclude this FSM component from being targeted.")]
		public FsmBool excludeSelf;

		[Tooltip("The name of the Sub-FSM to send the event to.")]
		public FsmString subFSMName;

		//use a work-around for the 'missing local event or global transition' error from here
		//http://hutonggames.com/playmakerforum/index.php?topic=2952.0
		public FsmEventTarget tmpTarget = new FsmEventTarget();
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;

		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		public override void Reset()
		{
			//work-around counter-part
			tmpTarget.target = FsmEventTarget.EventTarget.BroadcastAll;

			eventTarget = FsmEventTarget.EventTarget.GameObjectFSM;
			gameObjects = null;
			fsmComponents = null;
			sendToChildren = false;
			FSMName = "";
			excludeSelf = false;
			subFSMName = "";
			sendEvent = null;
			delay = 0f;

			base.Reset();
		}

		public override void OnEnter()
		{
			DoSendEvents();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoSendEvents();
		}

		private void DoSendEvents()
		{
			if(sendEvent == null)
			{
				UnityEngine.Debug.LogError("'Send Event' is empty in " + LogPath() + "!");
				return;
			}

			switch(eventTarget)
			{
				case FsmEventTarget.EventTarget.Self:
					Send(Fsm.FsmComponent, sendEvent);
					break;
				case FsmEventTarget.EventTarget.GameObject:

					if(gameObjects.Length == 0)
					{
						UnityEngine.Debug.LogError("No GameObjects set in " + LogPath() + "!");
						return;
					}

					foreach(var item in gameObjects)
					{
						if(item.IsNone) continue;

						if(!item.Value)
						{
							UnityEngine.Debug.LogError("Some GameObjects are null in " + LogPath() + "!");
							return;
						}

						if(!item.Value.GetComponent<PlayMakerFSM>())
						{
							UnityEngine.Debug.LogError("GameObject " + item.Value.name + " in " + LogPath() 
													    + " doesn't have a PlayMakerFSM component to send to!");
							return;
						}

						Fsm.BroadcastEventToGameObject(item.Value, sendEvent.ToString(), sendToChildren.Value);
					}
					break;
				case FsmEventTarget.EventTarget.GameObjectFSM:

					if(gameObjects.Length == 0)
					{
						UnityEngine.Debug.LogError("No GameObjects set in " + LogPath() + "!");
						return;
					}

					foreach(var item in gameObjects)
					{
						if(item.IsNone) continue;

						if(!item.Value)
						{
							UnityEngine.Debug.LogError("Some GameObjects are null in " + LogPath() + "!");
							return;
						}

						if(!item.Value.GetComponent<PlayMakerFSM>())
						{
							UnityEngine.Debug.LogError("GameObject " + item.Value.name + " in " + LogPath() 
													    + " doesn't have a PlayMakerFSM component to send to");
							return;
						}

						foreach(var currComponent in item.Value.GetComponents<PlayMakerFSM>())
						{
							if(FSMName.Value != "")
							{
								if(currComponent.FsmName != FSMName.Value) continue;
							} 

							Send(currComponent, sendEvent);
						}
					}
					break;
				case FsmEventTarget.EventTarget.FSMComponent:

					if(fsmComponents.Length == 0)
					{
						UnityEngine.Debug.LogError("No FSMComponents in " + LogPath() + "set!");
						return;
					}

					int i = 0;
					foreach(var item in fsmComponents)
					{
						if(item.IsNone) continue;
						
						i++;
						if(item != null)
						{
							Send(item.Value as PlayMakerFSM, sendEvent);
						} else
						{
							UnityEngine.Debug.LogError("FSM-Component #" + (i + 1).ToString() 
													    + " in " + LogPath() + " is Null!");
						}
					}
					break;
				case FsmEventTarget.EventTarget.BroadcastAll:
					if(!sendEvent.IsGlobal)
					{
						UnityEngine.Debug.LogError("You are trying to broadcast an event that isn't global in " 
													+ LogPath() + "!");
						return;
					}

					Fsm.BroadcastEvent(sendEvent, excludeSelf.Value);
					break;
				case FsmEventTarget.EventTarget.HostFSM:
					if(Fsm.Host == null)
					{
						UnityEngine.Debug.LogError("Current FSM from "
													+ LogPath() + " isn't a SubFSM!");
						return;
					}

					Send(Fsm.Host.FsmComponent, sendEvent);
					break;
				case FsmEventTarget.EventTarget.SubFSMs:
					if(Fsm.GetSubFsm(subFSMName.Value) == null)
					{
						UnityEngine.Debug.LogError("Current FSM from "
													+ LogPath() + " doesn't have a SubFSM called " 
													+ subFSMName.Value + "!");
						return;
					}

					Send(Fsm.GetSubFsm(subFSMName.Value).FsmComponent, sendEvent);
					break;
				default:
					break;
			}
		}

		private void Send(PlayMakerFSM target, FsmEvent fsmEvent)
		{
			if(delay.Value < 0.001f 
			   || eventTarget == FsmEventTarget.EventTarget.BroadcastAll
			   || everyFrame) target.Fsm.Event(fsmEvent);
			else target.Fsm.DelayedEvent(sendEvent, delay.Value);
		}

		private string LogPath()
		{
			return Owner.name + "(\"" + Fsm.Name + "\")>\"" 
				   + Fsm.FsmComponent.ActiveStateName + "\">\"SendEvents\"";
		}

		public override string ErrorCheck()
		{
			return base.ErrorCheck();
		}
	}
}
