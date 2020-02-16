// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//v2

// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10193

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sine Wave/Motion (Fx) on a object (GameObject: Camera, Canvas, Cube, etc")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10193")]
    public class GameObjectMotionFx : FsmStateAction
	{
		public enum LerpInterpolationType {EaseIn,EaseOut,Smoothstep,SimpleSine,OtherSimpleSine,DoubleSine,DoubleByHalfSine, Curve};
		public enum Vector3Direction {up,down,left,right,forward,back,other};

		[ActionSection("Set GameObject")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("GameObject")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Setup")]
		[Tooltip("Set time")]
		public FsmFloat setTime;
		[Tooltip("How far the object moves")]
		public FsmFloat amplitude;
		[HasFloatSlider(0.01f,2f)]
		[Tooltip("How quickly the object moves")]
		public FsmFloat speed;

		[ActionSection("Interpolation Setup")]
		[Tooltip("Select interpolation type.")]
		public LerpInterpolationType interpolation;
		[Tooltip("Only works if interpolation is set to curve!)")]
		public FsmAnimationCurve lerpCurve;
		[ActionSection("Vector 3 Setup")]
		[Tooltip("Vector3 Direction")]
		public Vector3Direction vectordirection;
		[Title("or Input Vector3 Direction")]
		public FsmVector3 objDirection;


		[ActionSection("Set loop")]
		[UIHint(UIHint.Description)]
		[Tooltip("Repeat this action every frame. To allow a loop force quit, set Fsm Bool (true = active) and change it to false in game.")]
		public FsmBool loop;

		[ActionSection("Events")]
		[Tooltip("Leave state when finished shaking")]
		public FsmBool exitOnFinish;
		public FsmEvent exit;

		private FsmBool positionObjectbool;
		private Transform objTransform;
		private GameObject gameObject2;
		private FsmFloat shakeTime;
		Vector3 startPos;
		Vector3 direction;
		float t;
		float a;
		private FsmBool animationCurvebool = false;
		private bool mainCamera;

		public override void Reset ()
		{
			gameObject = null;
			loop = true;
			positionObjectbool = false;
			setTime = 5f;
			amplitude = 10f;
			speed = 1f;
			shakeTime = 0f;
			direction= new Vector3(0,1,0);
			lerpCurve = new FsmAnimationCurve ();
			mainCamera = false;

		}
		
		public override void OnEnter ()
		{
			gameObject2 = Fsm.GetOwnerDefaultTarget(gameObject);
			objTransform = gameObject2.GetComponent(typeof(Transform)) as Transform;
			shakeTime.Value = setTime.Value;
			startPos = objTransform.transform.position;

			a = amplitude.Value;
	
			if (Fsm.GetOwnerDefaultTarget(gameObject).tag == "MainCamera" ){
				mainCamera = true;
			}

		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
				Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnUpdate()
		{
		
			if (mainCamera){
				OnLateUpdate();

			}

			if (loop.Value == true)
			{
				DoObjWave();
			}
			
			
			else 	{
				
				if (shakeTime.Value <= 0f)
				{
					
					objTransform.transform.position = startPos;
					
					
					if (exitOnFinish.Value == true){
						
						Fsm.Event(exit);
					}
					
					else {
						Finish();
					}
					
				}
				
				else {
					
					DoObjWave();
					
				}
			}
		}


		public override void OnLateUpdate()
			{

			if (!mainCamera){
				return;
			}

				if (loop.Value == true)
				{
					DoObjWave();
				}
				
				
				else 	{
					
					if (shakeTime.Value <= 0f)
					{
						
					objTransform.transform.position = startPos;

						
						if (exitOnFinish.Value == true){
							
							Fsm.Event(exit);
						}

					else {
						Finish();
					}

				}
					
					else {
						
						DoObjWave();
						
					}
				}
		
			}

		void DoObjWave()
		{
			a = amplitude.Value;
			shakeTime.Value = setTime.Value;

			{
				direction = Getvectordirection(direction,vectordirection);

				if (positionObjectbool.Value == true){

					direction = objDirection.Value;
				}

			t = GetInterpolation(t,interpolation);


				if (animationCurvebool.Value == true){
					objTransform.transform.position = Vector3.Lerp(objTransform.localPosition,(startPos + direction * lerpCurve.curve.Evaluate(a * Mathf.Sin(Time.timeSinceLevelLoad))), Time.deltaTime);
				}

				else if (animationCurvebool.Value == false){

					objTransform.transform.position = Vector3.Lerp(objTransform.localPosition,(startPos + direction * t),Time.deltaTime* setTime.Value);
				
				}

				shakeTime.Value -= Time.deltaTime;

			}
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
		
			case LerpInterpolationType.EaseIn:
				animationCurvebool = false;
				return 1f - Mathf.Cos(Time.timeSinceLevelLoad * Mathf.PI * speed.Value);
			case LerpInterpolationType.EaseOut:
				animationCurvebool = false;
				return Mathf.Sin(Time.timeSinceLevelLoad * Mathf.PI * speed.Value);
			case LerpInterpolationType.Smoothstep:
				animationCurvebool = false;
				return a*a * (3f - 2f*Time.timeSinceLevelLoad);
			case LerpInterpolationType.SimpleSine:
				animationCurvebool = false;
				return a * Mathf.Sin(Time.timeSinceLevelLoad);
			case LerpInterpolationType.DoubleSine:
				animationCurvebool = false;
				return a * Mathf.Sin(Time.timeSinceLevelLoad/setTime.Value);
			case LerpInterpolationType.DoubleByHalfSine:
				animationCurvebool = false;
				return a * (1.5f * Mathf.Sin(Time.timeSinceLevelLoad*setTime.Value));
			case LerpInterpolationType.OtherSimpleSine:
				animationCurvebool = false;
				return a * Mathf.Sin ((Time.timeSinceLevelLoad * speed.Value) * a);
			case LerpInterpolationType.Curve:
				animationCurvebool = true;
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

