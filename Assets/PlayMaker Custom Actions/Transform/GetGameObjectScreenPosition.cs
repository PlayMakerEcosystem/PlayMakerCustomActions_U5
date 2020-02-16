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
	[Tooltip("Get the actual position on the screen of the given GameObject.")]
	public class GetGameObjectScreenPosition : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("<Insert variable description>")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The camera that sees the GameObject. If not specified, uses the MainCamera.")]
		public Camera camera;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the position as a Vector2 variable.")]
		public FsmVector2 v2Result;

		[UIHint(UIHint.Variable)]
		[Tooltip("Or store the position as a Vector3 variable (z = 0, but you wont need to" +
				" convert it to use it in other actions.")]
		public FsmVector3 v3Result;

		[Tooltip("If true, X/Y coordinates are considered normalized (0-1), otherwise they are expected to be in pixels")]
		public FsmBool normalize;

		private GameObject go;

		public override void Reset()
		{
			//resets 'everyFrame' and 'updateType'
			base.Reset();

			gameObject = null;
			camera = null;
			go = null;
			normalize = false;
		}

		public override void OnEnter()
		{
			DoGetGameObjectScreenPosition();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoGetGameObjectScreenPosition();
		}

		private void DoGetGameObjectScreenPosition()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if(!go)
			{
				LogError("GameObject in " + Owner.name + " (" + Fsm.Name + ") is null!");
				return;
			}

			if(!camera) camera = Camera.main;

			Vector3 tmpV3 = camera.WorldToScreenPoint(go.transform.position);
			if(normalize.Value) tmpV3 = tmpV3.normalized;
			v3Result.Value = new Vector3(tmpV3.x, tmpV3.y, 0f);
			v2Result.Value = new Vector2(tmpV3.x, tmpV3.y);
		}
	}
}
