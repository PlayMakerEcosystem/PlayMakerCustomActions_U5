// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Get the simulated property of the Rigidbody2D component")]
    public class GetRigidBody2dIsSimulated : ComponentAction<Rigidbody2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(Rigidbody2D))]
        public FsmOwnerDefault gameObject;

        [Tooltip("The simulated value of the Rigidbody2D")]
        [UIHint(UIHint.Variable)]
        public FsmBool simulated;

        [Tooltip("Get value Every frame")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            simulated = null;
        }

        public override void OnEnter()
        {
            ExecuteAction();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            ExecuteAction();
        }

        void ExecuteAction()
        {
            if (this.UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                simulated.Value = this.cachedComponent.simulated;
            }
        }
    }
}