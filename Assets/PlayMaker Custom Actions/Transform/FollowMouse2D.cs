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
	[Tooltip("Moves a GameObject under the current mouse position in 2D space with optional smoothing to simulate a dragging-delay. (Currently) only supports orthographic cameras.")]
	public class FollowMouse2D : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject with the camera attached, that you want to capture the mouse position in.")]
		public FsmGameObject camera;

		[Tooltip("Move a GameObject with the mouse poisition. Can be None/null if you only want to store the current independent mouse position.")]
		public FsmOwnerDefault attachedGameObject;

		[HasFloatSlider(0.0f, 1.0f)]
		[Tooltip("How much delay is applied to the dragged GameObject. 1 = Object is directly under the mouse; the closer you get to 0, the more it lags behind (on 0 it stands still).")]
		public FsmFloat smoothing;

		[Title("Offset From Mouse Center")]
		[Tooltip("Determines an offset for the dragged GameObject from the current mouse position if it's not supposed to be directly under it.")]
		public FsmVector2 offset;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the current mouse position.")]
		public FsmVector3 storeMousePosition;

		[Tooltip("Select this if you don't want the object to move off screen with the mouse.")]
		public FsmBool keepOnScreen;

		[Tooltip("This determines how close to the edge of the screen the object can get IF Keep On Screen is used.")]
		public FsmFloat screenOffset;

		[Tooltip("Toggle this to let go of the GameObject while dragging.")]
		public FsmBool stopDragging;

		[Tooltip("Wheter to move the GameObject back to the position before dragging")]
		public FsmBool resetPosition;


		private GameObject go;
		private Vector3 initGOPos;
		private Vector3 camPos;
		private Camera currCam;

		public override void Reset()
		{
			if(GameObject.FindGameObjectWithTag("MainCamera"))
			{
				camera = GameObject.FindGameObjectWithTag("MainCamera");
			} else
			{
				camera = null;
			}
			smoothing = .3f;
			attachedGameObject = null;
			offset = Vector2.zero;
			storeMousePosition = null;
			keepOnScreen = true;
			screenOffset = .25f;
			stopDragging = new FsmBool() { UseVariable = true };
			resetPosition = new FsmBool() { UseVariable = true };
			everyFrame = true;
			updateType = FrameUpdateSelector.OnLateUpdate;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(attachedGameObject);

			if(go)
			{
				initGOPos = go.transform.position;
			}

			DoFollowMouse2D();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoFollowMouse2D();
		}

		private void DoFollowMouse2D()
		{
			go = Fsm.GetOwnerDefaultTarget(attachedGameObject);
			currCam = camera.Value.GetComponent<Camera>();

			storeMousePosition.Value = currCam.ScreenToWorldPoint(Input.mousePosition);
			camPos = storeMousePosition.Value;
			camPos.x += offset.Value.x;
			camPos.y += offset.Value.y;
			camPos.z = go.transform.position.z;

			//keeps the object inside the screen
			if(keepOnScreen.Value)
			{
				float wScreenWidth = currCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
				float wScreenHeight = currCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

				float xleft = -wScreenWidth + screenOffset.Value;
				float xright = wScreenWidth - screenOffset.Value;
				float ytop = -wScreenHeight + screenOffset.Value;
				float ybottom = wScreenHeight - screenOffset.Value;

				if(camPos.x < xleft) camPos.x = xleft;
				if(camPos.x > xright) camPos.x = xright;
				if(camPos.y < ytop) camPos.y = ytop;
				if(camPos.y > ybottom) camPos.y = ybottom;
			}
			storeMousePosition.Value = camPos;


			//skip if GameObject got destroyed or 'Stop Dragging' is enabled
			if(!go || stopDragging.Value)
			{
				return;
			}

			if(!resetPosition.Value)
			{
				go.transform.position = Vector3.Lerp(go.transform.position, storeMousePosition.Value, smoothing.Value);
			} else
			{
				go.transform.position = initGOPos;
			}

		}
	}
}
