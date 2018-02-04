// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the acceleration value of the NavMeshAgent.")]
	public class NavMeshAgentGetAcceleration : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat acceleration;

		public override void Reset()
		{
			agent = null;
			acceleration = null;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go) LogError("GameObject is null!");

			acceleration.Value = go.GetComponent<NavMeshAgent>().acceleration;

			Finish();
		}
	}
}
