// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[ActionTarget(typeof(GameObject), "gameObject")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Tests if the Collider2D on a GameObject is visible. This will return true even if just a portion of the collider is visible (checks if the Collider bounds are outside the camera frustum planes).")]
	public class ColliderIsVisible : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Collider))]
		[Tooltip("The GameObject to test the collider of.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The Camera to check against the collider.")]
		public FsmGameObject camera;

		[Tooltip("Event to send if the collider is visible.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if the collider is not visible.")]
		public FsmEvent falseEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		private GameObject go = null;
		private bool result = false;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			camera = Camera.main.gameObject;
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIsVisible();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsVisible();
		}

		void DoIsVisible()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera.Value.GetComponent<Camera>());
			if(GeometryUtility.TestPlanesAABB(planes, go.GetComponent<Collider>().bounds))
			{
				result = true;
			} else
			{
				result = false;
			}
			storeResult.Value = result;
			Fsm.Event(result ? trueEvent : falseEvent);
		}
	}
}
