// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Set Box Collider2D Size")]
    [HelpUrl("")]
    public class SetBox2dColliderSize : ComponentAction<BoxCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(BoxCollider2D))]
        [Tooltip("Targeted BoxCollider2d GameObject")]
        public FsmOwnerDefault gameObject;

        [Tooltip("BoxCollider2d size. Leave to none for no effect")]
        public FsmVector2 size;
        
        [Tooltip("BoxCollider2d size X. Leave to none for no effect, else overrides the x value")]
	    public FsmFloat sizeX;
        
        [Tooltip("BoxCollider2d size y. Leave to none for no effect, else overrides the y value")]
	    public FsmFloat sizeY;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;
        
        private Vector2 _size;
        
        public override void Reset()
        {
            gameObject = null;
            size = null;
            sizeX = new FsmFloat{UseVariable = true};
            sizeY = new FsmFloat{UseVariable = true};
            everyFrame = false;
        }
        
        public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }


        public override void OnEnter()
        {
            ExecuteAction();
            
            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            ExecuteAction();
        }

        void ExecuteAction()
        {
            
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                return;
            }

            _size = this.cachedComponent.offset;
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
