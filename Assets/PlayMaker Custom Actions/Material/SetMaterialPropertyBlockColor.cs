// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named color value in a game object's material PropertyBlock. ")]
	public class SetMaterialPropertyBlockColor : ComponentAction<Renderer>
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("A named color parameter in the shader. Must have [PerRendererData] attribute")]
		public FsmString propertyName;
		
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmColor color;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		MaterialPropertyBlock _propBlock;

		public override void Reset()
		{
			gameObject = null;
			propertyName = "_Color";
			color = Color.black;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_propBlock = new MaterialPropertyBlock();

			DoSetMaterialColor();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetMaterialColor();
		}

		void DoSetMaterialColor()
		{
			if (color.IsNone)
			{
				return;
			}

			var colorName = propertyName.Value;
			if (colorName == "") colorName = "_Color";
			
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
			_propBlock.SetColor(colorName, color.Value);
			renderer.SetPropertyBlock(_propBlock);
		}
	}
}