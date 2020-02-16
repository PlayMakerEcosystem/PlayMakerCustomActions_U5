// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets a named float value in a game object's material PropertyBlock. ")]
	public class GetMaterialPropertyBlockFloat : ComponentAction<Renderer>
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("A named float parameter in the shader. Must have [PerRendererData] attribute")]
		public FsmString propertyName;
		
		[RequiredField]
		[Tooltip("Get the parameter value.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatValue;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		MaterialPropertyBlock _propBlock;

		public override void Reset()
		{
			gameObject = null;
			propertyName = "_Glossiness";
			floatValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_propBlock = new MaterialPropertyBlock();

			DoGetMaterialFloat();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetMaterialFloat();
		}

		void DoGetMaterialFloat()
		{

			string _propertyName = propertyName.Value;
			if (_propertyName == "") _propertyName = "_Glossiness";
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (!UpdateCache(go))
		    {
		        return;
		    }

			if (renderer.material == null)
			{
				LogError("Missing Material!");
				return;
			}		

			renderer.GetPropertyBlock(_propBlock);
			floatValue.Value = _propBlock.GetFloat(_propertyName);

		}
	}
}