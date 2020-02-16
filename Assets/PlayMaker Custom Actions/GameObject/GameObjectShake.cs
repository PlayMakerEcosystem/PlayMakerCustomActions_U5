// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//v1.3

// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=9928


using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Shake an object (GameObject: Camera, Canvas, Cube, etc)")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9928")]
	public class GameObjectShake : FsmStateAction
	{
		public enum LerpInterpolationType {Off,Linear,Quadratic,EaseIn,EaseOut,Smoothstep,Smootherstep,DeltaTime,SimpleSine,DoubleSine,DoubleByHalfSine, Curve};
		
		[ActionSection("Set GameObject")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("GameObject to shake")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Setup")]
		[Tooltip("Amount of Time for shake")]
		public FsmFloat setTime;
		[Tooltip("Shake strength. If interpolation is off, use low numbers such as 0,02 for subtle shake of camera for example.")]
		public FsmFloat shakeAmount;
		
		[ActionSection("Interpolation Setup")]
		[Tooltip("Select interpolation type. Off means interpolation + shake speed are disabled")]
		public LerpInterpolationType interpolation;
		[Tooltip("If interpolation is on, recommended shake speed above 1f (depends on selection!)")]
		public FsmFloat shakeSpeed;
		[Tooltip("Only works if interpolation is to curve!)")]
		public FsmAnimationCurve lerpCurve;
		
		[ActionSection("Rotation Setup")]
		[Tooltip("Set rotation strength. 0 = Off/disabled")]
		public FsmFloat rotationAmount;
		
		[ActionSection("Events")]
		[Tooltip("Leave state when finished shaking")]
		public FsmBool exitOnFinish;
		public FsmEvent exit;
		
		[ActionSection("")]
		[UIHint(UIHint.Description)]
		[Tooltip("Repeat this action every frame. To allow a loop force quit, set Fsm Bool (true = active) and change it to false in game")]
		public FsmBool loop;
		
		
		Vector3 originalPos;
		Vector3 newPos;
		Quaternion newPosRot;

		private bool lerpOn = true;
		private bool animationCurvebool = false;
		private Transform objTransform;
		private GameObject gameObject2;
		private float shakeTime;
		private Quaternion OriginalRot;
		private float lerpFactor = 0.5f;
		private bool rotation;
		private float rotationIntent;
		private float rotationDecay;
		private bool isCamera; 
		
		float t;
		
		public override void Reset ()
		{
			gameObject = null;
			loop = false;
			rotation = true;
			exitOnFinish = false;
			shakeTime = 0f;
			setTime = 1f;
			shakeAmount = 0.02f;
			lerpFactor = 1.0f;
			shakeSpeed = 1f;
			rotationAmount = 0.3f;
			animationCurvebool= false;
			isCamera = false;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (isCamera)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter ()
		{
			gameObject2 = Fsm.GetOwnerDefaultTarget(gameObject);
			objTransform = gameObject2.GetComponent(typeof(Transform)) as Transform;

			if(gameObject2.GetComponent<Camera>() != null) {isCamera = true;}

			originalPos = objTransform.localPosition;
			OriginalRot = objTransform.localRotation;
			shakeTime = setTime.Value;
			rotationIntent = rotationAmount.Value;
			
			t = shakeSpeed.Value/30;
			
			t = GetInterpolation(Mathf.Abs(t),interpolation);
			
			
			if (loop.Value == true)
			{
				lerpFactor = 0.0f;
				exitOnFinish = false;
			}
			
			else {
				lerpFactor = 1.0f;
			}
			
			if (rotationAmount.Value >0 || rotationAmount.Value <0)
			{
				
				rotation = true;
			}
			
			DoObjShake();
			
		}
		
		
		public override void OnUpdate()
		{
			if (isCamera == false){doSetup();}
		}


		public override void OnLateUpdate()
		{
			if (isCamera == true){doSetup();}
		}
		
		
		void doSetup()
		{
			if (loop.Value == true)
			{
				DoObjShake();
			}
			
			
			else 	{
				
				if (shakeTime <= 0f)
				{
					
					objTransform.localPosition = originalPos;
					objTransform.localRotation = OriginalRot;
					newPos = objTransform.localPosition;
					
					if (exitOnFinish.Value == true){
						
						Fsm.Event(exit);
						if (shakeTime <= 0.0f) Finish();
					}
					
				}
				
				else {
					
					DoObjShake();
					
				}
			}

		}
		
		void DoObjShake()
		{
			
			if (shakeTime > 0f || loop.Value == true){
				
				
				if (lerpOn == true) {
					DoObjShakeLerp();
				}
				
				else if (lerpOn == false) {
					objTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount.Value;
					shakeTime -= Time.deltaTime * lerpFactor;
				}
				
				
				if (rotation == true) {
					DoObjShakerotation();
				}
				
				
				
			}
			
		}
		
		
		void DoObjShakerotation(){
			
			newPosRot = objTransform.localRotation;
			
			
			
			objTransform.localRotation = new Quaternion(OriginalRot.x + Random.Range(-rotationIntent, rotationIntent)*.2f,
			                                            OriginalRot.y + Random.Range(-rotationIntent, rotationIntent)*.2f,
			                                            OriginalRot.z + Random.Range(-rotationIntent, rotationIntent)*.2f,
			                                            OriginalRot.w + Random.Range(-rotationIntent, rotationIntent)*.2f);
			
			if (animationCurvebool == true){
				objTransform.localRotation = Quaternion.Lerp(newPosRot,objTransform.localRotation, lerpCurve.curve.Evaluate(shakeTime));
			}
			
			else if (lerpOn == true){
				objTransform.localRotation = Quaternion.Lerp(newPosRot,objTransform.localRotation,t);
			}
			
			rotationIntent-= Time.deltaTime*t;
			
			return;
		}
		
		
		void DoObjShakeLerp()
		{
			
			newPos = objTransform.localPosition;
			
			if (Vector3.Distance(newPos,objTransform.localPosition)<=shakeAmount.Value/30) {
				
				newPos = originalPos + Random.insideUnitSphere * shakeAmount.Value;
			}
			
			if (animationCurvebool == true){
				objTransform.localPosition = Vector3.Lerp(objTransform.localPosition, newPos , lerpCurve.curve.Evaluate(shakeTime));
			}
			
			else if (animationCurvebool == false){
				objTransform.localPosition = Vector3.Lerp(objTransform.localPosition, newPos , t);
				
			}
			
			shakeTime -= Time.deltaTime;
			
			return;
		}	
		
		
		float GetInterpolation(float t,LerpInterpolationType type)
		{
			switch(type)
			{
			case LerpInterpolationType.Quadratic:
				lerpOn = true;
				animationCurvebool = false;
				return Time.timeSinceLevelLoad*setTime.Value;
			case LerpInterpolationType.EaseIn:
				lerpOn = true;
				animationCurvebool = false;
				return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.EaseOut:
				lerpOn = true;
				animationCurvebool = false;
				return Mathf.Sin(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.Smoothstep:
				lerpOn = true;
				animationCurvebool = false;
				return t*t * (3f - 2f*t);
			case LerpInterpolationType.Smootherstep:
				animationCurvebool = false;
				lerpOn = true;
				return t*t*t * (t * (6f*t - 15f) + 10f);
			case LerpInterpolationType.DeltaTime:
				lerpOn = true;
				animationCurvebool = false;
				return Time.deltaTime*t;
			case LerpInterpolationType.SimpleSine:
				lerpOn = true;
				animationCurvebool = false;
				return t * Mathf.Sin(Time.timeSinceLevelLoad);
			case LerpInterpolationType.DoubleSine:
				lerpOn = true;
				animationCurvebool = false;
				return t * Mathf.Sin(Time.timeSinceLevelLoad/setTime.Value);
			case LerpInterpolationType.DoubleByHalfSine:
				lerpOn = true;
				animationCurvebool = false;
				return t * (1.5f * Mathf.Sin(Time.timeSinceLevelLoad*setTime.Value));
			case LerpInterpolationType.Curve:
				lerpOn = true;
				animationCurvebool = true;
				return t;
			case LerpInterpolationType.Off:
				animationCurvebool = false;
				lerpOn = false;
				return t;
			}
			
			return t;
		}
		
		
		
		public override string ErrorCheck()
		{
			if (gameObject == null)
			{
				return "Need GameObject";
			}
			
			return "";
		}
	}
}

