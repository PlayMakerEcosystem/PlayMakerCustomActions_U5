// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the velocity value of the NavMeshAgent.")]
	public class NavMeshAgentGetVelocity : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 velocity;

		[Tooltip("Wheter to update this action every frame.")]
		public FsmBool everyFrame;

		private GameObject go;

		public override void Reset()
		{
			agent = null;
			velocity = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go) LogError("GameObject is null!");

			velocity.Value = go.GetComponent<NavMeshAgent>().velocity;

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			velocity.Value = go.GetComponent<NavMeshAgent>().velocity;
		}
	}
}
