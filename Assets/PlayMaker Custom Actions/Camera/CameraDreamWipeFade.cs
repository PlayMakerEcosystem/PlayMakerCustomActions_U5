// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
	"script dependancies":[
		"Assets/Unlit2Pass.shader"
	]
}
EcoMetaEnd ---*/

// v1 for Min. PM 1.8.1+ & Custom/Unlit2Pass shader
// Keywords: Camera Fade


using System;
using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("DreamWipe fade between two camera")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11308.0")]
	public class CameraDreamWipeFade : FsmStateAction
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

		[ActionSection("DreamWipe Settings")]
		[Tooltip("Higher numbers make the DreamWipe effect smoother, but take more CPU time")]
		public FsmInt planeResolution;
		public FsmFloat setWaveScale;
		public FsmFloat setWaveFrequency;
		[Tooltip("You should use Custom/Unlit2Pass")]
		public FsmMaterial planeMaterial;
	
		[ActionSection("Output")]
		public FsmBool inProgress;

		private float alpha;
		private bool  reEnableListener;
		private bool isWorking;

		private RenderTexture renderTex;

		private Vector3[] baseVertices;
		private Vector3[] newVertices;

		private GameObject plane;
		private RenderTexture renderTex2;

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
			planeResolution = 90;
			setWaveScale = 10;
			setWaveFrequency = 10;
		}
		
		public override void OnEnter()
		{
			isWorking = false;
			inProgress.Value = true;
			Camera c1 = cameraFrom.Value.GetComponent<Camera>();
			Camera c2 = cameraTo.Value.GetComponent<Camera>();

			routine = StartCoroutine (DreamWipe (c1, c2, fadeTime.Value,setWaveScale.Value,setWaveFrequency.Value));
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
			
			

		public void InitializeDreamWipe ()
		{

			if (planeMaterial.Value && plane)
				return;
			
			if (planeMaterial.Value == null)
			planeMaterial.Value = new Material (Shader.Find("Custom/Unlit2Pass"));

			plane = new GameObject ("Plane");
			plane.AddComponent <MeshFilter>();
			plane.AddComponent <MeshRenderer>();
			plane.GetComponent<Renderer>().material = planeMaterial.Value;
			plane.GetComponent<Renderer>().shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;
			plane.GetComponent<Renderer>().receiveShadows = false;
			plane.GetComponent<Renderer>().enabled = false;

		
			Mesh planeMesh = new Mesh ();
			(plane.GetComponent<MeshFilter> () as MeshFilter).mesh = planeMesh;

			planeResolution.Value = Mathf.Clamp (planeResolution.Value, 1, 16380);
			baseVertices = new Vector3[4 * planeResolution.Value + 4];
			newVertices = new Vector3[baseVertices.Length];
			Vector2[] newUV = new Vector2[baseVertices.Length];
			int[] newTriangles = new int[18 * planeResolution.Value];

			int idx = 0;
			for (int i = 0; i <= planeResolution.Value; i++) {
				float add = 1.0f / planeResolution.Value * i;
				newUV [idx] = new Vector2 (0.0f, 1.0f - add);
				baseVertices [idx++] = new Vector3 (-1.0f, .5f - add, 0.0f);
				newUV [idx] = new Vector2 (0.0f, 1.0f - add);
				baseVertices [idx++] = new Vector3 (-.5f, .5f - add, 0.0f);
				newUV [idx] = new Vector2 (1.0f, 1.0f - add);
				baseVertices [idx++] = new Vector3 (.5f, .5f - add, 0.0f);
				newUV [idx] = new Vector2 (1.0f, 1.0f - add);
				baseVertices [idx++] = new Vector3 (1.0f, .5f - add, 0.0f);
			}

			idx = 0;

			for (int y = 0; y < planeResolution.Value; y++) {
				for (int x = 0; x < 3; x++) {
					newTriangles [idx++] = (y * 4) + x;
					newTriangles [idx++] = (y * 4) + x + 1;
					newTriangles [idx++] = ((y + 1) * 4) + x;

					newTriangles [idx++] = ((y + 1) * 4) + x;
					newTriangles [idx++] = (y * 4) + x + 1;
					newTriangles [idx++] = ((y + 1) * 4) + x + 1;
				}
			}

			planeMesh.vertices = baseVertices;
			planeMesh.uv = newUV;
			planeMesh.triangles = newTriangles;


			renderTex = new RenderTexture (Screen.width, Screen.height, 24);
			renderTex2 = new RenderTexture (Screen.width, Screen.height, 24);
			renderTex.filterMode = renderTex2.filterMode = FilterMode.Point;
			planeMaterial.Value.SetTexture ("_Tex1", renderTex);
			planeMaterial.Value.SetTexture ("_Tex2", renderTex2);
		}
			

		public IEnumerator DreamWipe (Camera cam1, Camera cam2, float time, float waveScale, float waveFrequency)
		{
			isWorking = true;

			if (!plane) {
				InitializeDreamWipe ();
			}

			waveScale = Mathf.Clamp (waveScale, -.5f, .5f);
			waveFrequency = .25f / (planeResolution.Value / waveFrequency);

			CameraSetup (cam1, cam2, true, false);

	
			Camera cam2Clone = MonoBehaviour.Instantiate (cam2, cam2.transform.position, cam2.transform.rotation) as Camera;
			cam2Clone.depth = cam1.depth + 1;

			if (cam2Clone.depth <= cam2.depth) {
				cam2Clone.depth = cam2.depth + 1;
			}


			cam2Clone.nearClipPlane = .5f;
			cam2Clone.farClipPlane = 1.0f;
			Vector3 p = cam2Clone.transform.InverseTransformPoint (cam2.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, cam2Clone.nearClipPlane)));
			plane.transform.localScale = new Vector3 (-p.x * 2.0f, -p.y * 2.0f, 1.0f);
			plane.transform.parent = cam2Clone.transform;
			plane.transform.localPosition = plane.transform.localEulerAngles = Vector3.zero;


			plane.transform.Translate (Vector3.forward * (cam2Clone.nearClipPlane + .00005f));


			cam2Clone.transform.Translate (Vector3.forward * -1.0f);
			cam2Clone.transform.parent = cam2.transform;


			plane.GetComponent<Renderer>().enabled = true;
			float scale = 0.0f;
			Mesh planeMesh = plane.GetComponent<MeshFilter> ().mesh;
			cam1.targetTexture = renderTex;
			cam2.targetTexture = renderTex2;

		
			float rate = 1.0f / time;

			for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * rate) {
				planeMaterial.Value.color = new Color (planeMaterial.Value.color.r, planeMaterial.Value.color.g, planeMaterial.Value.color.b, Mathf.SmoothStep (0.0f, 1.0f, Mathf.InverseLerp (.75f, .15f, i)));


				for (int j = 0; j < newVertices.Length; j++) {
					newVertices [j] = baseVertices [j];
					newVertices [j].x += Mathf.Sin (j * waveFrequency + i * time) * scale;
				}

				planeMesh.vertices = newVertices;
				scale = Mathf.Sin (Mathf.PI * Mathf.SmoothStep (0.0f, 1.0f, i)) * waveScale;
				yield return 0;
			}
				

	
			CameraCleanup (cam1, cam2);
			plane.GetComponent<Renderer>().enabled = false;
			plane.transform.parent = null;
			MonoBehaviour.Destroy (cam2Clone.gameObject);
			cam1.targetTexture = cam2.targetTexture = null;
			renderTex.Release ();
			renderTex2.Release ();

			isWorking = false;

			if (isWorking == false){
				inProgress.Value = false;	
				Finish();
		}
		}

}
}
