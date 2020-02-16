// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Set Box Collider2D Offset")]
    [HelpUrl("")]
    public class SetBox2dColliderOffset : ComponentAction<BoxCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(BoxCollider2D))]
        [Tooltip("Targeted BoxCollider2d GameObject")]
        public FsmOwnerDefault gameObject;

        [Tooltip("BoxCollider2d offset. Leave to none for no effect")]
        public FsmVector2 offset;
        
        [Tooltip("BoxCollider2d offset X. Leave to none for no effect, else overrides the x value")]
	    public FsmFloat offsetX;
        
        [Tooltip("BoxCollider2d offset y. Leave to none for no effect, else overrides the y value")]
	    public FsmFloat offsetY;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;


        private Vector2 _offset;
        
        public override void Reset()
        {
            gameObject = null;
            offset = null;
            offsetX = new FsmFloat{UseVariable = true};
            offsetY = new FsmFloat{UseVariable = true};
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

            _offset = this.cachedComponent.offset;
            if (!offset.IsNone)
            {
                _offset = offset.Value;
            }
            
            if (!offsetX.IsNone)
            {
                _offset.x = offsetX.Value;
            }
            
            if (!offsetY.IsNone)
            {
                _offset.y = offsetY.Value;
            }

            this.cachedComponent.offset = _offset;

        }
    }
}
