// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the area mask the given NavMeshAgent is on. " +
			 "Optionally send an event if the area mask has changed.")]
	public class NavMeshAgentGetAreaMask : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[Tooltip("The GameObject with the NavMeshAgent component attached.")]
		public FsmOwnerDefault agent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt areaMask;

		public FsmEvent hasChangedEvent;

		[Tooltip("Wheter to run this action on every frame or only once.")]
		public FsmBool everyFrame;

		private bool isInit;
		private int prevMask;

		public override void Reset()
		{
			agent = null;
			areaMask = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetAreaMask();

			isInit = true;

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			if(!isInit)
			{
				if(prevMask != areaMask.Value)
				{
					Fsm.Event(hasChangedEvent);
				}
			}

			GetAreaMask();
		}

		void GetAreaMask()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(agent);
			if(!go) LogError("GameObject is null!");

			NavMeshAgent nmAgent = go.GetComponent<NavMeshAgent>();

			NavMeshHit _NavMeshHit;
			bool _nearestPointFound = NavMesh.SamplePosition(go.transform.position, out _NavMeshHit,
															 nmAgent.height, NavMesh.AllAreas);

			areaMask.Value = _NavMeshHit.mask;
			prevMask = areaMask.Value;
		}
	}
}
