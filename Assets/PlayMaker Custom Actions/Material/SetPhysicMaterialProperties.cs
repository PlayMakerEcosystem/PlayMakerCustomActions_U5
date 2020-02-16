// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the various properties of a Physics Material.")]
	public class SetPhysicMaterialProperties : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to. Requires a Collider components")]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault gameObject;

		[Tooltip("alternativly, can set directly, without a reference of the gameobject. Leave to null if targeting the gameobject")]
		public PhysicMaterial physicsMaterial;

		[Tooltip("Set the dynamicFriction value")]
		public FsmFloat dynamicFriction;

		[Tooltip("Set the staticFriction value")]
		public FsmFloat staticFriction;

		[Tooltip("Set the bounciness value")]
		public FsmFloat bounciness;

		[Tooltip("Set the frictionCombine value")]
		[ObjectType(typeof(PhysicMaterialCombine))]
		public FsmEnum frictionCombine;

		[Tooltip("Set the bounceCombine value")]
		[ObjectType(typeof(PhysicMaterialCombine))]
		public FsmEnum bounceCombine;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		GameObject _go;
		Collider _col;
		PhysicMaterial _mat;

		public override void Reset()
		{
			gameObject = null;
			physicsMaterial = null;

			dynamicFriction = new FsmFloat (){UseVariable=true};
			staticFriction = new FsmFloat (){UseVariable=true};
			bounciness = new FsmFloat (){UseVariable=true};
			frictionCombine = new FsmEnum (){UseVariable=true};
			bounceCombine = new FsmEnum (){UseVariable=true};

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoSetMaterialValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetMaterialValue();
		}
		
		void DoSetMaterialValue()
		{
			if (physicsMaterial != null) {
				_mat = physicsMaterial;
			} else {
			
				_go = Fsm.GetOwnerDefaultTarget (gameObject);
				if (_go == null) {

					return;
				}

				_col = _go.GetComponent<Collider> ();
				if (_col == null) {
					return;
				}
				_mat = 	_col.material;
			}
				
			if (!bounceCombine.IsNone)
			{
				_mat.bounceCombine = (PhysicMaterialCombine)bounceCombine.Value;
			}

			if(!bounciness.IsNone)
			{
				_mat.bounciness = bounciness.Value;
			}

			if (!dynamicFriction.IsNone)
			{
				_mat.dynamicFriction = dynamicFriction.Value;
			}
			if (!frictionCombine.IsNone)
			{
				_mat.frictionCombine = (PhysicMaterialCombine) frictionCombine.Value ;
			}
			if (!staticFriction.IsNone)
			{
				_mat.staticFriction = staticFriction.Value;
			}
		}
	}
}