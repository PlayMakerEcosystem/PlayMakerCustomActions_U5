// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Original Source http://hutonggames.com/playmakerforum/index.php?topic=10493.0
// Keywords: Fighter 3d Camera

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Camera for a 3D fighting style game (or anything else you want)")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10495.0")]
	public class Fighter3dCamera : FsmStateAction
	{
		[ActionSection("Camera")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
		public FsmGameObject gameObject;

		[RequiredField]
		[ActionSection("Character")]
		[UIHint(UIHint.FsmGameObject)]
		[TitleAttribute("Character A")]
		public FsmGameObject charA;
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[TitleAttribute("Character B")]
		public FsmGameObject charB;

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

		[ActionSection("Direction")]
		[UIHint(UIHint.FsmFloat)]
		[HasFloatSlider(0.01f,20f)]
		[TitleAttribute("Direction distance")]
		public FsmFloat upDistance;
		[Tooltip("Camera Axis Direction - Default is Up")]
		[TitleAttribute("Axis Vector direction")]
		public Vector3Direction vectordirection;
		[Title("or Input Vector3 Direction (other)")]
		public FsmVector3 objDirection;

		[ActionSection("Camera Update Function")]
		public updateType updateTypeSelect;
		public enum updateType
		{
			Update,
			LateUpdate
		};

		[UIHint(UIHint.FsmBool)]
		public FsmBool everyFrame;

		Vector3 middle;
		Vector3 betweenVector;
		Vector3 perpendicular;
		Vector3 targetCamPosition;
		Vector3 direction;
		float t;

		private Transform objTransform;

		public enum LerpInterpolationType {Off,Linear,Quadratic,EaseIn,EaseOut,Smoothstep,Smootherstep,DeltaTime,SimpleSine,DoubleSine,DoubleByHalfSine};
		public enum Vector3Direction {up,down,left,right,forward,back,other};

		private FsmBool positionObjectbool;
		private FsmBool animationCurvebool;
		private FsmBool lerpOn;

		public override void Reset()
		{
			gameObject = null;
			distance = 1f;
			upDistance = 1f;
			snappyness = 5f;
			charA = null;
			charB = null;
			updateTypeSelect = updateType.LateUpdate;
			everyFrame = true;
			direction= new Vector3(0,1,0);
			interpolation = LerpInterpolationType.Off;
			sensitivity=30;
			positionObjectbool = false;
			lerpOn = true;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnEnter()
		{
			objTransform = gameObject.Value.GetComponent(typeof(Transform)) as Transform;


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

			DoFighter3DCamera();


			if (updateTypeSelect == updateType.Update)
				Do3DCamera();
		}

		public override void OnLateUpdate()
		{
			if (updateTypeSelect != updateType.Update)
				Do3DCamera();
		
		}


		 void DoFighter3DCamera()
		{

		direction = Getvectordirection(direction,vectordirection);

			if (positionObjectbool.Value == true){
				
				direction = objDirection.Value;
			}

		t = snappyness.Value/sensitivity.Value;
		t = GetInterpolation(t,interpolation);

		middle = (charA.Value.transform.position + charB.Value.transform.position) / 2f;
			
		betweenVector = charB.Value.transform.position - charA.Value.transform.position;
		perpendicular = Vector3.Cross(betweenVector, direction);
			
		targetCamPosition = middle + perpendicular * distance.Value + direction * upDistance.Value;
		

		return;

		}

		void Do3DCamera()
		
		{


			if (lerpOn.Value == false){

		objTransform.transform.position = Vector3.Lerp(objTransform.position, targetCamPosition, (Time.deltaTime * snappyness.Value));

			}

			if (lerpOn.Value == true){

				objTransform.transform.position = Vector3.Lerp(objTransform.position, targetCamPosition, t);
				
			}




		objTransform.LookAt(middle);


		return;
		
		}


		Vector3 Getvectordirection(Vector3 direction,Vector3Direction type)
		{
			switch(type)
			{
			case Vector3Direction.up:
				direction= new Vector3(0,1,0);
				positionObjectbool = false;
				return direction;
			case Vector3Direction.right:
				direction= new Vector3(1,0,0);
				positionObjectbool = false;
				return direction;
			case Vector3Direction.left:
				direction= new Vector3(-1,0,0);
				positionObjectbool = false;
				return direction;
			case Vector3Direction.forward:
				direction= new Vector3(0,0,1);
				positionObjectbool = false;
				return direction;
			case Vector3Direction.down:
				direction= new Vector3(0,-1,0);
				positionObjectbool = false;
				return direction;
			case Vector3Direction.back:
				direction= new Vector3(0,0,-1);
				positionObjectbool = false;
				return direction;
			case Vector3Direction.other:
				positionObjectbool = true;
				return direction;
				
			}
			
			return direction;
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
			
			case LerpInterpolationType.Off:
				lerpOn.Value = false;
				return t;
				
			}
			
			return t;
		}


	}
}
