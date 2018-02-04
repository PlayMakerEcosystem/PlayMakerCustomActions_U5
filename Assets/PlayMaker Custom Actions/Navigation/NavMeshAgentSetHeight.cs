// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Set the height of the NavMeshAgent.")]
	public class NavMeshAgentSetHeight : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		public FsmFloat height;

		public override void Reset()
		{
			agent = null;
			height = null;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go)
				LogError("GameObject is null!");

			go.GetComponent<NavMeshAgent>().height = height.Value;

			Finish();
		}
	}
}
