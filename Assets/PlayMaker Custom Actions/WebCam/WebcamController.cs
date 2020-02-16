// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("WebCam")]
	[Tooltip("Controls for webcam")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12472.0")]
	public class WebcamController : FsmStateAction
    {

		[ActionSection("Controller")]
		public FsmBool play;
		public FsmBool pause;
		public FsmBool stop;

		[ActionSection("Set Webcam Material")]
		public FsmMaterial webcamMaterial;
		private WebCamTexture webCamTex;

		[ActionSection("Custom Options")]
		public FsmBool useCustom;
		public FsmInt webTextureHeight;
		public FsmInt webTextureWidth;
		public FsmInt setFps;
		public FsmInt device;
		public FsmString webcamName;
		
		[ActionSection("Optional UI options")]
		[Tooltip("autoAdjust rotation for UI raw image only")]
		public FsmBool autoAdjustRotation;
		[Tooltip("GameObject with UI raw image")]
		public FsmGameObject UiRawImage;
		public FsmBool useAspectRatio;
		
		[ActionSection("Options")]
		public FsmBool safeMode;
		public enum FilterModeSelect{
			Point,
			Bilinear,
			Trilinear,
		}
		
		public FilterModeSelect filterModeselect;

		[ActionSection("Take picture")]
		public FsmBool takePicture;
		private Texture2D webcamTextTemp;
		public FsmTexture webcamTexture;
		public FsmBool useCrop;
		public FsmInt cropSize;
		public FsmInt offsetX;
		public FsmInt offsetY;
		public enum Mode{
			RGB24,
			ARGB32,
			RGB565,
			DXT1,
			DXT5,
			DXT1Crunched,
			DXT5Crunched,
			PVRTC_RGB2,
			PVRTC_RGBA2,
			PVRTC_RGB4,
			PVRTC_RGBA4,
			ETC_RGB4,
			ETC2_RGB,
			ETC2_RGBA1,
			ETC2_RGBA8,
			ETC_RGB4_3DS,
			ETC_RGBA8_3DS,
		}
		
		public Mode textureFormat;

		[ActionSection("Output")]
		public FsmBool isPlaying;

		private bool endTemp;
		private Texture2D cropTex;
		private RectTransform _rt;
		private TextureFormat modeSelect;
		private UnityEngine.UI.RawImage _texture;
		private UnityEngine.UI.AspectRatioFitter setAspectRatio;
		private string finalWebCamName;

		public override void Reset()
		{

			webcamMaterial = null;
			useCustom = false;
			play = true;
			stop = false;
			pause = false;
			endTemp = false;
			webTextureHeight = null;
			webTextureWidth = null;
			setFps = 24;
			webcamTexture = null;
			takePicture = false;
			useCrop = false;
			offsetX = 0;
			offsetY = 0;
			cropSize = 0;
			textureFormat = Mode.RGB24;
			safeMode = true;
			autoAdjustRotation = true;
			device = 0;
			webcamName = null;
			useAspectRatio = true;
		
		}
		
		public override void OnEnter()
		{
	
			if (safeMode.Value == true && webCamTex != null){

				if (webCamTex.isPlaying == true){
				webCamTex.Stop ();
				}
			}

			if (autoAdjustRotation.Value == true && UiRawImage.Value != null)
			{
				_texture = UiRawImage.Value.GetComponent<UnityEngine.UI.RawImage>();
		
				if (UiRawImage.Value.GetComponent<UnityEngine.UI.AspectRatioFitter>() != null){

					setAspectRatio = UiRawImage.Value.GetComponent<UnityEngine.UI.AspectRatioFitter>();
				}
			}


			if (useCustom.Value == true) {

				if (webcamName.Value.Length >= 1){
					finalWebCamName =  webcamName.Value;
				}

				else {
					finalWebCamName = WebCamTexture.devices[device.Value].name;
				}
				
				webCamTex = new WebCamTexture(finalWebCamName,webTextureWidth.Value,webTextureHeight.Value,setFps.Value);

			}

			else {
				webCamTex = new WebCamTexture();
		
			}

			switch (filterModeselect)
			{
			case FilterModeSelect.Bilinear:
				webCamTex.filterMode = FilterMode.Bilinear;
				break;
			case FilterModeSelect.Point:
				webCamTex.filterMode = FilterMode.Point;
				break;
			case FilterModeSelect.Trilinear:
				webCamTex.filterMode = FilterMode.Trilinear;;
				break;
			}

			webcamMaterial.Value.SetTexture("_MainTex", webCamTex);

			
			PlayCam ();
			
			
		}
		
		public override void OnUpdate()
		{
		
			if (stop.Value == true)	{
				play.Value = false;
				pause.Value = false;

				StopCam ();
				endTemp = true;
			}

			if (pause.Value == true)	{
				play.Value = false;
				stop.Value = false;

				PauseCam ();
				endTemp = true;
			}

			if (play.Value == true)	{
				pause.Value = false;
				stop.Value = false;
			}

			if (pause.Value == false && pause.Value == false && play.Value == true && endTemp == true){
				PlayCam ();
				endTemp = false;
			}


			if (takePicture.Value == true){
			    TakePicture ();
			}

			if (autoAdjustRotation.Value == true && webCamTex.width >=100){

				int cwNeeded = webCamTex.videoRotationAngle;
				int ccwNeeded = -cwNeeded;
			
				if ( webCamTex.videoVerticallyMirrored ) ccwNeeded += 180;

				UiRawImage.Value.transform.localEulerAngles = new Vector3(0f,0f,ccwNeeded);

				float videoRatio = (float)webCamTex.width/(float)webCamTex.height;

				if (useAspectRatio.Value == true){
					setAspectRatio.aspectRatio = videoRatio;
				}

				if ( webCamTex.videoVerticallyMirrored )
					_texture.uvRect = new Rect(1,0,-1,1);
				else
					_texture.uvRect = new Rect(0,0,1,1);

			}

			isPlaying.Value = webCamTex.isPlaying;
		}

		void StopCam ()	{

			webCamTex.Stop ();
			return;

		}

		void PauseCam ()	{
			
			webCamTex.Pause ();
			return;
			
		}

		void PlayCam ()	{
			
			webCamTex.Play();
			return;
			
		}


		void TakePicture ()	{

			switch (textureFormat)
			{
			
			case Mode.ARGB32:
				modeSelect = TextureFormat.ARGB32;
				break;
			case Mode.DXT1:
				modeSelect = TextureFormat.DXT1;
				break;
			case Mode.DXT1Crunched:
				modeSelect = TextureFormat.DXT1Crunched;
				break;
			case Mode.DXT5:
				modeSelect = TextureFormat.DXT5;
				break;
			case Mode.DXT5Crunched:
				modeSelect = TextureFormat.DXT5Crunched;
				break;
			case Mode.ETC_RGB4:
				modeSelect = TextureFormat.ETC_RGB4;
				break;
			case Mode.ETC_RGB4_3DS:
				modeSelect = TextureFormat.ETC_RGB4_3DS;
				break;
			case Mode.ETC_RGBA8_3DS:
				modeSelect = TextureFormat.ETC_RGBA8_3DS;
				break;
			case Mode.ETC2_RGB:
				modeSelect = TextureFormat.ETC2_RGB;
				break;
			case Mode.ETC2_RGBA1:
				modeSelect = TextureFormat.ETC2_RGBA1;
				break;
			case Mode.ETC2_RGBA8:
				modeSelect = TextureFormat.ETC2_RGBA8;
				break;
			case Mode.PVRTC_RGB2:
				modeSelect = TextureFormat.PVRTC_RGB2;
				break;
			case Mode.PVRTC_RGB4:
				modeSelect = TextureFormat.PVRTC_RGB4;
				break;
			case Mode.PVRTC_RGBA2:
				modeSelect = TextureFormat.PVRTC_RGBA2;
				break;
			case Mode.PVRTC_RGBA4:
				modeSelect = TextureFormat.PVRTC_RGBA4;
				break;
			case Mode.RGB24:
				modeSelect = TextureFormat.RGB24;
				break;
			case Mode.RGB565:
				modeSelect = TextureFormat.RGB565;
				break;
			}

			webcamTextTemp = new Texture2D(webCamTex.width, webCamTex.height, modeSelect,false);

			switch (filterModeselect)
			{
			case FilterModeSelect.Bilinear:
				webcamTextTemp.filterMode = FilterMode.Bilinear;
				break;
			case FilterModeSelect.Point:
				webcamTextTemp.filterMode = FilterMode.Point;
				break;
			case FilterModeSelect.Trilinear:
				webcamTextTemp.filterMode = FilterMode.Trilinear;;
				break;
			}

			if (useCrop.Value == true){
	
				cropTex = new Texture2D (cropSize.Value,cropSize.Value);

				if (webCamTex.height < cropSize.Value && webCamTex.width < cropSize.Value){
					Debug.LogWarning("<b>[WebcamController]</b><color=#FF9900ff>The Webcam picture is smaller than the crop - set to "+webCamTex.height+" -  Please review!</color>", this.Owner);

				}

				else {

					Color[] data = webCamTex.GetPixels(((webcamTextTemp.width - cropSize.Value) / 2)+offsetY.Value,((webcamTextTemp.height - cropSize.Value) / 2)+offsetX.Value,cropSize.Value, cropSize.Value);//((webcamTextTemp.width - cropSize.Value) / 2, (webcamTextTemp.height - cropSize.Value) / 2, cropSize.Value, cropSize.Value, 0);
					cropTex.SetPixels(0,0,cropSize.Value, cropSize.Value,data,0);
				}

				cropTex.Apply();
				webcamTexture.Value = cropTex;
			}

			else {

			Color32[] pix = webCamTex.GetPixels32();
			webcamTextTemp.SetPixels32(pix);
			webcamTextTemp.Apply();
	
			webcamTexture.Value = webcamTextTemp;

			}


			webcamTextTemp = null;
			cropTex = null;

			takePicture.Value = false;

			return;


	}


}
}
