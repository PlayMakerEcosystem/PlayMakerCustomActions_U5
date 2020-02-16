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
	[ActionCategory(ActionCategory.Transform)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Resets each Transform of the given GameObjects to their inital value (Position(0, 0, 0), Rotation(0, 0, 0) and Scale(1, 1, 1)).")]
	public class ResetTransforms : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The GameObject to reset the Transform of.")]
		public FsmGameObject[] gameObjects;

		[Tooltip("Include Position.")]
		public FsmBool resetPosition;

		[Tooltip("Include Rotation.")]
		public FsmBool resetRotation;

		[Tooltip("Include Scale.")]
		public FsmBool resetScale;

		public override void Reset()
		{
			//resets 'everyFrame' and 'updateType'
			base.Reset();

			gameObjects = new FsmGameObject[0];
			resetPosition = true;
			resetRotation = true;
			resetScale = true;
		}

		public override void OnEnter()
		{
			DoResetTransforms();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoResetTransforms();
		}

		private void DoResetTransforms()
		{
			foreach(var go in gameObjects)
			{
				if(!go.Value)
				{
					LogError("GameObject in " + Owner.name + " (" + Fsm.Name + ") is null!");
					return;
				}

				Transform trans = go.Value.transform;

				if(!resetPosition.IsNone && resetPosition.Value) trans.position = new Vector3(0, 0, 0);
				if(!resetRotation.IsNone && resetRotation.Value) trans.rotation = new Quaternion(0, 0, 0, 0);
				if(!resetScale.IsNone && resetScale.Value) trans.localScale = new Vector3(0, 0, 0);
			}
		}
	}
}
