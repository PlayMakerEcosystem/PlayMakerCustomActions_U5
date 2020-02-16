// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=9938


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Pinch zoom the main camera (Touch zoom)")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9938")]
    public class CameraPinchZoom : FsmStateAction
	{


		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Use current main camera. If false use storeGameObject to change the main camera")]
		public FsmBool useMainCamera;
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
		[Title("Camera")]
		public FsmGameObject gameObject;

		[ActionSection("Camera Zoom speed")]
		[Tooltip("Perspective camera zoom speed")]
		public FsmFloat perspectiveZoomSpeed;
		[Tooltip("Orthographic camera zoom speed")]
		public FsmFloat orthoZoomSpeed;

		[ActionSection("Camera FOV/Size clamp")]
		[RequiredField]
		[Tooltip("Minimum Value. Cannot go below that number")]
		public FsmFloat minSizeClamp;
		[RequiredField]
		[Tooltip("Maximum Value. Cannot go above that number")]
		public FsmFloat maxSizeClamp;

		[ActionSection("Events")]
		[Tooltip("Disable action and send event")]
		public FsmBool disableAction;
		public FsmEvent sendEvent;


		public override void Reset()
		{

			perspectiveZoomSpeed = 0.5f;
			orthoZoomSpeed = 0.5f;
			minSizeClamp = 0.1f;
			maxSizeClamp = 179.9f;
			disableAction  = false;
			useMainCamera = true;
			gameObject = null;
		}
	
		public override void OnEnter ()
		{
			if (useMainCamera.Value == false){

				if (gameObject.Value != null)
			{
				if (Camera.main != null)
				{
					Camera.main.gameObject.tag = "Untagged";
				}
				
					gameObject.Value.tag = "MainCamera";
			}

			}
		}

	
		public override void OnUpdate()
		{
			if (Input.touchCount == 2)
			{
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
				

				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				if (Camera.main.orthographic)
				{

					Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed.Value;

					Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSizeClamp.Value, maxSizeClamp.Value);
				}
				else
				{

					Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed.Value;

					Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minSizeClamp.Value, maxSizeClamp.Value);
				}
			}

			if (disableAction.Value == true)
			{
				Fsm.Event(sendEvent);
			}
		}

	}
}
