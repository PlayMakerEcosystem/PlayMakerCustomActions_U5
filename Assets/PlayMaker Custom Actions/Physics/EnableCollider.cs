// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10242


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Enables/Disables a Collider(or a Rigidbody) in a single GameObject.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10242")]
    public class EnableCollider : FsmStateAction
	{
		[ActionSection("Setup")]
        [Tooltip("The GameObject that owns the Collider.")]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Optionally drag a Collider directly into this field (Script name will be ignored).")]
		[TitleAttribute("Collider")]
		public Collider component;

		[Tooltip("The name of the Collider to enable/disable.")]
		[TitleAttribute("or Collider DropDown")]
		private FsmString script;
		public enum Selection {None, Box, Sphere, Capsule, Mesh, Rigidbody, Wheel, Terrain };
		[TitleAttribute("or Collider Type Select")]
		public Selection colliderSelect;

		[ActionSection("Options")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
        [Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;
	
		[ActionSection("Collider Option")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable/disable all Collider in gameobject.")]
		public FsmBool allCollider;


		Collider componentTarget;

		public override void Reset()
		{
			gameObject = null;
			script = null;
			component = null;
			enable = true;
			allCollider = false;
			inclChildren = false;

		
		}


		public override void OnEnter()
		{

			var gos = Fsm.GetOwnerDefaultTarget(gameObject);
			if (gos == null)
			{
				Debug.LogWarning("missing gameObject: "+ gos.name);
				return;
			}

			if (allCollider.Value == false & component == null & colliderSelect == Selection.None)
			{
				Debug.LogWarning(gos.name + " !!! Check your setup ");
				return;
			}

			if (allCollider.Value == true){
				colliderSelect = Selection.None;
				DisableAll(Fsm.GetOwnerDefaultTarget(gameObject));
			}


			switch (colliderSelect)
			{
			case Selection.None:
				break;
			case Selection.Box:
				script = "BoxCollider";
				DisableBoxCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Capsule:
				script = "CapsuleCollider";
				DisableCapsuleCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Sphere:
				script = "SphereCollider";
				DisableSphereCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Rigidbody:
				script = "Rigidbody";
				DisableRigidbody(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Mesh:
				script = "MeshCollider";
				DisableMeshCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;

			case Selection.Wheel:
				script = "WheelCollider";
				DisableWheelCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;

			case Selection.Terrain:
				script = "TerrainCollider";
				DisableTerrainCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			}

			if (colliderSelect == Selection.None || component != null){


			if (allCollider.Value == false)
			DoEnableScript(Fsm.GetOwnerDefaultTarget(gameObject));

			}

			Finish();
		}

		void DoEnableScript(GameObject go)
		{
			colliderSelect = Selection.None;

			if (go == null)
			{
				return;
			}

				componentTarget = component as Collider;
				componentTarget.enabled = enable.Value;

			if (inclChildren.Value == true)
			{

				for(int i=0; i< go.transform.childCount; i++)
				{
					var child = go.transform.GetChild(i).gameObject;
					if(child != null){

					if (colliderSelect != Selection.None)
					{
					(child.gameObject.GetComponent(script.Value) as Collider).enabled = enable.Value;
					}

						else {
							Debug.LogWarning("Please select type for child filter !!!");
							return;
						}
				
				}
				}

			}
			return;
		}

		void DisableAll(GameObject go)
		{

			Collider[] scriptComponents = go.gameObject.GetComponents<Collider>();    
			foreach(Collider script in scriptComponents) {
			script.enabled = enable.Value;
			}

			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<Collider>();    
				foreach(Collider script in scriptChildComponents) {
					script.enabled = enable.Value;
				}
			}
		}

		void DisableBoxCollider(GameObject go)
		{
			
			Collider[] scriptComponents = go.gameObject.GetComponents<BoxCollider>();    
			foreach(BoxCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<BoxCollider>();    
				foreach(BoxCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableCapsuleCollider(GameObject go)
		{
			
			Collider[] scriptComponents = go.gameObject.GetComponents<CapsuleCollider>();    
			foreach(CapsuleCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<CapsuleCollider>();    
				foreach(CapsuleCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableSphereCollider(GameObject go)
		{
			
			Collider[] scriptComponents = go.gameObject.GetComponents<SphereCollider>();    
			foreach(SphereCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<SphereCollider>();    
				foreach(SphereCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableRigidbody(GameObject go)
		{
			
			Rigidbody[] scriptComponents = go.gameObject.GetComponents<Rigidbody>();    
			foreach(Rigidbody temp in scriptComponents) {
				temp.isKinematic = !enable.Value;
				temp.detectCollisions = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Rigidbody[] scriptChildComponents = go.gameObject.GetComponentsInChildren<Rigidbody>();    
				foreach(Rigidbody temp in scriptChildComponents) {
					temp.isKinematic = !enable.Value;
					temp.detectCollisions = enable.Value;
				}
			}
		}

		void DisableMeshCollider(GameObject go)
		{
			
			Collider[] scriptComponents = go.gameObject.GetComponents<MeshCollider>();    
			foreach(MeshCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<MeshCollider>();    
				foreach(MeshCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableWheelCollider(GameObject go)
		{
			
			Collider[] scriptComponents = go.gameObject.GetComponents<WheelCollider>();    
			foreach(WheelCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<WheelCollider>();    
				foreach(WheelCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableTerrainCollider(GameObject go)
		{
			
			Collider[] scriptComponents = go.gameObject.GetComponents<TerrainCollider>();    
			foreach(TerrainCollider temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider[] scriptChildComponents = go.gameObject.GetComponentsInChildren<TerrainCollider>();    
				foreach(TerrainCollider temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

	}
}
