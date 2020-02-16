// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10464.0

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Detect collisions with objects that have RigidBody components. \nNOTE: The system events, TRIGGER ENTER, TRIGGER STAY, and TRIGGER EXIT are sent when any object collides with the trigger. Use this action to filter collisions by Layer.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10464.0")]
    public class TriggerEventByLayer : FsmStateAction
	{
		public TriggerType trigger;
		[UIHint(UIHint.Layer)]
		public int layer;
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeCollider;

		public override void Reset()
		{
			trigger = TriggerType.OnTriggerEnter;
			layer = 0;
			sendEvent = null;
			storeCollider = null;
		}

		public override void OnPreprocess()
        {
			switch (trigger)
			{
				case TriggerType.OnTriggerEnter:
					Fsm.HandleTriggerEnter = true;
					break;
				case TriggerType.OnTriggerStay:
					Fsm.HandleTriggerStay = true;
					break;
				case TriggerType.OnTriggerExit:
					Fsm.HandleTriggerExit = true;
					break;
			}
		}

		void StoreCollisionInfo(Collider collisionInfo)
		{
			storeCollider.Value = collisionInfo.gameObject;
		}

		public override void DoTriggerEnter(Collider other)
		{
			if (trigger == TriggerType.OnTriggerEnter)
			{
				if (other.gameObject.layer == layer)
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		public override void DoTriggerStay(Collider other)
		{
			if (trigger == TriggerType.OnTriggerStay)
			{
				if (other.gameObject.layer == layer)
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		public override void DoTriggerExit(Collider other)
		{
			if (trigger == TriggerType.OnTriggerExit)
			{
				if (other.gameObject.layer == layer )
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		public override string ErrorCheck()
		{
			return ActionHelpers.CheckOwnerPhysicsSetup(Owner);
		}


	}
}
