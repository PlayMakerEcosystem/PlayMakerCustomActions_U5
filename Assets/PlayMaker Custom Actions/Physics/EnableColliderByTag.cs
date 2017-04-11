// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10242

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Enables/Disables a Collider(or a Rigidbody) by Tag (and Layer).")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10242")]
    public class EnableColliderByTag : FsmStateAction
	{
		[ActionSection("Setup")]
		[TitleAttribute("Collider Type Select")]
		public Selection colliderSelect;
		public enum Selection {None, Box, Sphere, Capsule, Mesh, Rigidbody, Wheel, Terrain };
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable/disable all Collider.")]
		[TitleAttribute("or All Collider")]
		public FsmBool allCollider;

		[ActionSection("Options")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;

		[ActionSection("Tag and Layer Options")]
		[Tooltip("Activate this option?")]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		[TitleAttribute("Incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;
		[Tooltip("Filter layer on child?")]
		public FsmBool inclLayerFilterOnChild;
		[UIHint(UIHint.Layer)]
		public int layer;



		Collider componentTarget;

		public override void Reset()
		{
		
			enable = true;
			allCollider = false;
			inclChildren = false;
			layerFilterOn = false;
			layer = 0;
			inclLayerFilterOnChild = false;
			
		}


		public override void OnEnter()
		{


			if (allCollider.Value == false & colliderSelect == Selection.None)
			{
				Debug.LogWarning(" !!! Check your setup - Collider Type Select = None");
				return;
			}

			if (allCollider.Value == true & layerFilterOn.Value == false){
				colliderSelect = Selection.None;
				DisableAllTag();
			}

			if (allCollider.Value == true & layerFilterOn.Value == true){
				colliderSelect = Selection.None;
				DisableAllTagFilter();
			}


			switch (colliderSelect)
			{
			case Selection.None:
				break;
			case Selection.Box:
				DisableBoxCollider();
				break;
				
			case Selection.Capsule:
				DisableCapsuleCollider();
				break;
				
			case Selection.Sphere:
				DisableSphereCollider();
				break;
				
			case Selection.Rigidbody:
				DisableRigidbody();
				break;
				
			case Selection.Mesh:
				DisableMeshCollider();
				break;

			case Selection.Wheel:
				DisableWheelCollider();
				break;

			case Selection.Terrain:
				DisableTerrainCollider();
				break;
			
			}
			
			Finish();
		}


		void DisableAllTagFilter()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);

			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){

				if (objtag[i].layer == layer){


			Collider[] scriptComponents = objtag[i].gameObject.GetComponents<Collider>();    
			foreach(Collider script in scriptComponents) {
			
			script.enabled = enable.Value;
			}

			if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
			{
				Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Collider>();    
				foreach(Collider script in scriptChildComponents) {
					script.enabled = enable.Value;
				}
			}

			if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
			{
						  
						foreach(Collider script in objtag[i].gameObject.GetComponentsInChildren<Collider>()) {
							if (script.gameObject.layer == layer)
							script.enabled = enable.Value;
						}

			}

			}
			}
		}

		void DisableAllTag()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){
				
				Collider[] scriptComponents = objtag[i].gameObject.GetComponents<Collider>();    
				foreach(Collider script in scriptComponents) {
					
					script.enabled = enable.Value;
				}
				
				if (inclChildren.Value == true)
				{
					Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Collider>();    
					foreach(Collider script in scriptChildComponents) {
						script.enabled = enable.Value;
					}
				}
			}
		}

		void DisableBoxCollider()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}

			if (inclLayerFilterOnChild.Value == false)
			{

			for(int i = 0; i<objtag.Length;i++){

			Collider[] scriptComponents = objtag[i].gameObject.GetComponents<BoxCollider>();    
			foreach(BoxCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<BoxCollider>();    
				foreach(BoxCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
			}
				return;
			}

			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){

					if (objtag[i].layer == layer){
					
					Collider[] scriptComponents = objtag[i].gameObject.GetComponents<BoxCollider>();    
					foreach(BoxCollider temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<BoxCollider>();    
							foreach(BoxCollider script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(BoxCollider script in objtag[i].gameObject.GetComponentsInChildren<BoxCollider>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
				}
				}
				return;
			}


		}

		void DisableCapsuleCollider()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider[] scriptComponents = objtag[i].gameObject.GetComponents<CapsuleCollider>();    
					foreach(CapsuleCollider temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<CapsuleCollider>();    
						foreach(CapsuleCollider temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider[] scriptComponents = objtag[i].gameObject.GetComponents<CapsuleCollider>();    
						foreach(CapsuleCollider temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<CapsuleCollider>();    
							foreach(CapsuleCollider script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(CapsuleCollider script in objtag[i].gameObject.GetComponentsInChildren<CapsuleCollider>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

		void DisableSphereCollider()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider[] scriptComponents = objtag[i].gameObject.GetComponents<SphereCollider>();    
					foreach(SphereCollider temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<SphereCollider>();    
						foreach(SphereCollider temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider[] scriptComponents = objtag[i].gameObject.GetComponents<SphereCollider>();    
						foreach(SphereCollider temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<SphereCollider>();    
							foreach(SphereCollider script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(SphereCollider script in objtag[i].gameObject.GetComponentsInChildren<SphereCollider>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

		void DisableRigidbody()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Rigidbody[] scriptComponents = objtag[i].gameObject.GetComponents<Rigidbody>();    
					foreach(Rigidbody temp in scriptComponents) {
						temp.isKinematic = !enable.Value;
						temp.detectCollisions = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Rigidbody[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Rigidbody>();    
						foreach(Rigidbody temp in scriptChildComponents) {
							temp.isKinematic = !enable.Value;
							temp.detectCollisions = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Rigidbody[] scriptComponents = objtag[i].gameObject.GetComponents<Rigidbody>();    
						foreach(Rigidbody temp in scriptComponents) {
							temp.isKinematic = !enable.Value;
							temp.detectCollisions = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Rigidbody[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Rigidbody>();    
							foreach(Rigidbody temp in scriptChildComponents) {
								temp.isKinematic = !enable.Value;
								temp.detectCollisions = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(Rigidbody temp in objtag[i].gameObject.GetComponentsInChildren<Rigidbody>()) {
								if (temp.gameObject.layer == layer){
									temp.isKinematic = !enable.Value;
									temp.detectCollisions = enable.Value;
								}
							}
							
						}
					}
				}
				return;
			}
			
			
		}
		void DisableMeshCollider()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider[] scriptComponents = objtag[i].gameObject.GetComponents<MeshCollider>();    
					foreach(MeshCollider temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<MeshCollider>();    
						foreach(MeshCollider temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider[] scriptComponents = objtag[i].gameObject.GetComponents<MeshCollider>();    
						foreach(MeshCollider temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<MeshCollider>();    
							foreach(MeshCollider script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(MeshCollider script in objtag[i].gameObject.GetComponentsInChildren<MeshCollider>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

		void DisableWheelCollider()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider[] scriptComponents = objtag[i].gameObject.GetComponents<WheelCollider>();    
					foreach(WheelCollider temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<WheelCollider>();    
						foreach(WheelCollider temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider[] scriptComponents = objtag[i].gameObject.GetComponents<WheelCollider>();    
						foreach(WheelCollider temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<WheelCollider>();    
							foreach(WheelCollider script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(WheelCollider script in objtag[i].gameObject.GetComponentsInChildren<WheelCollider>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

		void DisableTerrainCollider()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider[] scriptComponents = objtag[i].gameObject.GetComponents<TerrainCollider>();    
					foreach(TerrainCollider temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<TerrainCollider>();    
						foreach(TerrainCollider temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider[] scriptComponents = objtag[i].gameObject.GetComponents<TerrainCollider>();    
						foreach(TerrainCollider temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<TerrainCollider>();    
							foreach(TerrainCollider script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(TerrainCollider script in objtag[i].gameObject.GetComponentsInChildren<TerrainCollider>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

	}
}
