// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: NPC fov

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("FOV")]
	[Tooltip("Visually create a FOV mesh and set bool if target is inside 2D gameobject FOV.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11753.0")]

	public class DynamicVisionConeFov2D : FsmStateAction 
	
	{
		[CheckForComponent(typeof(MeshFilter))]
		[CheckForComponent(typeof(MeshRenderer))]
		public FsmOwnerDefault gameObject;

		[ActionSection("Setup")]
		public FsmInt quality;
		public FsmInt fovAngle;
		public FsmFloat fovMaxDistance;


		[ActionSection("General Filter")]
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers. 0 is off")]
		public FsmInt[] layerMask;
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		public FsmBool useTag;
		[Tooltip("Filter by tag. First mask then tag.")]
		[UIHint(UIHint.Tag)]
		public FsmString filterTag;
	

		[ActionSection("status")]
		[Tooltip("Set the status by int. This int = material index. The status int is linked to the amount of materials used. Change the int in Game Mode to change the material. For example: 0 = Idle, 2 = Suspicious 4 = Alert. etc... ")]
		public FsmInt status;
		[Tooltip("Set the material you will use")]
		public FsmMaterial[] material;


		[ActionSection("Optional Target Setup")]
		[Tooltip("Your agent - player or npc - If empty tag filter will be active")]
		public FsmGameObject target;
		[Tooltip("Your agent - player or npc by tag if target object is empty")]
		[UIHint(UIHint.Tag)]
		[TitleAttribute("or by Tag")]
		public FsmString tag;
	
		[ActionSection("Output")]
		[Tooltip("Will set bool to true or false")]
		public FsmBool insideFov;


		[ActionSection("Update Setup")]
		[Tooltip("When to cast the ray")]
		public updateType updateTypeSelect;
		public enum updateType
		{
			Update,
			FixedUpdate,
			LateUpdate
		};

		public FsmBool forceFinish;

		[ActionSection("Debug")]
		public FsmBool raysGizmosEnabled;
		[Tooltip("The color to use for the draw debug.")]
		public FsmColor debugColor;


		//---

		public List<RaycastHit2D> hits2D = new List<RaycastHit2D>();
		private GameObject father;
		private GameObject go;
		private int sizeOfList;

		//---

		int numRays;
		float currentAngle;
		Vector3 direction;
		RaycastHit2D hit;

		//---

		Vector3[] newVertices;
		Vector2[] newUV;
		int[] newTriangles;
		Mesh mesh;
		MeshRenderer meshRenderer;
		int i;
		int v;

		//---

		public override void Reset()
		{
			quality = 4;
			fovAngle = 90;
			fovMaxDistance = 15f;
			debugColor = Color.yellow;
			updateTypeSelect = updateType.Update;
			filterTag = null;
			status = 0;
			layerMask = new FsmInt[0];
			invertMask =false;
			useTag = false;
			forceFinish = false;
		}
        
        public override void OnPreprocess()
		{
			if (updateTypeSelect == updateType.FixedUpdate )
			{
			Fsm.HandleFixedUpdate = true;
			}

			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateTypeSelect == updateType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}


		public override void OnEnter()
		{


			father = Fsm.GetOwnerDefaultTarget(gameObject);
	
			go = new GameObject("FOV");
			go.transform.position = father.transform.position ;
			go.transform.parent = father.transform;

			go.AddComponent<MeshFilter>();
			go.AddComponent<MeshRenderer>();

			mesh = go.GetComponent<MeshFilter>().mesh;
			meshRenderer = go.GetComponent<MeshRenderer>();

            meshRenderer.receiveShadows = false;
			meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
			meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            
            
			meshRenderer.material = material[status.Value].Value;

		}



		public override void OnUpdate(){
		

			if (updateTypeSelect == updateType.Update )
			{


				CastRays();
					

			}
		}

		public override void OnFixedUpdate(){
			
			
			if (updateTypeSelect == updateType.FixedUpdate )
			{
				
				
				CastRays();
			}
			
		}

		public override void OnLateUpdate(){


			if (updateTypeSelect == updateType.LateUpdate )
			{
				CastRays();
			}

			UpdateMesh();
			
			UpdateMeshMaterial();

		}

		void CastRays()
		{
			numRays = fovAngle.Value * quality.Value;
			currentAngle = fovAngle.Value / -2;


			hits2D.Clear();
			
			for (int i = 0; i < numRays; i++)
			{
				direction = Quaternion.AngleAxis(currentAngle, go.gameObject.transform.forward) * go.gameObject.transform.up;
			

				if (useTag.Value == false){

					hit = Physics2D.Raycast(go.gameObject.transform.position, direction, fovMaxDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
				
					if(hit == false)
					{
					hit.point = go.gameObject.transform.position + (direction * fovMaxDistance.Value);
				}
				}

				else {
					
					hit = Physics2D.Raycast(go.gameObject.transform.position, direction, fovMaxDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
					
						if(hit == false)
					
					{

						if (hit.transform.tag == filterTag.Value){
						hit.point = go.gameObject.transform.position + (direction * fovMaxDistance.Value);
						}
					}
				}


				hits2D.Add(hit);
				
				currentAngle += 1f / quality.Value;
			}

			Output();
		}

		void UpdateMesh()
		{
			
			if (hits2D == null || hits2D.Count == 0)
				return;
			
			if (mesh.vertices.Length != hits2D.Count + 1)
			{
				mesh.Clear();
				newVertices = new Vector3[hits2D.Count + 1];
				newTriangles = new int[(hits2D.Count - 1) * 3];
				
				i = 0;
				v = 1;
				while (i < newTriangles.Length)
				{
					if ((i % 3) == 0)
					{
						newTriangles[i] = 0;
						newTriangles[i + 1] = v;
						newTriangles[i + 2] = v + 1;
						v++;
					}
					i++;
				}
			}
			
			newVertices[0] = Vector3.zero;
			for (i = 1; i <= hits2D.Count; i++)
			{
				newVertices[i] = go.gameObject.transform.InverseTransformPoint(hits2D[i-1].point);
			}
			
			newUV = new Vector2[newVertices.Length];
			i = 0;
			while (i < newUV.Length) {
				newUV[i] = new Vector2(newVertices[i].x, newVertices[i].z);
				i++;
			}
			
			mesh.vertices = newVertices;
			mesh.triangles = newTriangles;
			mesh.uv = newUV;
			
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
		}
		
		void UpdateMeshMaterial()
		{	


			for (i = 0; i < material.Length; i++)
			{
				if (i == status.Value && meshRenderer.material != material[i].Value)
				{
					meshRenderer.material = material[i].Value;
				}
			}

			if (forceFinish.Value == true){
				meshRenderer.enabled = false;
				Finish();
			}
		}

		void Output()
		{

			insideFov.Value = false;
		
			if (tag.IsNone || tag.Value == null || tag.Value == "Untagged" || tag.Value.Length <= 0){


				if (target.Value == null || target.IsNone)
				{
					return;
				}

			foreach (RaycastHit2D hit in hits2D)
			{
				if (hit.transform == target.Value.transform)
				{
					insideFov.Value = true;
				}
			}

			}

			else {

				foreach (RaycastHit2D hit in hits2D)
				{
					if (hit.transform && hit.transform.tag == tag.Value)
					{
						insideFov.Value = true;
					}
				}

			}

			return;
		}
		
		public void OnDrawGizmos()
		{
			if (raysGizmosEnabled.Value == true){
			Gizmos.color = debugColor.Value;

			sizeOfList = hits2D.Count;

			if (raysGizmosEnabled.Value == true && sizeOfList > 0) 
			{
					foreach (RaycastHit2D hit in hits2D)
				{
					Gizmos.DrawSphere(hit.point, 0.04f);
					Gizmos.DrawLine(go.gameObject.transform.position, hit.point);
				}
			}
			}
		}

	
	}
}
