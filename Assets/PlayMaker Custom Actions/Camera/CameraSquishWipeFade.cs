// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// v1 for Min. PM 1.8.1+

// Keywords: Camera Fade


using System;
using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Squish fade between two camera")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11308.0")]
	public class CameraSquishWipeFade : FsmStateAction
	{
		[ActionSection("Camera")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("From this camera")]
		public FsmGameObject cameraFrom;
		[CheckForComponent(typeof(Camera))]
		[Tooltip("To this camera")]
		public FsmGameObject cameraTo;

		[ActionSection("General settings")]
		public FsmFloat fadeTime;
		public FsmBool useCurve;
		public FsmAnimationCurve curve;

		[ActionSection("SquishWipe Settings")]
		public TransitionType TypeSelect;
		public enum TransitionType
		{
			Left,
			Right,
			Up,
			Down
		}
			

		[ActionSection("Output")]
		public FsmBool inProgress;

		private float alpha;
		private bool  reEnableListener;
		private bool isWorking;

		private Coroutine routine;

		public override void Reset()
		{
			cameraFrom = null;
			cameraTo = null;
			fadeTime = 2.0f;
			curve = null;
			inProgress = false;
			isWorking = false;
			useCurve = true;
		}
		
		public override void OnEnter()
		{
			isWorking = false;
			inProgress.Value = true;
			Camera c1 = cameraFrom.Value.GetComponent<Camera>();
			Camera c2 = cameraTo.Value.GetComponent<Camera>();

			routine = StartCoroutine (SquishWipe (c1, c2, fadeTime.Value,TypeSelect));
		}


		public override void OnExit()
		{
			StopCoroutine(routine);
		}
			

		IEnumerator AlphaTimer (float time)
		{
			float rate = 1.0f / time;
			if (useCurve.Value) {
				float t = 1.0f;

				while (t > 0f) {
					alpha = curve.curve.Evaluate (t);
					t -= Time.deltaTime * rate;

					yield return 0;
				}
			} else {
				for (alpha = 1.0f; alpha > 0.0f; alpha -= Time.deltaTime * rate) {
					yield return 0;

				}
			}

		}

		private void CameraSetup (Camera cam1, Camera cam2, bool cam1Active,bool enableThis)
		{

			cam1.gameObject.SetActive(cam1Active);
			cam2.gameObject.SetActive(true);
			AudioListener listener = cam2.GetComponent<AudioListener> ();

			if (listener) {
				reEnableListener = listener.enabled ? true : false;
				listener.enabled = false;
			}
		}

		void CameraCleanup (Camera cam1, Camera cam2)
		{
			AudioListener listener = cam2.GetComponent<AudioListener> ();
			if (listener && reEnableListener) {
				listener.enabled = true;
			}
			cam1.gameObject.SetActive(false);
		}
			
			
		public IEnumerator SquishWipe (Camera cam1, Camera cam2, float time, TransitionType transitionType)
		{
			isWorking = true;

			Rect originalCam1Rect = cam1.rect;
			Rect originalCam2Rect = cam2.rect;
			Matrix4x4 cam1Matrix = cam1.projectionMatrix;
			Matrix4x4 cam2Matrix = cam2.projectionMatrix;
			CameraSetup (cam1, cam2, true, false);

			float rate = 1.0f / time;
			float t = 0.0f;
			float i = 0.0f;

			while (i < 1.0f) {
				if (useCurve.Value) { 
					i = curve.curve.Evaluate (t);
					t += Time.deltaTime * rate;
				} else {
					i += Time.deltaTime * rate;
				}

				switch (transitionType) {
				case TransitionType.Right:
					cam1.rect = new Rect (i, 0, 1.0f, 1.0f);
					cam2.rect = new Rect (0, 0, i, 1.0f);
					break;
				case TransitionType.Left:
					cam1.rect = new Rect (0, 0, 1.0f - i, 1.0f);
					cam2.rect = new Rect (1.0f - i, 0, 1.0f, 1.0f);
					break;
				case TransitionType.Up:
					cam1.rect = new Rect (0, i, 1.0f, 1.0f);
					cam2.rect = new Rect (0, 0, 1.0f, i);
					break;
				case TransitionType.Down:
					cam1.rect = new Rect (0, 0, 1.0f, 1.0f - i);
					cam2.rect = new Rect (0, 1.0f - i, 1.0f, 1.0f);
					break;
				}

				cam1.projectionMatrix = cam1Matrix;
				cam2.projectionMatrix = cam2Matrix;

				yield return 0;
			}

			cam1.rect = originalCam1Rect;
			cam2.rect = originalCam2Rect;
			CameraCleanup (cam1, cam2);

			isWorking = false;

			if (isWorking == false){
				inProgress.Value = false;	
				Finish();
		}
			
	}

}
}
