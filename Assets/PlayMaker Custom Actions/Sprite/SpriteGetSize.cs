// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Gets a Sprite size. Object must have a Sprite Renderer.")]
	public class SpriteGetSize : ComponentAction<SpriteRenderer>
	{

		[RequiredField]
		[Tooltip("The SpriteRenderer to control.")]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;

        [Tooltip("The size")]
        [UIHint(UIHint.Variable)]
        public FsmVector2 size;

		[Tooltip("The size X value")]
        [UIHint(UIHint.Variable)]
        public FsmFloat sizeX;

		[Tooltip("The size Y value")]
        [UIHint(UIHint.Variable)]
        public FsmFloat sizeY;


		[Tooltip("Repeat every frame.")]
		public bool everyFrame;


		public override void Reset()
		{
			gameObject = null;
            sizeX = null;
            sizeY = null;
            size = null;

		}

		public override void OnEnter()
		{
            Execute();

            
			if (!everyFrame) 
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
		
                Execute();
			
		}

		void Execute()
		{
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                return;
            }


            if (!size.IsNone)
            {
                size.Value = this.cachedComponent.size;
            }

            if (!sizeX.IsNone)
            {
                sizeX.Value = this.cachedComponent.size.x;
            }

            if (!sizeY.IsNone)
            {
                sizeY.Value = this.cachedComponent.size.y;
            }

		}



	}
}
