// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Get Box Collider2D size")]
    [HelpUrl("")]
    public class GetBox2dColliderSize : ComponentAction<BoxCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(BoxCollider2D))]
        [Tooltip("Targeted BoxCollider2d GameObject")]
        public FsmOwnerDefault gameObject;

        [Tooltip("BoxCollider2d size.")]
        [UIHint(UIHint.Variable)]
        public FsmVector2 size;
        
        [Tooltip("BoxCollider2d size X.")]
        [UIHint(UIHint.Variable)]
	    public FsmFloat sizeX;
        
        [Tooltip("BoxCollider2d size y.")]
        [UIHint(UIHint.Variable)]
	    public FsmFloat sizeY;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            size = null;
            sizeX = null;
            sizeY = null;
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