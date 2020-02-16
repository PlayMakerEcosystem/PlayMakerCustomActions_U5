// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

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
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the closest agent to the specified one.")]
	public class FindClosestAgent : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The GameObject containing the NavMeshAgent component to get the closest agent of.")]
		public FsmOwnerDefault agent;

		[UIHint(UIHint.Tag)]
		[Tooltip("Only consider objects with this Tag.")]
		public FsmString withTag;

		[UIHint(UIHint.Tag)]
		[Tooltip("Only consider objects without this Tag.")]
		public FsmString ignoreTag;

		[Tooltip("Only consider objects visible to the camera.")]
		public FsmBool mustBeVisible;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the closest GameObject with an NavMeshAgent component.")]
		public FsmGameObject closestAgent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance to the closest agent.")]
		public FsmFloat storeDistance;

		private GameObject go;

		public override void Reset()
		{
			//resets 'everyFrame' and 'updateType'
			base.Reset();

			agent = null;
			withTag = "Untagged";
			ignoreTag = "Untagged";
			mustBeVisible = false;
			closestAgent = null;
			storeDistance = null;

			go = null;
		}

		public override void OnEnter()
		{
			DoTemplate();

			if(!everyFrame)
				Finish();
		}

		public override void OnActionUpdate()
		{
			DoTemplate();
		}

		private void DoTemplate()
		{
			go = Fsm.GetOwnerDefaultTarget(agent);

			if(!go)
				LogError("GameObject in " + Owner.name + " (" + Fsm.Name + ") is null!");

			NavMeshAgent[] allNavMeshAgents = GameObject.FindObjectsOfType<NavMeshAgent>();

			GameObject closestObj = null;
			var closestDist = Mathf.Infinity;

			foreach(var agent in allNavMeshAgents)
			{
				//skip if equals specified agent
				if(agent.gameObject == go) continue;

				if(!string.IsNullOrEmpty(withTag.Value) && withTag.Value != "Untagged" && !withTag.IsNone)
				{
					if(agent.gameObject.tag != withTag.Value) continue;
				}

				if(!string.IsNullOrEmpty(ignoreTag.Value) && ignoreTag.Value != "Untagged" && !ignoreTag.IsNone)
				{
					if(agent.gameObject.tag == ignoreTag.Value) continue;
				}

				if(mustBeVisible.Value && !ActionHelpers.IsVisible(agent.gameObject)) continue;

				var dist = (go.transform.position - agent.transform.position).sqrMagnitude;

				if(dist < closestDist)
				{
					closestDist = dist;
					closestObj = agent.gameObject;
				}
			}

			closestAgent.Value = closestObj;

			if(!storeDistance.IsNone)
				storeDistance.Value = Mathf.Sqrt(closestDist);
		}
	}
}
