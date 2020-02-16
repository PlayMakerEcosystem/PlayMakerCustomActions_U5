// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Deek

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
	[Tooltip("Gets the number of children that a GameObject has. Adds the options to get all children recursively, only return those that are enabled/disabled and get the amount every frame.")]
	public class GetChildCountAdvanced : FsmStateActionAdvanced
	{
		public enum ActiveType
		{
			ActiveInHirarchy,
			ActiveSelf,
			Active
		}

		[RequiredField]
		[Tooltip("The GameObject to get the child amount from.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the number of children in an int variable.")]
		public FsmInt storeResult;

		[Title("Only Get Active/Inactive")]
		[Tooltip("Wheter to only get children that are enabled or disabled. Gets ignored if set to 'None'.")]
		public FsmBool getActive;

		[Tooltip("What active type should be the children checked against.\n\nActiveInHirarchy: If active in the scene\n\nActiveSelf: Is the GameObject itself active independent of any parents state\n\nActive: Deprecated function included to support pre Unity 4 users")]
		public ActiveType activeType;

		[Tooltip("Wheter to even get all children of children.")]
		public FsmBool recursive;

		private GameObject go;
		private int tmpResult;

		public override void Reset()
		{
			//resets 'everyFrame' and 'updateType'
			base.Reset();

			gameObject = null;
			storeResult = null;
			getActive = new FsmBool() { UseVariable = true };
			activeType = ActiveType.ActiveInHirarchy;
			recursive = false;
			go = null;
			tmpResult = 0;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			GoThroughChildren();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			GoThroughChildren();
		}

		private void GoThroughChildren()
		{
			tmpResult = 0;

			if(!go)
			{
				LogError("GameObject in " + Owner.name + " (" + Fsm.Name + ") is null!");
				return;
			}


			for(int i = 0; i < go.transform.childCount; i++)
			{
				Transform currChildTrans = go.transform.GetChild(i);

				AddCurrentChild(currChildTrans.gameObject);

				if(recursive.Value && currChildTrans.childCount > 0) Recursive(currChildTrans);
			}

			storeResult.Value = tmpResult;
		}

		private void Recursive(Transform rootTrans)
		{
			for(int j = 0; j < rootTrans.childCount; j++)
			{
				Recursive(rootTrans.GetChild(j));
			}

			AddCurrentChild(rootTrans.gameObject);
		}

		private void AddCurrentChild(GameObject currGO)
		{
			if(!getActive.IsNone)
			{
				bool isActive = false;

				switch(activeType)
				{
					case ActiveType.ActiveInHirarchy:
						isActive = currGO.activeInHierarchy;
						break;
					case ActiveType.ActiveSelf:
						isActive = currGO.activeSelf;
						break;
					case ActiveType.Active:
					#if UNITY_5_3_OR_NEWER
						isActive = currGO.activeInHierarchy;
					#else
						isActive = currGO.active;
					#endif
						break;
					default:
						break;
				}

				if(getActive.Value && isActive) tmpResult += 1;
				if(!getActive.Value && !isActive) tmpResult += 1;
			} else
			{
				tmpResult += 1;
			}
		}
	}
}
