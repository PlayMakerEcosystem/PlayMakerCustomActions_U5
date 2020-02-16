// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords:  orbit, planet gravity, extreme force, magnet

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Create your own fake gravity (F = GMm/r^2)")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11962.0")]
	public class SetGameobjectAsCenterOfGravity : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
	
		[ActionSection("Gravity setting")]
		public FsmFloat gravity;

		[TitleAttribute("Force Multiplier")]
		[HasFloatSlider(-5f, 5f)]
		public FsmFloat forceMultiplierTemp;
		public enum UpdateType { FixedUpdate, LateUpdate, Update };
		public UpdateType updateSelect;

		[ActionSection("Target Filter")]
		public FsmBool useOverlapSphere;
		public FsmFloat overlapSphereSize;
		[Tooltip("")]
		[TitleAttribute("Or use Tag")]
		public FsmBool usetag;
		[UIHint(UIHint.Tag)]
		[TitleAttribute("by Tag")]
		public FsmString tag;
		[TitleAttribute("Incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;
		[UIHint(UIHint.Layer)]
		public int layer;

		[ActionSection("Control")]
		public FsmBool disableGravity;

		[ActionSection("Options")]
		public FsmBool useAddForceAtPosition;
		public enum ForceType { None, Acceleration};
		public ForceType forceType;
		public FsmBool useAddTorque;
		public FsmFloat torqueAmount;
		public FsmBool useOrbit;
		public enum OrbitType { normal, optionUp, optionForward, optionDown, optionBack };
		public OrbitType orbitType;
		public FsmFloat orbitOffset;
		public FsmFloat orbitSpeed;
		public FsmBool useFx;
		public enum FxType { normal, Acceleration, Impulse, VelocityChange};
		public FxType fxType;
		public FsmBool onlyUseFx;
		public FsmFloat fxSpeed;

		[ActionSection("Speed Velocity Options")]
		[TitleAttribute("Disable Physics")]
		public FsmBool useSpeedVelocity;
		public FsmFloat setSpeedVelocity;

		private double G;
		private float M;
		private float m;
		private GameObject go;
		private Collider[] colliders;
		private Rigidbody rb;
		private float forceMultiplier;
		private GameObject[] objtag;
		private Collider[] SphereTemp;
		private Vector3 direction;

		public override void Reset()
		{
			gameObject = null;
			useOverlapSphere = true;
			gravity = 6.25f;
			useOrbit = false;
			overlapSphereSize = 50;
			forceMultiplierTemp = 2;
			layerFilterOn = false;
			layer = 0;
			disableGravity = false;
			updateSelect = UpdateType.FixedUpdate;
			forceType = ForceType.None;
			orbitType = OrbitType.normal;
			useSpeedVelocity = false;
			setSpeedVelocity = 500f;
			useAddForceAtPosition = false;
			useAddTorque = false;
			torqueAmount = 0;
			orbitOffset = 20f;
			orbitSpeed = 20f;
			useFx = false;
			fxSpeed = 0;
			fxType = FxType.Impulse;
			onlyUseFx =false;
		}

		public override void OnPreprocess()
		{
			if (updateSelect == UpdateType.FixedUpdate )
			{
				Fsm.HandleFixedUpdate = true;
			}

			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateSelect == UpdateType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}



		public override void OnEnter()
		{
		
			go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
			rb = go.GetComponent<Rigidbody>();
			M = rb.mass;
			M = M * (float)1e+09;
			rb.mass = M;
			G = gravity.Value * Mathf.Pow(10, -11);

		}

		public override void OnUpdate () 
		{
			if (updateSelect == UpdateType.Update) {
			if (disableGravity.Value == false){
				Action();
			}
			}
		}

		public override void OnLateUpdate () 
		{
			if (updateSelect == UpdateType.LateUpdate) {
				if (disableGravity.Value == false){
					Action();
				}
			}
			
		}

		public override void OnFixedUpdate()
		{
			if (updateSelect == UpdateType.FixedUpdate) {
			if (disableGravity.Value == false){
				Action();
			}
			}
		}
				
		void Action()
		{

			if (tag.IsNone || tag.Value == null){
				Debug.LogWarning ("<color=#6B8E23ff>No object with tag:  "+tag.Value+"  - Using 'Untagged'!!!</color>");
				tag.Value = "Untagged";
			}	

			forceMultiplier = forceMultiplierTemp.Value * Mathf.Pow(10, 5);

//
			if (usetag.Value == true & useOverlapSphere.Value == false) {
			
				objtag = GameObject.FindGameObjectsWithTag(tag.Value);
				colliders = new Collider[objtag.Length];
				List<GameObject> tempCol = new List<GameObject>();

			if (layerFilterOn.Value == true)
			{
				for (int i =0; i < objtag.Length; i++)
				{

				if (objtag[i].layer == layer){
						tempCol.Add(objtag[i]);
				}

				}
				colliders = new Collider[tempCol.Count];
				objtag = new GameObject[tempCol.Count];

					for (int i =0; i < tempCol.Count; i++)
					{
						objtag[i] = tempCol[i];
					}
			}


			for (int i =0; i < objtag.Length; i++)
			{

				colliders[i] = objtag[i].GetComponent<Collider>();

			}
			}
//
			if (usetag.Value == true & useOverlapSphere.Value == true & layerFilterOn.Value == true) {
			 
				List<Collider> tempColSphere = new List<Collider>();

				SphereTemp = Physics.OverlapSphere(go.transform.position, overlapSphereSize.Value);

					for (int i =0; i < SphereTemp.Length; i++)
					{

					if (SphereTemp[i].gameObject.tag == tag.Value){

						if (SphereTemp[i].gameObject.layer == layer){
							tempColSphere.Add(SphereTemp[i]);
						}
						
					}
				}
					colliders = new Collider[tempColSphere.Count];
					tempColSphere.CopyTo(colliders);
			}
//
			if (usetag.Value == false & useOverlapSphere.Value == true & layerFilterOn.Value == false) {

				SphereTemp = Physics.OverlapSphere(go.transform.position, overlapSphereSize.Value);
				colliders = new Collider[SphereTemp.Length-1];
				List<Collider> tempColSphere = new List<Collider>();

				for (int i =0; i < SphereTemp.Length; i++)
				{
					if(SphereTemp[i].gameObject != go && SphereTemp[i].gameObject.GetComponent<Rigidbody>() != null){
						tempColSphere.Add(SphereTemp[i]);
					}
				}

				colliders = new Collider[tempColSphere.Count];
				tempColSphere.CopyTo(colliders);

			}
//
			if (usetag.Value == false & useOverlapSphere.Value == true & layerFilterOn.Value == true) {
				
				SphereTemp = Physics.OverlapSphere(go.transform.position, overlapSphereSize.Value);
				List<Collider> tempColSphere = new List<Collider>();

				for (int i =0; i < SphereTemp.Length; i++)
				{
					if(SphereTemp[i].gameObject != go && SphereTemp[i].gameObject.GetComponent<Rigidbody>() != null){
						if (SphereTemp[i].gameObject.layer == layer){
							tempColSphere.Add(SphereTemp[i]);
						}
					}
				}


				colliders = new Collider[tempColSphere.Count];
				tempColSphere.CopyTo(colliders);

			}
//

			for (int i =0; i < colliders.Length; i++)
			{

				rb = colliders[i].GetComponent<Rigidbody>();
				m = rb.mass;
				float distance = Vector3.Distance(go.transform.position, colliders[i].gameObject.transform.position);
				direction = (go.transform.position - colliders[i].gameObject.transform.position).normalized;


				if (useSpeedVelocity.Value == true){

					rb.velocity = direction * Time.smoothDeltaTime * setSpeedVelocity.Value;
				}

				else{
					var typeFinale = ForceMode.Force;

					if (forceType == ForceType.Acceleration) {
						typeFinale = ForceMode.Acceleration;
					}

					Vector3 force = direction * (float)G * M * m / Mathf.Pow(distance, 2);

					if (useAddForceAtPosition.Value == true & onlyUseFx.Value == false){

					rb.AddForceAtPosition(force*forceMultiplier,colliders[i].gameObject.transform.position,typeFinale);
					

					}

					else if (onlyUseFx.Value == false){

						rb.AddForce(force*forceMultiplier,typeFinale);

					}

					if (useOrbit.Value == true){

						Vector3 finalDirection = orbitDirection();

						Vector3 offsetfinal = colliders[i].transform.position - go.transform.position;
						colliders[i].transform.position = go.transform.position + (offsetfinal.normalized * orbitOffset.Value); 
						colliders[i].transform.RotateAround (go.transform.position, finalDirection, orbitSpeed.Value * Time.smoothDeltaTime); 
					}

					if (useFx.Value == true){
						var forceSelect = selectForce();
						Vector3 directionFx = go.transform.position - colliders[i].transform.position;
						rb.AddForce(directionFx*fxSpeed.Value*(1/Vector3.Distance(colliders[i].transform.position,go.transform.position)), forceSelect);
					}

					if (useAddTorque.Value == true){
						rb.AddTorque(colliders[i].gameObject.transform.up * torqueAmount.Value * Time.deltaTime,typeFinale);
						rb.AddTorque(colliders[i].gameObject.transform.right * torqueAmount.Value * Time.deltaTime,typeFinale);
					}
				}
			}


		}

		Vector3 orbitDirection(){

			Vector3 temp = new Vector3 (0,0,0);

			switch(orbitType)
			{
			case OrbitType.normal: 
				temp = direction;
				break;
			case OrbitType.optionUp: 
				temp = Vector3.up;
				break;

			case OrbitType.optionForward: 
				temp = Vector3.forward;
				break;

			case OrbitType.optionDown: 
				temp = Vector3.down;
				break;

			case OrbitType.optionBack: 
				temp = Vector3.back;
				break;
		}

			return temp;
		}

		ForceMode selectForce(){
			
			var temp = ForceMode.Force;
			
			switch(fxType)
			{
			case FxType.normal: 
				temp = ForceMode.Force;
				break;
			case FxType.Acceleration: 
				temp = ForceMode.Acceleration;
				break;
				
			case FxType.Impulse: 
				temp = ForceMode.Impulse;
				break;
				
			case FxType.VelocityChange: 
				temp = ForceMode.VelocityChange;
				break;

			}
			
			return temp;
		}
	}
}

