// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Gets the last child of a GameObject.")]
	public class GetLastChild : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObjec to get the last child of.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("The last child of the given GameObject.")]
		public FsmGameObject result;

		[Tooltip("Wheter to run every frame or only once.")]
		public FsmBool everyFrame;

		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			result = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetLastChild();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			DoGetLastChild();
		}

		private void DoGetLastChild()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if(!go)
			{
				UnityEngine.Debug.LogError("GameObject is null!");
				return;
			}

			result.Value = go.transform.GetChild(go.transform.childCount - 1).gameObject;
		}
	}
}
