// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// 


using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Get Vector 3 of center of camera")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11585.0")]
	public class PositionInCameraCenter : FsmStateAction
	{
		[ActionSection("Setup")]
		[Tooltip("Select main camera")]
		public FsmBool UseMainCamera;
		[Tooltip("Or use another camera than the Main Camera")]
		public FsmGameObject otherCamera;

		[RequiredField]
		[TitleAttribute("Save Vector3")]
		public FsmVector3 Position;


		[ActionSection("Options")]
		[Tooltip("Setup Z position of object")]
		public FsmFloat zPosition;
		public FsmBool useNearClipPlane;
		public FsmBool useFarClipPlane;
		public enum UpdateType { Update, FixedUpdate, LateUpdate };
		public UpdateType updateSelect;
		

	
		[ActionSection("Other")]
		[Tooltip("Repeat every frame")]
		public FsmBool everyFrame;

		private float zTemp;


		public override void Reset()
		{
			Position = null;
			UseMainCamera = true;
			otherCamera = null;
			everyFrame = false;
			zPosition = 0f;
			updateSelect = UpdateType.Update;
		}
			
		public override void OnPreprocess()
		{
			if (updateSelect == UpdateType.FixedUpdate )
			{
				Fsm.HandleFixedUpdate = true;
			}

			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateSelect == UpdateType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter()
		{
			DoVectorToWorldPoint();

			if (!everyFrame.Value)
			{
				Finish();
			}	
		}

		public override void OnUpdate () 
		{
			if (updateSelect == UpdateType.Update) DoVectorToWorldPoint() ;
		}


		public override void OnFixedUpdate () 
		{
			if (updateSelect == UpdateType.FixedUpdate) DoVectorToWorldPoint() ;
			
		
		}
		
		public override void OnLateUpdate () 
		{
			if (updateSelect == UpdateType.LateUpdate) DoVectorToWorldPoint() ;
			
		}



		public void DoVectorToWorldPoint() 
		{

			if (useNearClipPlane.Value == true & useFarClipPlane.Value == true)
				Debug.LogWarning ("You cannot use use NearClipPlane and use FarClipPlane. Please choose one or use Z float: zPosition");



			if (UseMainCamera.Value == true){


				if (useNearClipPlane.Value == true)
					zTemp = Camera.main.nearClipPlane;
				
				if (useFarClipPlane.Value == true)
					zTemp = Camera.main.farClipPlane;

				if (useNearClipPlane.Value == false & useNearClipPlane.Value == false)

					zTemp = zPosition.Value;

				Position.Value = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, (zTemp - Camera.main.transform.position.z)));
			}

			else{

				Camera camera = otherCamera.Value.GetComponent<Camera>();

				if (useNearClipPlane.Value == true)
					zTemp = camera.nearClipPlane;
				
				if (useFarClipPlane.Value == true)
					zTemp = camera.farClipPlane;
				
				if (useNearClipPlane.Value == false & useNearClipPlane.Value == false)
					
					zTemp = zPosition.Value;

				Position.Value = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, (zTemp - camera.transform.position.z)) );
			
			
			}

			if (!everyFrame.Value)
			{
				Finish();
			}	

		}

	}
}

