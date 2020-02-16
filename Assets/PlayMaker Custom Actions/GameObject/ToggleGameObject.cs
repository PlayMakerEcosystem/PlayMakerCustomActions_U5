// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enables a GameObject if it's disabled and vice versa.")]
	public class ToggleGameObject : FsmStateAction
	{
		public enum ActiveType
		{
			ActiveInHirarchy,
			ActiveSelf
		}

		[RequiredField]
		[Tooltip("The GameObject to toggle.")]
		public FsmGameObject gameObject;

		[Tooltip("What active state should be checked.\n'Active In Hirarchy' = If the GameObject and all " +
				 "of it's parents are active (a.k.a. if it's visible in the scene);\n" +
				 "'ActiveSelf' = If the GameObject is active independent of its parents.")]
		public ActiveType activeType;

		[Tooltip("Wheter to run this action every frame or only once.")]
		public FsmBool everyFrame;

		private GameObject go;

		public override void Reset()
		{
			gameObject = new FsmGameObject() { UseVariable = true };
			activeType = ActiveType.ActiveSelf;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoToggleGameObject();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			DoToggleGameObject();
		}

		private void DoToggleGameObject()
		{
			go = gameObject.Value;

			switch(activeType)
			{
				case ActiveType.ActiveInHirarchy:
					go.SetActive(!go.activeInHierarchy);
					break;
				case ActiveType.ActiveSelf:
					go.SetActive(!go.activeSelf);
					break;
			}
		}
	}
}
