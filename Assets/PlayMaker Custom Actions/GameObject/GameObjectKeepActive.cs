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

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Keeps multiple GameObjects active, as long as the other GameObject is.")]
	public class GameObjectKeepActive : FsmStateActionAdvanced
	{
		public enum CheckState
		{
			IsActive,
			IsNotActive
		}

		public enum ActiveType
		{
			ActiveInHirarchy,
			ActiveSelf
		}

		[RequiredField]
		[Tooltip("Specify the GameObject that is being checked against.")]
		public FsmOwnerDefault gameObjectToCheck;

		[RequiredField]
		[Tooltip("Specify the GameObject that is being checked against.")]
		public FsmGameObject[] keepActive;

		[Tooltip("What state the GameObject to check must be in order to active the given GameObjects.")]
		public CheckState state;

		[Tooltip("What active state should be checked.\n'Active In Hirarchy' = If the GameObject and all " +
				 "of it's parents are active (a.k.a. if it's visible in the scene);\n" +
				 "'ActiveSelf' = If the GameObject is active independent of its parents.")]
		public ActiveType activeType;

		private GameObject go;

		public override void Reset()
		{
			gameObjectToCheck = null;
			keepActive = null;
			state = CheckState.IsActive;
			activeType = ActiveType.ActiveInHirarchy;

			go = null;

			everyFrame = true;
			updateType = FrameUpdateSelector.OnUpdate;
		}

		public override void OnEnter()
		{
			DoKeepActive();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoKeepActive();
		}

		private void DoKeepActive()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObjectToCheck);

			if(!go)
			{
				LogError("GameObject in " + Owner.name + " (" + Fsm.Name + ") is null!");
				return;
			}

			foreach(var ka in keepActive)
			{
				bool secReq = false;

				switch(state)
				{
					case CheckState.IsActive:
						switch(activeType)
						{
							case ActiveType.ActiveSelf:
								if(go.activeSelf) secReq = true;
								break;
							case ActiveType.ActiveInHirarchy:
								if(go.activeInHierarchy) secReq = true;
								break;
						}
						break;
					case CheckState.IsNotActive:
						switch(activeType)
						{
							case ActiveType.ActiveSelf:
								if(!go.activeSelf) secReq = true;
								break;
							case ActiveType.ActiveInHirarchy:
								if(!go.activeInHierarchy) secReq = true;
								break;
						}
						break;
				}

				ka.Value.SetActive(secReq);
			}
		}

		public override void OnExit()
		{
			if(gameObjectToCheck.OwnerOption == OwnerDefaultOption.UseOwner) DoKeepActive();
		}
	}
}
