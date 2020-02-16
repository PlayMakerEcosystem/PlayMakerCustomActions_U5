// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Flip a Sprite. Object must have a Sprite Renderer.")]
	public class SpriteSetFlip : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The SpriteRenderer to control.")]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Flip X value")]
		public FsmBool flipX;

		[Tooltip("The Flip Y value")]
		public FsmBool flipY;

		[Tooltip("Reset flip values when state exits")]
		public FsmBool resetOnExit;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		SpriteRenderer spriteRenderer;
		bool flipX_orig;
		bool flipY_orig;


		public override void Reset()
		{
			gameObject = null;
			flipX = null;
			flipY = null;
			resetOnExit = false;

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

			if (resetOnExit.Value)
			{
				flipX_orig = spriteRenderer.flipX;
				flipY_orig = spriteRenderer.flipY;
			}

			FlipSprites();

			if (!everyFrame) 
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			if (this.Enabled)
			{
				
				FlipSprites ();
			}
		}


		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
				spriteRenderer.flipX = flipX_orig;
				spriteRenderer.flipY = flipY_orig;
			}
		}
		void FlipSprites()
		{
			if (spriteRenderer.flipX != flipX.Value) spriteRenderer.flipX = flipX.Value;
			if (spriteRenderer.flipY != flipY.Value) spriteRenderer.flipY = flipY.Value;
		}



	}
}
