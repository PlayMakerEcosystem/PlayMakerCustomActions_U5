// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Set the the 'Auto Repath' value of the given NavMeshAgent.")]
	public class NavMeshAgentSetAutoRepath : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;
		
		public FsmBool autoRepath;

		public override void Reset()
		{
			agent = null;
			autoRepath = null;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go) LogError("GameObject is null!");

			go.GetComponent<NavMeshAgent>().autoRepath = autoRepath.Value;

			Finish();
		}
	}
}
