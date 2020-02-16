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
	[Tooltip("Cross fade between two camera using RenderTexture")]
	public class CameraCrossFadePro : FsmStateAction
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

		[ActionSection("Output")]
		public FsmBool inProgress;


		private Texture tex;
		private RenderTexture renderTex;
		private float alpha;
		private bool  reEnableListener;
		private bool isWorking;

		private Coroutine routine;

		public override void Reset()
		{
			cameraFrom = null;
			cameraTo = null;
			fadeTime = 2.0f;
			inProgress = false;
			isWorking = false;
		}
		
		public override void OnEnter()
		{
			isWorking = false;
			inProgress.Value = true;
			Camera c1 = cameraFrom.Value.GetComponent<Camera>();
			Camera c2 = cameraTo.Value.GetComponent<Camera>();

			routine = StartCoroutine (CrossFadePro (c1, c2, fadeTime.Value));
		}
			
		public override void OnGUI ()
		{
			if (isWorking) {
				GUI.depth = -9999999;
				GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tex, ScaleMode.ScaleToFit, false, 0.0F);
			}
		}
	
		public override void OnExit()
		{
			StopCoroutine(routine);
		}
			

		IEnumerator AlphaTimer (float time)
		{
			float rate = 1.0f / time;
				for (alpha = 1.0f; alpha > 0.0f; alpha -= Time.deltaTime * rate) {
					Debug.Log ("OK");
					yield return 0;

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

		private IEnumerator CrossFadePro (Camera cam1, Camera cam2, float time)
		{
			isWorking = true;
			if (!renderTex) {
				renderTex = new RenderTexture (Screen.width, Screen.height, 24);
			}

			cam1.targetTexture = renderTex;
			tex = renderTex;
			CameraSetup (cam1, cam2, true, true);
		
			yield return StartCoroutine (AlphaTimer (time));

			cam1.targetTexture = null;
			renderTex.Release ();
			CameraCleanup (cam1, cam2);

			isWorking = false;
			if (isWorking == false){
				inProgress.Value = false;	
				Finish();
			}
		}


			
	}
}
