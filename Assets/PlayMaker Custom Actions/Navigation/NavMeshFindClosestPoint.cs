// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					  ]
}
EcoMetaEnd
---*/

///ToDo: Also return a Success or Failed Event as result

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Searches for the closest point on any NavMesh from the given position. Useful to lead/teleport an Agent to a point of interest or clicked position, independent of where the agent is currently at.")]
	public class NavMeshFindClosestPoint : FsmStateActionAdvanced
	{
		[Tooltip("From what GameObject should the closest NavMesh point be searched.")]
		public FsmOwnerDefault source;

		[Tooltip("Alternatively set the source as a Vector3. If set, 'Source' gets ignored.")]
		public FsmVector3 sourcePosition;

		[Tooltip("Define the max distance from the source to search for a NavMesh. A big search radius can get quite expensive; a good starting point would be twice the Agent's height.")]
		public FsmFloat maxDistance;

		[Tooltip("Wheter the source position and possible outcome is seen as being in world or local space.")]
		public Space space;


		[ActionSection("Result")]

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The closest NavMesh point from the source, if found.")]
		public FsmVector3 nearestPoint;

		[Tooltip("Event to send when a nearest point was found.")]
		public FsmEvent succeeded;

		[Tooltip("Event that gets fired when there was no NavMesh to be found. Increase the Max Distance to see, if it can find it, unless this is the desired result.")]
		public FsmEvent failed;

		private GameObject go = null;

		public override void Reset()
		{
			//resets 'everyFrame' and 'updateType'
			base.Reset();

			source = null;
			sourcePosition = new FsmVector3() { UseVariable = true };
			maxDistance = 100f;
			space = Space.World;
			nearestPoint = new FsmVector3();
			succeeded = null;
			failed = null;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(source);

			DoTemplate();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoTemplate();
		}

		private void DoTemplate()
		{
			if(!go)
			{
				LogError("GameObject in " + Owner.name + " (" + Fsm.Name + ") is null!");
				return;
			}

			NavMeshHit hit = new NavMeshHit();
			Vector3 pos = go ? (space == Space.World ? go.transform.position
													 : go.transform.localPosition)
							 : new Vector3();

			if(!sourcePosition.IsNone)
			{
				pos = sourcePosition.Value;
			}

			NavMesh.SamplePosition(pos, out hit, maxDistance.Value, NavMesh.AllAreas);

			if(hit.hit)
			{
				nearestPoint.Value = hit.position;
				Fsm.Event(succeeded);
			} else
			{
				Fsm.Event(failed);
			}
		}
	}
}
