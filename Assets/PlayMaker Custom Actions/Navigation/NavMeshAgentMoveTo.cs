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

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Sets the destination of the given NavMesh Agent to an object or position (only updates when the target has moved).")]
	public class NavMeshAgentMoveTo : FsmStateActionAdvanced
	{
		[CheckForComponent(typeof(NavMeshAgent))]
		[RequiredField]
		[Tooltip("The GameObject with the Agent component attached.")]
		public FsmOwnerDefault agent;

		[Tooltip("Set the GameObject to move to.")]
		public FsmGameObject target;

		[Tooltip("Alternatively define the position where the agent is supposed to move to directly.")]
		public FsmVector3 targetPosition;

		[Tooltip("Wheter the given position is in local or world space.")]
		public Space space;

		[ActionSection("Result")]
		[Tooltip("Send an event when the NavMesh Agent has reached the target (dependent on slider below). Only works when 'Every Frame' is selected.")]
		public FsmEvent reachedTargetEvent;

		[HasFloatSlider(0f, 100f)]
		[Tooltip("Determines the distance to the target in % from where the event should be sent (0 = at target, 100 = at start).")]
		public FsmFloat offsetFromTarget;

		[Tooltip("Wheter the agent should stop where it's currently at when leaving this state or still keep going.")]
		public FsmBool stopAgentOnExit;

		private Vector3 tPos;
		private Vector3 prevTargetPos;

		private NavMeshAgent compAgent;

		public override void Reset()
		{
			agent = null;
			target = new FsmGameObject() { UseVariable = true };
			targetPosition = new FsmVector3() { UseVariable = true };
			space = Space.World;
			reachedTargetEvent = null;
			offsetFromTarget = 0f;
			stopAgentOnExit = false;

			tPos = new Vector3();
			prevTargetPos = new Vector3();
			compAgent = null;

			everyFrame = true;
			updateType = FrameUpdateSelector.OnFixedUpdate;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go)
			{
				UnityEngine.Debug.LogError("GameObject is null!");
				return;
			}

			//get Agent component
			compAgent = go.GetComponent<NavMeshAgent>();

			OnActionUpdate();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			UpdateDestination();

			//check if path is still being calculated to counteract asynchronicity of NavMeshAgent.SetDestination()
			if(compAgent.pathPending) return;

			if(compAgent.remainingDistance > compAgent.stoppingDistance) return;

			if(!compAgent.hasPath || compAgent.velocity.sqrMagnitude == 0f)
			{
				Fsm.Event(reachedTargetEvent);
			}
		}

		void UpdateDestination()
		{
			if(targetPosition.IsNone && target.IsNone)
			{
				UnityEngine.Debug.LogWarning("Please specify either the Target Position or Target.");
				return;
			}

			tPos = targetPosition.Value;

			if(!target.IsNone || target.Value != null)
			{
				if(space == Space.World)
				{
					tPos = target.Value.transform.position;
				} else
				{
					tPos = target.Value.transform.localPosition;
				}
			}

			//skip if target position hasn't changed
			if(prevTargetPos == tPos) return;

			//only update NavMesh Agent's position if it's on a NavMesh
			if(compAgent.isOnNavMesh)
			{
				//move NavMesh Agent to position
				compAgent.SetDestination(tPos);
			} else
			{
				UnityEngine.Debug.LogError("Agent " + compAgent.gameObject.name + " isn't on a NavMesh!");
				return;
			}

			prevTargetPos = tPos;
		}

		public override void OnExit()
		{
			if(stopAgentOnExit.Value)
			{
				compAgent.SetDestination(compAgent.gameObject.transform.position);
			}
		}
	}
}
