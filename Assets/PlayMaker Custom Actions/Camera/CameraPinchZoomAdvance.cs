// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=9938

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Advance pinch zoom the main camera (Touch zoom)")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9938")]
    public class CameraPinchZoomAdvance : FsmStateAction
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

		[ActionSection("Set Bool")]
		[Tooltip("Disable action and send event")]
		public FsmBool pinchZoomActive;

		[ActionSection("Send Events if above bool change")]
		[Tooltip("Activate Send Events")]
		public FsmBool activate;
		[Tooltip("Where to send the event. ")]
		public FsmEventTarget eventTarget;

		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		[Title("Send Event to")]
		public FsmEvent sendExternalEvent;
		
		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		[ActionSection("Store Finger Id (x)")]
		[Tooltip("Store Finger Id's when pinch is active. Index = (x)")]
		public FsmInt[] fingerId;
		[Tooltip("Total Finger Id when pinch is active")]
		public FsmInt fingerIdCount;
		[Tooltip("Activate 2 finger rule which means 2 finger have to touch screen at once. Not one after the other.")]
		[Title("Activate (x) finger rule")]
		public FsmBool activateIfFingerCount;
	
		private FsmInt fingerIdCountRule;
		private int tCount;
		private FsmBool temp;
		private FsmBool temp2;
		private FsmInt tempfingercount;
		private int tInt = 2;

		public override void Reset()
		{

			perspectiveZoomSpeed = 0.5f;
			orthoZoomSpeed = 0.5f;
			minSizeClamp = 0.1f;
			maxSizeClamp = 179.9f;
			fingerId = new FsmInt[2];
			fingerIdCountRule = 2; 
			useMainCamera = true;
			pinchZoomActive = false;
			activateIfFingerCount = true;
			activate = false;
			gameObject = null;
			eventTarget = null;
			delay = null;
			temp = false;
			temp2= false;
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

			temp = false;
			temp2 = false;
		}

	
		public override void OnUpdate()
		{
			tCount = Input.touchCount;

			if (Input.touchCount >= 1){

				for(int i = 0; i<tCount;i++){
					fingerId[i].Value = i+1;
				}


				if (activateIfFingerCount.Value == true) {
			
				if (temp2.Value == false) {
				fingerIdCount.Value = Input.touchCount;
				tInt =  fingerId.Length;
				temp2 = true;

				}
					fingerIdCountRule.Value = fingerId.Length;

				if (fingerIdCount.Value == fingerIdCountRule.Value){

					RunPinch();
				}
			}

			
			else if (activateIfFingerCount.Value == false) {
				fingerIdCount.Value = Input.touchCount;
				RunPinch();
			}

			}

			if (Input.touchCount == 0){

				
				for(int i = 0; i<fingerId.Length;i++){
					if(!fingerId[i].IsNone || !fingerId[i].Value.Equals(""))
					fingerId[i].Value = 0;
				}

				fingerIdCount.Value = 0;
				pinchZoomActive.Value = false;
				temp = false;
				temp2 = false;
			}
		}



		public void RunPinch() {
			if (Input.touchCount == tInt)
			{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			pinchZoomActive.Value = true;
			
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
			
			if (activate.Value == true){
				
				if (pinchZoomActive.Value == true)
				{
					
					if (temp.Value == false){
						
						if (delay.Value < 0.001f)
						{
							Fsm.Event(eventTarget, sendExternalEvent);
							temp = true;
						}
						else
						{
							Fsm.DelayedEvent(eventTarget, sendExternalEvent, delay.Value);
						}
					}
				}
			}

			return;
		}
	}
}
