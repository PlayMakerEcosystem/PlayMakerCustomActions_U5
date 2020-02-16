// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Get Box Collider2D Offset")]
    [HelpUrl("")]
    public class GetBox2dColliderOffset : ComponentAction<BoxCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(BoxCollider2D))]
        [Tooltip("Targeted BoxCollider2d GameObject")]
        public FsmOwnerDefault gameObject;

        [Tooltip("BoxCollider2d offset.")]
        [UIHint(UIHint.Variable)]
        public FsmVector2 offset;
        
        [Tooltip("BoxCollider2d offset X.")]
        [UIHint(UIHint.Variable)]
	    public FsmFloat offsetX;
        
        [Tooltip("BoxCollider2d offset y.")]
        [UIHint(UIHint.Variable)]
	    public FsmFloat offsetY;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            offset = null;
            offsetX = null;
            offsetY = null;
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

            if (!offset.IsNone)
            {
                offset.Value = this.cachedComponent.offset;
            }
            
            if (!offsetX.IsNone)
            {
                offsetX.Value = this.cachedComponent.offset.x;
            }
            
            if (!offsetY.IsNone)
            {
                offsetY.Value = this.cachedComponent.offset.y;
            }
        }
    }
}