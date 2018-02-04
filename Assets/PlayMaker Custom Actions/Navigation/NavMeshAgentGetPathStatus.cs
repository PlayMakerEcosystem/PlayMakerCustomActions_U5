// License: Attribution 4.0 International (CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Navigation")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Send an event depending on the current status of the path for the selected agent.")]
	public class NavMeshAgentGetPathStatus : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[Tooltip("Event to send when the path was created fully.")]
		public FsmEvent completeEvent;

		[Tooltip("Event to send when the path is only partially created or during the process of creation.")]
		public FsmEvent partialEvent;

		[Tooltip("Event to send if the agent currently has no path or it failed to create one.")]
		public FsmEvent invalidEvent;

		[Tooltip("If the path is partially created, continuously check for completion and then send the 'Complete Event'.")]
		public FsmBool waitTillCompletion;

		private GameObject go;

		public override void Reset()
		{
			agent = null;
			completeEvent = null;
			partialEvent = null;
			invalidEvent = null;
			waitTillCompletion = false;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go)
				LogError("GameObject is null!");

			if(!waitTillCompletion.Value)
			{
				switch(go.GetComponent<NavMeshAgent>().pathStatus)
				{
					case NavMeshPathStatus.PathComplete:
						Fsm.Event(completeEvent);
						break;
					case NavMeshPathStatus.PathPartial:
						Fsm.Event(partialEvent);
						break;
					case NavMeshPathStatus.PathInvalid:
						Fsm.Event(invalidEvent);
						break;
					default:
						break;
				}

				Finish();
			}
		}

		public override void OnUpdate()
		{
			if(!go)
				LogError("GameObject is null!");

			if(!waitTillCompletion.Value)
				return;

			if(go.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
			{
				Fsm.Event(completeEvent);
				Finish();
			}
		}
	}
}
