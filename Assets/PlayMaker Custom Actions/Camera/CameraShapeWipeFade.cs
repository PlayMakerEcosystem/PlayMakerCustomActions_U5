// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
	"script dependancies":[
		"Assets/DepthMask.shader"
	]
}
EcoMetaEnd ---*/

// v1 for Min. PM 1.8.1+ & Custom/DepthMask shader
// Keywords: Camera Fade

using System;
using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Shape wipe fade between two camera")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11308.0")]
	public class CameraShapeWipeFade : FsmStateAction
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

		[ActionSection("ShapeWipe Settings")]
		public ZoomType zoomTypeSelect;
		public enum ZoomType
		{
			Grow,
			Shrink,
		}

		public FsmFloat amountRotate;
		public Mesh setMesh;
		public FsmBool destroyMesh;
		[Tooltip("You should use Custom/DepthMask")]
		public FsmMaterial shapeMaterial;

		[ActionSection("Output")]
		public FsmBool inProgress;

		private float alpha;
		private bool  reEnableListener;

		private Transform shape;
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
			amountRotate = 0f;
			destroyMesh = true;
		}
		
		public override void OnEnter()
		{
			isWorking = false;
			inProgress.Value = true;
			Camera c1 = cameraFrom.Value.GetComponent<Camera>();
			Camera c2 = cameraTo.Value.GetComponent<Camera>();

			routine = StartCoroutine (ShapeWipe (c1, c2, fadeTime.Value,zoomTypeSelect,setMesh,amountRotate.Value));
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
			

		private IEnumerator ShapeWipe (Camera cam1, Camera cam2, float time, ZoomType zoom, Mesh mesh, float rotateAmount)
		{
			isWorking = true;

			if (shapeMaterial.Value == null)
			shapeMaterial.Value = new Material (Shader.Find("Custom/DepthMask"));

			if (!shape) {
				GameObject gobjShape = new GameObject ("Shape");
				gobjShape.AddComponent <MeshFilter>();
				gobjShape.AddComponent <MeshRenderer>();
				gobjShape.GetComponent<Renderer>().shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;
				gobjShape.GetComponent<Renderer>().receiveShadows=false;
				shape = gobjShape.transform;
				shape.GetComponent<Renderer>().material = shapeMaterial.Value;

			}

			CameraSetup (cam1, cam2, true, false);
			Camera useCam = (zoom == ZoomType.Shrink) ? cam1 : cam2;
			Camera otherCam = (zoom == ZoomType.Shrink) ? cam2 : cam1;
			float originalDepth = otherCam.depth;
			CameraClearFlags originalClearFlags = otherCam.clearFlags;
			otherCam.depth = useCam.depth + 1;
			otherCam.clearFlags = CameraClearFlags.Depth;

			shape.gameObject.SetActive(true);
			(shape.GetComponent<MeshFilter> () as MeshFilter).mesh = mesh;
			shape.position = otherCam.transform.position + otherCam.transform.forward * (otherCam.nearClipPlane + .01f);
			shape.parent = otherCam.transform;
			shape.localRotation = Quaternion.identity;

			if (useCurve.Value) {
				float rate = 1.0f / time;

				if (zoom == ZoomType.Shrink) {
					for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime * rate) {
						float t = curve.curve.Evaluate (i);
						shape.localScale = new Vector3 (t, t, t);
						shape.localEulerAngles = new Vector3 (0.0f, 0.0f, i * rotateAmount);
						yield return 0;
					}
				} else {
					for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * rate) {
						float t = curve.curve.Evaluate (i);
						shape.localScale = new Vector3 (t, t, t);
						shape.localEulerAngles = new Vector3 (0.0f, 0.0f, -i * rotateAmount);
						yield return 0;
					}   
				}
			} else {
				float rate = 1.0f / time;
				if (zoom == ZoomType.Shrink) {
					for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime * rate) {
						float t = Mathf.Lerp (1.0f, 0.0f, Mathf.Sin ((1.0f - i) * Mathf.PI * 0.5f));
						shape.localScale = new Vector3 (t, t, t);
						shape.localEulerAngles = new Vector3 (0.0f, 0.0f, i * rotateAmount);
						yield return 0;
					}
				} else {
					for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * rate) {
						float t = Mathf.Lerp (1.0f, 0.0f, Mathf.Sin ((1.0f - i) * Mathf.PI * 0.5f));
						shape.localScale = new Vector3 (t, t, t);
						shape.localEulerAngles = new Vector3 (0.0f, 0.0f, -i * rotateAmount);
						yield return 0;
					}   
				}
			}

			otherCam.clearFlags = originalClearFlags;
			otherCam.depth = originalDepth;
			CameraCleanup (cam1, cam2);
			shape.parent = null;
			shape.gameObject.SetActive(false);

			if (destroyMesh.Value == true){
			MonoBehaviour.Destroy(shape.gameObject);
			}

			isWorking = false;

			if (isWorking == false){
				inProgress.Value = false;	
				Finish();
			}
		}
			
			
	}
}
