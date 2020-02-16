// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEngine;

[CustomActionEditor(typeof(SendEvents))]
public class SendEventsInspector : CustomActionEditor
{
	//store references to the target script
	private static SendEvents action;
	private PlayMakerFSM thisFSM;

	//set this to true on every change to only update OnGUI when something has differed from the last call
	private bool isDirty = false;

	//gets called when interacting with the action window or if something has changed
	public override bool OnGUI()
	{
		//reset isDirty
		isDirty = false;

		//get action and FSM reference
		action = target as SendEvents;

		EditField("eventTarget");

		switch(action.eventTarget)
		{
			case FsmEventTarget.EventTarget.Self:
			default:
				break;
			case FsmEventTarget.EventTarget.GameObject:
				EditField("gameObjects");
				EditField("sendToChildren");
				break;
			case FsmEventTarget.EventTarget.GameObjectFSM:
				EditField("gameObjects");
				EditField("FSMName");
				break;
			case FsmEventTarget.EventTarget.FSMComponent:
				EditField("fsmComponents");
				break;
			case FsmEventTarget.EventTarget.BroadcastAll:
				EditField("excludeSelf");
				break;
			case FsmEventTarget.EventTarget.HostFSM:
				break;
			case FsmEventTarget.EventTarget.SubFSMs:
				EditField("subFSMName");
				break;
		}

		EditField("sendEvent");
		if(action.eventTarget != FsmEventTarget.EventTarget.BroadcastAll
		   && !action.everyFrame) EditField("delay");
		EditField("everyFrame");
		EditField("updateType");

		//needs to be at the end and tells OnGUI if something has changed
		return isDirty || GUI.changed;
	}

}
