// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Renderer)]
	[Tooltip("Gets the image sprite of a SpriteRenderer component.")]
	public class SpriteRendererGetSprite : ComponentAction<SpriteRenderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		[Tooltip("The GameObject with the SpriteRenderer component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The source sprite of the SpriteRenderer component.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Sprite))]
		public FsmObject sprite;

		private SpriteRenderer sp;

		public override void Reset()
		{
			gameObject = null;
			sprite = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				sp = cachedComponent;
			}
			
			DoSetImageSourceValue();

			Finish();
		}

	    private void DoSetImageSourceValue()
		{
			if (sp!=null)
			{
				sprite.Value = sp.sprite;
			}
		}
	}
}