// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// 


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Enables/Disables a 2D Collider(or a Rigidbody) in a single GameObject.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11580.0")]
    public class EnableCollider2D : FsmStateAction
	{
		[ActionSection("Setup")]
        [Tooltip("The GameObject that owns the Collider.")]
		[CheckForComponent(typeof(Collider2D))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Optionally drag a 2D Collider directly into this field (Script name will be ignored).")]
		[TitleAttribute("Collider")]
		public Collider2D component;

		[Tooltip("The name of the Collider to enable/disable.")]
		[TitleAttribute("or 2D Collider DropDown")]
		private FsmString script;
		public enum Selection {None, Box, Circle, Edge, Polygon, Rigidbody };
		[TitleAttribute("or 2D Collider Type Select")]
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
		[Tooltip("Set to True to enable/disable all 2D Collider in gameobject.")]
		public FsmBool allCollider;


		Collider2D componentTarget;

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
				script = "BoxCollider2D";
				DisableBoxCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Edge:
				script = "EdgeCollider2D";
				DisableEdgeCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Circle:
				script = "SphereCollider2D";
				DisableCircleCollider(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
				
			case Selection.Rigidbody:
				script = "Rigidbody2D";
				DisableRigidbody(Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			

			case Selection.Polygon:
				script = "PolygonCollider2D";
				DisablePolygonCollider(Fsm.GetOwnerDefaultTarget(gameObject));
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

				componentTarget = component as Collider2D;
				componentTarget.enabled = enable.Value;

			if (inclChildren.Value == true)
			{

				for(int i=0; i< go.transform.childCount; i++)
				{
					var child = go.transform.GetChild(i).gameObject;
					if(child != null){

					if (colliderSelect != Selection.None)
					{
					(child.gameObject.GetComponent(script.Value) as Collider2D).enabled = enable.Value;
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

			Collider2D[] scriptComponents = go.gameObject.GetComponents<Collider2D>();    
			foreach(Collider2D script in scriptComponents) {
			script.enabled = enable.Value;
			}

			if (inclChildren.Value == true)
			{
				Collider2D[] scriptChildComponents = go.gameObject.GetComponentsInChildren<Collider2D>();    
				foreach(Collider2D script in scriptChildComponents) {
					script.enabled = enable.Value;
				}
			}
		}

		void DisableBoxCollider(GameObject go)
		{
			
			Collider2D[] scriptComponents = go.gameObject.GetComponents<BoxCollider2D>();    
			foreach(BoxCollider2D temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider2D[] scriptChildComponents = go.gameObject.GetComponentsInChildren<BoxCollider2D>();    
				foreach(BoxCollider2D temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}



		void DisableEdgeCollider(GameObject go)
		{
			
			Collider2D[] scriptComponents = go.gameObject.GetComponents<EdgeCollider2D>();    
			foreach(EdgeCollider2D temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider2D[] scriptChildComponents = go.gameObject.GetComponentsInChildren<EdgeCollider2D>();    
				foreach(EdgeCollider2D temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableRigidbody(GameObject go)
		{
			
			Rigidbody2D[] scriptComponents = go.gameObject.GetComponents<Rigidbody2D>();    
			foreach(Rigidbody2D temp in scriptComponents) {
				temp.isKinematic = enable.Value;
			
			}
			
			if (inclChildren.Value == true)
			{
				Rigidbody2D[] scriptChildComponents = go.gameObject.GetComponentsInChildren<Rigidbody2D>();    
				foreach(Rigidbody2D temp in scriptChildComponents) {
					temp.isKinematic = enable.Value;

				}
			}
		}



		void DisablePolygonCollider(GameObject go)
		{
			
			Collider2D[] scriptComponents = go.gameObject.GetComponents<PolygonCollider2D>();    
			foreach(PolygonCollider2D temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider2D[] scriptChildComponents = go.gameObject.GetComponentsInChildren<PolygonCollider2D>();    
				foreach(PolygonCollider2D temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

		void DisableCircleCollider(GameObject go)
		{
			
			Collider2D[] scriptComponents = go.gameObject.GetComponents<CircleCollider2D>();    
			foreach(CircleCollider2D temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider2D[] scriptChildComponents = go.gameObject.GetComponentsInChildren<CircleCollider2D>();    
				foreach(CircleCollider2D temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
		}

	}
}

