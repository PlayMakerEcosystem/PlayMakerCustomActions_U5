// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10495.0
// Keywords: Fighter 2d Camera / Camera smooth follow multiple gameobject

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("2D Camera track multiple targets - Camera focuses on every GameObject on the screen. Unlimited objs - must all have same Tag OR set targets array.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10495.0")]
	public class TargetsTrackingOrthoCamera : FsmStateAction
	{
		[ActionSection("Camera")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
		[TitleAttribute("Camera")]
		public FsmGameObject gameObject;
		[Tooltip("The camera size.")]
		[TitleAttribute("Camera Min size.")]
		public FsmFloat orthoSizeMin;
		[TitleAttribute("Camera Max size.")]
		public FsmFloat orthoSizeMax;

		[ActionSection("Camera Buffer")]
		public FsmFloat boundingBoxPadding;
		public FsmFloat zoomSpeed;
	
		[ActionSection("Targets")]
		public FsmGameObject[] targets;
		[UIHint(UIHint.Tag)]
		[TitleAttribute("or by Tag")]
		public FsmBool useTag;
		public FsmString tag;

		[ActionSection("Camera Update Function")]
		public updateType updateTypeSelect;
		public enum updateType
		{
			Update,
			LateUpdate
		};

		[UIHint(UIHint.FsmBool)]
		public FsmBool everyFrame;

		private Transform objTransform;

		private float minX;
		private float maxX;
		private float minY;
		private float maxY;
		private Camera _camera;

		private GameObject[] players;

		public enum LerpInterpolationType {Off,Linear,Quadratic,EaseIn,EaseOut,Smoothstep,Smootherstep,DeltaTime,SimpleSine,DoubleSine,DoubleByHalfSine};

		private FsmBool lerpOn;
		private float t;
		private Vector3 tempPlayer;

		public override void Reset()
		{

			gameObject = null;
			zoomSpeed = 20f;
			updateTypeSelect = updateType.LateUpdate;
			lerpOn = true;
			everyFrame = true;
			orthoSizeMin = 1f;
			orthoSizeMax= 100f;
			boundingBoxPadding=15f; 
			boundingBoxPadding=2f;
			useTag = false;
		}


		public override void OnEnter()
		{
			var go = gameObject.Value;
			if (go == null) return;

			_camera = go.GetComponent<Camera>();

			objTransform =  go.GetComponent<Transform>();

			if (_camera.orthographic == false){
				Debug.Log ("<color=#6B8E23ff> You need a orthographic camera.Please review!</color>",this.Owner);
					}

			if (useTag.Value == false && targets.Length == 0){
				goDebug();
				Finish();
			}

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


			if (updateTypeSelect == updateType.Update){
				Rect boundingBox = CalculateBounds();
				_camera.transform.position = CalculateCameraPosition(boundingBox);
				_camera.orthographicSize = CalculateCameraPosAndSize(boundingBox);
			}
		}

		public override void OnLateUpdate()
		{
			if (updateTypeSelect != updateType.Update){
				Rect boundingBox = CalculateBounds();
				_camera.transform.position = CalculateCameraPosition(boundingBox);
				_camera.orthographicSize = CalculateCameraPosAndSize(boundingBox);
			}
		
		}

		Vector3 CalculateCameraPosition(Rect boundingBox)
		{
			Vector2 boundingBoxCenter = boundingBox.center;
			
			return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, _camera.transform.position.z);
		}


		Rect CalculateBounds() { 

			minX = Mathf.Infinity; 
			maxX = -Mathf.Infinity; 
			minY = Mathf.Infinity; 
			maxY = -Mathf.Infinity;
	

			if(targets.Length <= 0) {

				if (tag.Value == null || tag.IsNone){ 

				    players = GameObject.FindGameObjectsWithTag("MainCamera");
					goDebug();
					Finish();
				}

				else {
				useTag.Value = true;
				players = GameObject.FindGameObjectsWithTag(tag.Value);
				
				}
					
				if (players.Length == 0 &&  targets.Length ==0){ 
					players = GameObject.FindGameObjectsWithTag("MainCamera");
					goDebug();
					Finish();
				}



				for (int i = 0; i < players.Length; i++){

					if ( players[i] != null){
					tempPlayer = players[i].transform.position;
					
					
						minX = Mathf.Min(minX, tempPlayer.x);
						minY = Mathf.Min(minY, tempPlayer.y);
						maxX = Mathf.Max(maxX, tempPlayer.x);
						maxY = Mathf.Max(maxY, tempPlayer.y);
					}


				}


			}

			else {


				for (int i = 0; i < targets.Length; i++){

					if ( targets[i].Value != null){
					tempPlayer = targets[i].Value.transform.position;
				
						minX = Mathf.Min(minX, tempPlayer.x);
						minY = Mathf.Min(minY, tempPlayer.y);
						maxX = Mathf.Max(maxX, tempPlayer.x);
						maxY = Mathf.Max(maxY, tempPlayer.y);
			
				}

				
				
						}

			}

			if (players == null && targets.Length == 0){
				Debug.Log ("<color=#6B8E23ff> You always need one target or you will get problems!</color>",this.Owner);
				Finish();
			}
			
			return Rect.MinMaxRect(minX - boundingBoxPadding.Value, maxY + boundingBoxPadding.Value, maxX + boundingBoxPadding.Value, minY - boundingBoxPadding.Value);
		}

		float CalculateCameraPosAndSize(Rect boundingBox) { 


			float orthographicSize = _camera.orthographicSize;
			float tempResult = 0;

			Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
			Vector3 topRightAsViewport = _camera.WorldToViewportPoint(topRight);
			
			if (topRightAsViewport.x >= topRightAsViewport.y)
				orthographicSize = Mathf.Abs(boundingBox.width) / _camera.aspect / 2f;
			else
				orthographicSize = Mathf.Abs(boundingBox.height) / 2f;


			tempResult = Mathf.Clamp(Mathf.Lerp(_camera.orthographicSize, orthographicSize, Time.smoothDeltaTime * zoomSpeed.Value), orthoSizeMin.Value, orthoSizeMax.Value);


			return tempResult;
		}

		void goDebug(){
			Debug.Log ("<color=#6B8E23ff> You always need one target or you will get problems!</color>",this.Owner);
			Finish();
			
		}
	}
}

