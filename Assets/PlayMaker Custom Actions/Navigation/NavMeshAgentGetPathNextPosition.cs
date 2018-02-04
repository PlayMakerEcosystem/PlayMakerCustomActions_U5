// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the next position of the path the NavMeshAgent is on.")]
	public class NavMeshAgentGetNextPosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 nextPosition;

		[Tooltip("Send an event when the agent reached its destination. Only works when 'Every Frame' is checked.")]
		public FsmEvent eventOnFinish;

		[Tooltip("Wheter to update this action every frame.")]
		public FsmBool everyFrame;

		private GameObject go;
		private NavMeshAgent navAgent;

		public override void Reset()
		{
			agent = null;
			nextPosition = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go) LogError("GameObject is null!");

			navAgent = go.GetComponent<NavMeshAgent>();

			nextPosition.Value = navAgent.nextPosition;

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			nextPosition.Value = navAgent.nextPosition;

			if(navAgent.remainingDistance == 0) Fsm.Event(eventOnFinish);
		}
	}
}
