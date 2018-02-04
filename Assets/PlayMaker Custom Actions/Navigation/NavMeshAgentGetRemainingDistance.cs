// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the remaining distance of the current NavMeshAgent's position. Only works when the NavMeshAgent hat a path.")]
	public class NavMeshAgentGetRemainingDistance : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat remainingDistance;

		[Tooltip("Wheter to update this action every frame.")]
		public FsmBool everyFrame;

		private GameObject go;
		private NavMeshAgent navAgent;

		public override void Reset()
		{
			agent = null;
			remainingDistance = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go) LogError("GameObject is null!");

			navAgent = go.GetComponent<NavMeshAgent>();

			remainingDistance.Value = navAgent.remainingDistance;

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			remainingDistance.Value = navAgent.remainingDistance;
		}
	}
}
