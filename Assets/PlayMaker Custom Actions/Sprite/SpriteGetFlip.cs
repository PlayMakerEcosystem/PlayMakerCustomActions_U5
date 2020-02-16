// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Gets the Flips values of a Sprite. Object must have a Sprite Renderer.")]
	public class SpriteGetFlip : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The SpriteRenderer to control.")]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The x flip value")]
		[UIHint(UIHint.Variable)]
		public FsmBool flipX;

		[Tooltip("The y flip value")]
		[UIHint(UIHint.Variable)]
		public FsmBool flipY;


		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		SpriteRenderer spriteRenderer;


		public override void Reset()
		{
			gameObject = null;
			flipX = null;
			flipY = null;

		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			spriteRenderer = go.GetComponent<SpriteRenderer>();

			if (spriteRenderer == null)
			{
				LogError("SpriteFlip: Missing SpriteRenderer!");
				Finish();
				return;
			}

			GetFlipSprites();

			if (!everyFrame) 
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			GetFlipSprites ();
		}

		void GetFlipSprites()
		{
			flipX.Value = spriteRenderer.flipX;
			flipY.Value = spriteRenderer.flipY;
		}
	}
}
