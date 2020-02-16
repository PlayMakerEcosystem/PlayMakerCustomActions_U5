// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enables/Disables all children from the specified parent.")]
	public class EnableChildren : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to change the children off of.")]
		public FsmOwnerDefault parent;

		public bool enable;

		public override void Reset()
		{
			parent = null;
			enable = true;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(parent);

			if(go != null)
			{
				foreach(Transform child in go.transform)
				{
					child.gameObject.SetActive(enable);
				}
			}
			Finish();
		}

	}
}
