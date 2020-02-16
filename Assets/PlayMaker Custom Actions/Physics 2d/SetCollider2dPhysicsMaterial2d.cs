// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Original action by Thore : https://hutonggames.com/playmakerforum/index.php?topic=21066.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Set the physicsMaterial2d of a Capsule Collider 2D")]
    public class SetCollider2dPhysicsMaterial2d : ComponentAction<CapsuleCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(CapsuleCollider2D))]
        [Tooltip("The GameObject with the Capsule Collider 2D.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The physicsMaterial2d")]
        public PhysicsMaterial2D material;
        
        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;

            material = null;
            
            
            everyFrame = false;
        }

        public override void OnPreprocess()
        {
            if (everyFrame)
            {
                Fsm.HandleFixedUpdate = true;
            }
        }

        public override void OnEnter()
        {
            Execute();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            Execute();
        }

        void Execute()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                return;
            }

            if (!material == this.cachedComponent.sharedMaterial)
            {
                this.cachedComponent.sharedMaterial = material;
            }
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            string howOften;
            if (everyFrame) howOften = " /updated"; else howOften = " /once";

            return ActionHelpers.AutoName(this) + howOften;
        }
#endif
    }
}
