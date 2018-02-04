// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Check if the given NavMeshAgent is currently on an OffMeshLink or not.")]
	public class NavMeshAgentIsOnOffMeshLink : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool isOnOffMeshLink;

		public override void Reset()
		{
			agent = null;
			isOnOffMeshLink = null;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go) LogError("GameObject is null!");

			isOnOffMeshLink.Value = go.GetComponent<NavMeshAgent>().isOnOffMeshLink;

			Finish();
		}
	}
}
