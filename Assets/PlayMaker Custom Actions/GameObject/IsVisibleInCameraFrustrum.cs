// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Original action: http://hutonggames.com/playmakerforum/index.php?topic=8417.msg53550#msg53550
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Check to see if a gameobject is visible within the camera's frustrum. Because dynamic shadows break isVisible, it's better to use this check.")]
	public class IsVisibleInCameraFrustrum : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		[Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;
		
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The Camera to test with. leave to none or empty to use the main")]
		public FsmOwnerDefault camera;
		
		
		[Tooltip("Event to send if the GameObject is visible.")]
		public FsmEvent trueEvent;
		
		[Tooltip("Event to send if the GameObject is NOT visible.")]
		public FsmEvent falseEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;
		
		public bool everyFrame;
		
		GameObject go;
		Camera _cam = null;
		bool isVisible;
#if UNITY_2017_3_OR_NEWER
		private Plane[] planes = new Plane[6];
#else
		private Plane[] planes;
#endif
		
		
		public override void Reset ()
		{
			gameObject = null;
			camera = new FsmOwnerDefault();
			camera.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			camera.GameObject = new FsmGameObject(){UseVariable=true};
			
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
			everyFrame = false;
		}
		
		public override void OnEnter ()
		{
			DoIsVisible ();
			
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			DoIsVisible ();
		}
		
		void DoIsVisible ()
		{
		
			go = Fsm.GetOwnerDefaultTarget (gameObject);

			if (go == null || go.GetComponent<Renderer>() == null) {
				return;
			}
			
		
			
			var goCam = Fsm.GetOwnerDefaultTarget (camera);
			
			if (goCam != null)
			{
				_cam = goCam.GetComponent<Camera>();
				
			}
			
			if (_cam==null)
			{
				_cam = Camera.main;
			}
			
			isVisible = IsVisibleFrom (go.GetComponent<Renderer>(), _cam);

			storeResult.Value = isVisible;
			
			Fsm.Event (isVisible ? trueEvent : falseEvent);
		}
		
		public bool IsVisibleFrom (Renderer renderer, Camera camera)
		{
#if UNITY_2017_3_OR_NEWER
			

			GeometryUtility.CalculateFrustumPlanes(camera, planes);
			#else
			planes = GeometryUtility.CalculateFrustumPlanes (camera);
#endif
			
			return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
		}
		
		



	}
}