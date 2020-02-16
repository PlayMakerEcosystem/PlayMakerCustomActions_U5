// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: line cast extra ray cast

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Casts a line like a trip wire between the from and to position/gameObject. Option to ignore colliders set to trigger.")]
	public class PhysicsLineCast2 : FsmStateAction
	{
		//[ActionSection("Setup Linecast")]

		[Tooltip("Start line at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("Start line at a vector3 world position. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;

		[Tooltip("The end position of the linecast sweep. \nOr use ToPosition parameter.")]
		public FsmGameObject toGameObject;

		[Tooltip("The end position of the linecast sweep. \nOr use ToGameObject parameter.")]
		public FsmVector3 toPosition;

		[Tooltip("Set to true to ignore colliders set to trigger.")]
		public FsmBool ignoreTriggerColliders;

		[ActionSection("Result")]

		[Tooltip("Event to send if the line hits an object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent hitEvent;

		[Tooltip("Event to send if the ray does not hit any object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent noHitEvent;

		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;

		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeHitObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the line hit point and store it in a variable.")]
		public FsmVector3 storeHitPoint;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		public FsmVector3 storeHitNormal;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the line to the hit point and store it in a variable.")]
		public FsmFloat storeHitDistance;

		[ActionSection("Filter")]

		[Tooltip("Set how often to cast a line. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
		public FsmInt repeatInterval;

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		int repeat;

        [ActionSection("Debug")]

        [Tooltip("The color to use for the debug line.")]
        public FsmColor debugColor;

        [Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
        public FsmBool debug;

        public override void Reset()
		{
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };	
			hitEvent = null;
			storeDidHit = null;
			storeHitObject = null;
			storeHitPoint = null;
			storeHitNormal = null;
			storeHitDistance = null;
			repeatInterval = 1;
			layerMask = new FsmInt[0];
			invertMask = false;
			ignoreTriggerColliders = false;
            toGameObject = null;
            toPosition = null;
		}

		public override void OnEnter()
		{
			DoRaycast();

			if (repeatInterval.Value == 0)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
            repeat--;

            if (repeat == 0)
			{
				DoRaycast();
			}
		}

		void DoRaycast()
		{
			repeat = repeatInterval.Value;

			var go = Fsm.GetOwnerDefaultTarget(fromGameObject);

			Vector3 startPos = go != null ? go.transform.position : fromPosition.Value;
			Vector3 endPos =  toGameObject.Value != null ? toGameObject.Value .transform.position : toPosition.Value;
				
			RaycastHit hitInfo;

			if (ignoreTriggerColliders.Value == true)
			{
				Physics.Linecast(startPos,endPos,out hitInfo, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Ignore);
			}
			else
			{
				Physics.Linecast(startPos,endPos,out hitInfo, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);
			}


			Fsm.RaycastHitInfo = hitInfo;

			var didHit = hitInfo.collider != null;

			storeDidHit.Value = didHit;

			if (didHit)
			{
				storeHitObject.Value = hitInfo.collider.gameObject;
				storeHitPoint.Value = Fsm.RaycastHitInfo.point;
				storeHitNormal.Value = Fsm.RaycastHitInfo.normal;
				storeHitDistance.Value = Fsm.RaycastHitInfo.distance;
				Fsm.Event(hitEvent);
			}

			if (!didHit)
			{

				Fsm.Event(noHitEvent);
			}
            if (debug.Value)
            {
               
                Debug.DrawLine(startPos, endPos, debugColor.Value);
            }


        }
	}
}

