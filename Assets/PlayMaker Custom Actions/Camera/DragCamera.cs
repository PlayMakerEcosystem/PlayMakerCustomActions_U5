// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10163

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Drag Camera with mouse or touch.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10163")]
    public class DragCamera : FsmStateAction
	{

		[ActionSection("Setup")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The main camera (should have a Camera component).")]
		[Title("Camera")]
		public FsmGameObject cameraObj;

		[Tooltip("Movement speed.")]
		[RequiredField]
		public FsmFloat speed;
		[Tooltip("How smooth it should be. Highest number = Higher smoothness.")]
		[RequiredField]
		[TitleAttribute("Smoothness")]
		public FsmInt smoothControl;

		[ActionSection("Options")]
		[Tooltip("Use touch and not mouse")]
		public bool touchInput;
		[Tooltip("Only use Z axis")]
		[TitleAttribute("Z Axis Only")]
		public bool zoomOnly;

		[ActionSection("Mouse Input Options")]
		[Tooltip("Use a key. None = Off")]
		public KeyCode key;
		public enum ClickVariable {Left, Right, Middle};
		public ClickVariable mouseButton;

		[ActionSection("")]
		public FsmBool cameraMoving;
		[Tooltip("Disable Action")]
		public FsmBool disable;
		[TitleAttribute("Disable and Finish Action")]
		public FsmBool disableExit;

		bool active;
		Vector3 swipePos;
		Vector3 dragPos;
		int mouseInt;
		private FsmBool storeResult;
		bool useKey;

		public override void Reset()
		{
			cameraObj = null;
			speed = 1.2f;
			smoothControl = 25;
			active = false;
			touchInput = true;
			disable = false;
			disableExit = false;
			cameraMoving = false;
			zoomOnly = false;
			mouseInt= 0;
			key = KeyCode.None;
			storeResult = false;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnEnter()
		{

			setKey();

			switch (mouseButton) {
			case ClickVariable.Left:
				mouseInt = 0;
				break;
			case ClickVariable.Right:
				mouseInt = 1;
				break;
			case ClickVariable.Middle:
				mouseInt = 2;
				break;
			}

		}

		public override void OnLateUpdate()
		{

			if (useKey){

			storeResult.Value = Input.GetKey(key);

			}

			if (!disable.Value & !disableExit.Value){

				if (!touchInput) {
					MouseControl();
				}
				else {
					TouchControl();
				}
			}


			if (disableExit.Value)
			{
				Finish();
			}

		}

		void MouseControl()
		{

			if (useKey){

				if (Input.GetMouseButtonDown (mouseInt) && active==false && storeResult.Value == true) {
				
				swipePos = Input.mousePosition;
				active = true; 
				cameraMoving.Value=false; 
			}

				if (Input.GetMouseButtonUp (mouseInt) || storeResult.Value == false) {
					active = false; 
					cameraMoving.Value=false; 
					dragPos = swipePos;
				}

				dragCameraAction();
			}

			if (!useKey){

				if (Input.GetMouseButtonDown (mouseInt) && active==false) {
					
					swipePos = Input.mousePosition;
					active = true; 
					cameraMoving.Value=false; 
				}
			


			if (Input.GetMouseButtonUp (mouseInt)) {
				active = false; 
				dragPos = swipePos;
				cameraMoving.Value=false; 
			}

			dragCameraAction();
			
			}

		}


		void TouchControl()
		{
			
			if (Input.touchCount > 0 && active==false) {

				swipePos = Input.GetTouch(0).position;
				active = true; 
				cameraMoving.Value=false; 
			}

			if (Input.touchCount == 0) {
				active = false; 
				dragPos = swipePos;
				cameraMoving.Value=false; 
			}

			dragCameraAction();
		}

		void dragCameraAction()
		{

			if (active== true && zoomOnly == false) {

				if (touchInput) {

				dragPos = Input.GetTouch(0).position;

				}

				else {

				dragPos = Input.mousePosition;
				
				}
				
				if(swipePos.x < dragPos.x - Screen.width/smoothControl.Value){
					cameraMoving.Value=true;
					cameraObj.Value.transform.position -= new Vector3((dragPos.x - swipePos.x)/(Screen.width/speed.Value),0,0);
				}
				
				if(swipePos.x > dragPos.x + Screen.width/smoothControl.Value){
					cameraMoving.Value=true;
					cameraObj.Value.transform.position += new Vector3((swipePos.x - dragPos.x)/(Screen.width/speed.Value),0,0);
				}
				
				if(swipePos.y > dragPos.y + Screen.width/smoothControl.Value){
					cameraMoving.Value=true;
					cameraObj.Value.transform.position += new Vector3(0,(swipePos.y - dragPos.y)/(Screen.width/speed.Value),0);
				}
				
				if(swipePos.y < dragPos.y - Screen.width/smoothControl.Value){
					cameraMoving.Value=true;
					cameraObj.Value.transform.position -= new Vector3(0,(dragPos.y - swipePos.y)/(Screen.width/speed.Value),0);
				}

			}

			else{

			if(swipePos.y > dragPos.y + Screen.width/smoothControl.Value){
					cameraMoving.Value=true;
					cameraObj.Value.transform.position += new Vector3(0,0,(swipePos.y - dragPos.y)/(Screen.width/speed.Value));
			}
			
			if(swipePos.y < dragPos.y - Screen.width/smoothControl.Value){
					cameraMoving.Value=true;
					cameraObj.Value.transform.position -= new Vector3(0,0,(dragPos.y - swipePos.y)/(Screen.width/speed.Value));
				
				}
			}
		}

		void setKey()
		{

		useKey = true;
		
		switch (key) {
		case KeyCode.None:
			useKey = false;
			return;
		}
			return;
		}




	}
}
