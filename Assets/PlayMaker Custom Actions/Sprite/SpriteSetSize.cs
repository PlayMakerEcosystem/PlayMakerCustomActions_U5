// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Sets a Sprite size. Object must have a Sprite Renderer.")]
	public class SpriteSetSize : ComponentAction<SpriteRenderer>
	{

		[RequiredField]
		[Tooltip("The SpriteRenderer to control.")]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;

        [Tooltip("The size")]
        public FsmVector2 size;

		[Tooltip("The size X value")]
		public FsmFloat sizeX;

		[Tooltip("The size Y value")]
		public FsmFloat sizeY;

		[Tooltip("Reset flip values when state exits")]
		public FsmBool resetOnExit;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;


		Vector2 size_orig;

        Vector2 _size = new Vector2();
		public override void Reset()
		{
			gameObject = null;
			sizeX = new FsmFloat() { UseVariable = true };
			sizeY = new FsmFloat() { UseVariable = true };
            size = null;
			resetOnExit = false;

		}

		public override void OnEnter()
		{
            if (UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {

                if (resetOnExit.Value)
                {
                    size_orig = this.cachedComponent.size;
                }

                Execute();

            }
			if (!everyFrame) 
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
		
                Execute();
			
		}


		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
                this.cachedComponent.size = size_orig;
			}
		}


		void Execute()
		{
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                return;
            }

            _size = this.cachedComponent.size;

            if (!size.IsNone)
            {
                _size = size.Value;
            }

            if (!sizeX.IsNone)
            {
                _size.x = sizeX.Value;
            }

            if (!sizeY.IsNone)
            {
                _size.y = sizeY.Value;
            }

            this.cachedComponent.size = _size;
            
		}



	}
}
