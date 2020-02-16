// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enables/Disables all children from the specified parent in a given range.")]
	public class EnableChildrenInRange : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to change the children off of.")]
		public FsmOwnerDefault parent;

		[Tooltip("From which index to start enabling/disabling from. Leave at 0 to start from the first child.")]
		public FsmInt startingFrom;

		[Tooltip("From which index to end enabling/disabling at. Leave at 0 to end at the last child.")]
		public FsmInt endingAt;

		public bool enable;

		public override void Reset()
		{
			parent = null;
			startingFrom = 0;
			endingAt = 0;
			enable = true;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(parent);

			if(!go)
			{
				UnityEngine.Debug.LogError("GameObject is null!");
				return;
			}

			if(endingAt.Value == 0) endingAt = go.transform.childCount;

			int i = 0;
			foreach(Transform child in go.transform)
			{
				if(i < startingFrom.Value || i > endingAt.Value) continue;

				child.gameObject.SetActive(enable);
				i++;
			}

			Finish();
		}
	}
}
