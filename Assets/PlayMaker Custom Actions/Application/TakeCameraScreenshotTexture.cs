// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// NOTE: For UNITY 5 or UNITY 4+ PRO
// v1.1
// Keywords: Screenshot Texture

using System;
using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Saves a Screenshot from the camera. Save as png or jpg.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11308.0")]
	public class TakeCameraScreenshotTexture : FsmStateAction
	{
		[ActionSection("Camera")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject camera to take picture from (Must have a Camera component).")]
		public FsmGameObject gameObject;
		
		[ActionSection("Screen Setup")]
		public FsmInt resWidth;
		public FsmInt resHeight;
		[Tooltip("Automatically get the current resolution - RECOMMENDED")]
		public FsmBool Auto;
		[Tooltip("Use the current resolution - NOT RECOMMENDED")]
		public FsmBool useCurrentRes;
		
		
		[ActionSection("Output")]
		public FsmTexture newTexture;

		
		[ActionSection("Option")]
		public FsmBool inclGui;
		[Tooltip("Must be 0 or 16 or 24 - The precision of the render texture's depth buffer in bits / When 0 is used, then no Z buffer is created by a render texture")]
		private int Depth;
		public enum depthSelect  {
			
			_24,
			_16,
			_0,
			
		};
		
		public depthSelect setDepth;

		static private RequestHelperTex _helperTex;
		
		public override void Reset()
		{
			gameObject = null;
			Auto = true;
			useCurrentRes = false;
			resWidth = 2560;
			resHeight = 1440;
			newTexture = null;
			inclGui = true;
		
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnEnter()
		{
			if (useCurrentRes.Value == true || Auto.Value == true) getResolutions();

			switch(setDepth){
			case depthSelect._0:
					Depth = 0;
					break;

			case depthSelect._16:
				Depth = 16;
				break;

			case depthSelect._24:
				Depth = 24;
				break;
				
			}
			if (inclGui.Value == true){

				_helperTex = ( new GameObject("RequestHelper") ).AddComponent< RequestHelperTex >();
				_helperTex.startEndofFrame(this);

			}
		}
		
		public override void OnLateUpdate()
		{
			if (inclGui.Value == false)takePicture(true);
		}

		public void takePicture (bool state)
		{
			if (state) getPicture();
		}

		public void getPicture()
		{

	
			RenderTexture rt = new RenderTexture(resWidth.Value, resHeight.Value, Depth);
			gameObject.Value.GetComponent<Camera>().targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth.Value, resHeight.Value, TextureFormat.RGB24, false);
			gameObject.Value.GetComponent<Camera>().Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth.Value, resHeight.Value), 0, 0);
			screenShot.Apply();
			gameObject.Value.GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null; 
			UnityEngine.Object.Destroy(rt);
			newTexture.Value = screenShot;


			if (inclGui.Value == true) _helperTex = null;
			Finish();
		}

		public void getResolutions()
		{
			if (useCurrentRes.Value == true){
				resWidth.Value = Screen.currentResolution.width;
				resHeight.Value = Screen.currentResolution.height;
			}
			else {
				resWidth.Value = Screen.width;
				resHeight.Value = Screen.height;
			}
			return;
		}

//---------------------
		public sealed class RequestHelperTex : MonoBehaviour {

			TakeCameraScreenshotTexture _action;

			public void startEndofFrame(TakeCameraScreenshotTexture action)
			{
				_action= action;
				StartCoroutine("getPictureIENUM");
				
			}
			
			IEnumerator getPictureIENUM()
			{
				yield return new WaitForEndOfFrame();
				_action.takePicture (true);

				Destroy(this.gameObject);
			
			}


		}
	}
}

