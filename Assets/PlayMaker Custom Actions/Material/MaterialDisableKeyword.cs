// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/*--- Keywords: Shader Keyword parameter --- */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a disableKeyword property of a material.")]
	public class MaterialDisableKeyword : ComponentAction<Renderer>
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[RequiredField]
		[Tooltip("keyword to disable.")]
		public FsmString keyword;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			keyword = null;
			fail = null;
		}

		public override void OnEnter()
		{
			bool ok = DoSetMaterialProperty();

			if (!ok) Fsm.Event(fail);

			Finish();
		}

		bool DoSetMaterialProperty()
		{

			if (material.Value != null)
			{
				material.Value.DisableKeyword(keyword.Value);
				return true;
			}
			
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				LogError("Missing Renderer!");
				return false;
			}
			
			if (renderer.material == null)
			{
				LogError("Missing Material!");
				return false;
			}
			
			if (materialIndex.Value == 0)
			{
				renderer.material.DisableKeyword(keyword.Value);
				return true;
			}
			
			if (renderer.materials.Length > materialIndex.Value)
			{
				var materials = renderer.materials;
				materials[materialIndex.Value].DisableKeyword(keyword.Value);
				renderer.materials = materials;	
				return true;
			}	

			return false;
		}
	}
}
