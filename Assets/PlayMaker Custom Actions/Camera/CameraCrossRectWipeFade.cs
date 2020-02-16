// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// NOTE: For UNITY 5 or UNITY 4+ PRO
// v1 for Min. PM 1.8.1+
// Keywords: Camera Fade

using System;
using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Cross fade between two camera")]
	public class CameraRectWipeFade : FsmStateAction
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

		[ActionSection("RectWipe Settings")]
		public ZoomType zoomTypeSelect;
		public enum ZoomType
		{
			Grow,
			Shrink,
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

			routine = StartCoroutine (RectWipe (c1, c2, fadeTime.Value,zoomTypeSelect));
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
			

		private IEnumerator RectWipe (Camera cam1, Camera cam2, float time, ZoomType zoom)
		{
			isWorking = true;
			CameraSetup (cam1, cam2, true, false);
			Camera useCam = (zoom == ZoomType.Shrink) ? cam1 : cam2;
			Camera otherCam = (zoom == ZoomType.Shrink) ? cam2 : cam1;
			Rect originalRect = useCam.rect;
			float originalDepth = useCam.depth;
			useCam.depth = otherCam.depth + 1;

			if (useCurve.Value) {
				float rate = 1.0f / (time);

				if (zoom == ZoomType.Shrink) {
					for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * rate) {
						float t = curve.curve.Evaluate (i) * 0.5f;
						cam1.rect = new Rect (t, t, 1.0f - t * 2, 1.0f - t * 2);
						yield return 0;
					}
				} else {
					for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * rate) {
						float t = curve.curve.Evaluate (i) * 0.5f;
						cam2.rect = new Rect (.5f - t, .5f - t, t * 2.0f, t * 2.0f);
						yield return 0;
					}
				}
			} else {
				float rate = 1.0f / (time * 2);
				if (zoom == ZoomType.Shrink) {
					for (float i = 0.0f; i < .5; i += Time.deltaTime * rate) {
						float t = Mathf.Lerp (0.0f, .5f, Mathf.Sin (i * Mathf.PI));
						cam1.rect = new Rect (t, t, 1.0f - t * 2, 1.0f - t * 2);
						yield return 0;
					}
				} else {
					for (float i = 0.0f; i < .5f; i += Time.deltaTime * rate) {
						float t = Mathf.Lerp (.5f, 0.0f, Mathf.Sin ((.5f - i) * Mathf.PI));
						cam2.rect = new Rect (.5f - t, .5f - t, t * 2.0f, t * 2.0f);
						yield return 0;
					}
				}
			}

			useCam.rect = originalRect;
			useCam.depth = originalDepth;
			CameraCleanup (cam1, cam2);

			isWorking = false;

			if (isWorking == false){
				inProgress.Value = false;	
				Finish();
			}
		}
			
			
	}
}
