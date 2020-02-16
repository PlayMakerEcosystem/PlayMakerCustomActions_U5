// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10495.0
// Keywords: Fighter 2d Camera / Camera smooth follow multiple gameobject

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("2D Camera for a fighting style game (or anything else you want) - Camera focuses on every GameObject on the screen. Unlimited players - must all have same Tag OR set targets array.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10495.0")]
	public class FighterCameraAdvance2d : FsmStateAction
	{
		[ActionSection("Camera")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
		public FsmGameObject gameObject;
		[Tooltip("The camera size.")]
		[TitleAttribute("Camera Min size.")]
		public FsmFloat orthoSizeMin;
		[TitleAttribute("Camera Max size.")]
		public FsmFloat orthoSizeMax;

		[ActionSection("Camera Buffer")]
		public FsmFloat cameraBufferX;
		public FsmFloat cameraBufferY;


		[ActionSection("Character")]
		public FsmGameObject[] targets;
		[UIHint(UIHint.Tag)]
		[TitleAttribute("or by Tag")]
		public FsmString tag;

		[ActionSection("Options")]
		[UIHint(UIHint.FsmFloat)]
		[HasFloatSlider(0.01f,20f)]
		public FsmFloat distance;
		[UIHint(UIHint.FsmFloat)]
		[HasFloatSlider(0.01f,20f)]
		public FsmFloat snappyness;
	
		
		[ActionSection("Interpolation Setup")]
		[Tooltip("Select interpolation type.")]
		public LerpInterpolationType interpolation;
		[UIHint(UIHint.FsmFloat)]
		[HasFloatSlider(2f,80f)]
		public FsmFloat sensitivity;

		[ActionSection("Camera Update Function")]
		public updateType updateTypeSelect;
		public enum updateType
		{
			Update,
			LateUpdate
		};

		[UIHint(UIHint.FsmBool)]
		public FsmBool everyFrame;

		private Transform objTransform;

		private float minX;
		private float maxX;
		private float minY;
		private float maxY;
		private Camera _camera;

		private GameObject[] players;

		public enum LerpInterpolationType {Linear,Quadratic,EaseIn,EaseOut,Smoothstep,Smootherstep,DeltaTime,SimpleSine,DoubleSine,DoubleByHalfSine};

		private FsmBool lerpOn;
		float t;
		Vector3 finalLookAt;
		Vector3 cameraCenter; 
		float orthoSize;
		Vector3 pos;
		Vector3 originalPosFirstPass;
		Vector3 tempPlayer;
		Vector3 finalCameraCenter;

		public override void Reset()
		{

			gameObject = null;
			distance = 1f;
			snappyness = 5f;
			updateTypeSelect = updateType.LateUpdate;
			interpolation = LerpInterpolationType.Linear;
			sensitivity=30;
			lerpOn = true;
			everyFrame = true;
			orthoSizeMin = 1f;
			orthoSizeMax= 100f;
			cameraBufferX=15f; 
			cameraBufferY=15f;

		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateTypeSelect == updateType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter()
		{
			var go = gameObject.Value;
			if (go == null) return;

			_camera = go.GetComponent<Camera>();

			objTransform = gameObject.Value.GetComponent(typeof(Transform)) as Transform;
			originalPosFirstPass.z = objTransform.position.z;

			if (_camera.orthographic == false){
				Debug.Log ("<color=#6B8E23ff> You need a orthographic camera.Please review!</color>",this.Owner);
					}


			OnUpdate();

			if (everyFrame.Value){
				OnUpdate();
			}

			else {
				Finish();
			}
		}

		public override void OnUpdate()
		{

			CalculateBounds();


			if (updateTypeSelect == updateType.Update)
				CalculateCameraPosAndSize();
		}

		public override void OnLateUpdate()
		{
			if (updateTypeSelect != updateType.Update)
				CalculateCameraPosAndSize();
		
		}


		void CalculateBounds() { 

			minX = Mathf.Infinity; 
			maxX = -Mathf.Infinity; 
			minY = Mathf.Infinity; 
			maxY = -Mathf.Infinity;


			if(targets.Length <= 0) { 
				players = GameObject.FindGameObjectsWithTag(tag.Value);


				for (int i = 0; i < players.Length; i++){

					if ( players[i] != null){
					tempPlayer = players[i].transform.position;
					
					
					if (tempPlayer.x < minX)
						minX = tempPlayer.x;
					
					if (tempPlayer.x > maxX)
						maxX = tempPlayer.x;
					
					
					if (tempPlayer.y < minY)
						minY = tempPlayer.y;
					
					if (tempPlayer.y > maxY)
						maxY = tempPlayer.y;
					}


				}
			}

			else {


				for (int i = 0; i < targets.Length; i++){

					if ( targets[i].Value != null){
					tempPlayer = targets[i].Value.transform.position;
				
				
				if (tempPlayer.x < minX)
					minX = tempPlayer.x;
				
				if (tempPlayer.x > maxX)
					maxX = tempPlayer.x;
				
				
				if (tempPlayer.y < minY)
					minY = tempPlayer.y;
				
				if (tempPlayer.y > maxY)
					maxY = tempPlayer.y;

			
				}

				
				}

			}


			return;
		}

		void CalculateCameraPosAndSize() { 

			t = snappyness.Value/sensitivity.Value;
			t = GetInterpolation(t,interpolation);

			cameraCenter = objTransform.transform.position;

			cameraCenter.z = originalPosFirstPass.z;

			if(targets.Length <= 0) { 


				for (int i = 0; i < players.Length; i++){
							if ( players[i] != null){
						cameraCenter += players[i].transform.position;
				}
			}
							

				finalCameraCenter = cameraCenter / players.Length;

			}

			else {


				for (int i = 0; i < targets.Length; i++){
					if ( targets[i].Value != null){
					
						cameraCenter += targets[i].Value.transform.position;
				}
				}

				finalCameraCenter = cameraCenter / targets.Length;

			}



			pos = finalCameraCenter;

			
			if (lerpOn.Value == true){
				objTransform.transform.position = new Vector3(objTransform.position.x,objTransform.position.y,originalPosFirstPass.z);
				pos = new Vector3(pos.x,pos.y,originalPosFirstPass.z);
				objTransform.transform.position = Vector3.Lerp(objTransform.position, pos, t);
				finalLookAt = Vector3.Lerp (finalLookAt, finalCameraCenter, t);

			}


			gameObject.Value.transform.LookAt(finalLookAt);

			float sizeX = maxX - minX + cameraBufferX.Value; 
			float sizeY = maxY - minY + cameraBufferY.Value;
			
			orthoSize = (sizeX > sizeY ? sizeX : sizeY);
			orthoSize = orthoSize * 0.5f;

			if (orthoSize > orthoSizeMax.Value) orthoSize = orthoSizeMax.Value;
			if (orthoSize < orthoSizeMin.Value) orthoSize = orthoSizeMin.Value;

		

			_camera.orthographicSize =  orthoSize;
		
			return;
		}

		float GetInterpolation(float t,LerpInterpolationType type)
		{
			switch(type)
			{
				
			case LerpInterpolationType.Quadratic:
				lerpOn.Value = true;
				return Time.timeSinceLevelLoad*snappyness.Value;
			case LerpInterpolationType.EaseIn:
				lerpOn.Value = true;
				return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.EaseOut:
				lerpOn.Value = true;
				return Mathf.Sin(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.Smoothstep:
				lerpOn.Value = true;
				return t*t * (3f - 2f*t);
			case LerpInterpolationType.Smootherstep:
				lerpOn.Value = true;
				return t*t*t * (t * (6f*t - 15f) + 10f);
			case LerpInterpolationType.DeltaTime:
				lerpOn.Value = true;
				return Time.deltaTime*t;
			case LerpInterpolationType.SimpleSine:
				lerpOn.Value = true;
				return t * Mathf.Sin(Time.timeSinceLevelLoad);
			case LerpInterpolationType.DoubleSine:
				lerpOn.Value = true;
				return t * Mathf.Sin(Time.timeSinceLevelLoad/distance.Value);
			case LerpInterpolationType.DoubleByHalfSine:
				lerpOn.Value = true;
				return t * (1.5f * Mathf.Sin(Time.timeSinceLevelLoad*distance.Value));
				
			}
			
			return t;
		}

	}
}

