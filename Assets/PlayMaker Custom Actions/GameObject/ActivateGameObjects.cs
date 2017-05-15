// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("ActivateGameObjects")]
	public class ActivateGameObjects : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Array Variable to use.")]
		public FsmGameObject[] GameObjectList;

		[RequiredField]
		public FsmBool enable;
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			foreach (FsmGameObject gm in GameObjectList) {
				gm.Value.SetActive (enable.Value);
			}
	
			Finish();
		}


	}

}
