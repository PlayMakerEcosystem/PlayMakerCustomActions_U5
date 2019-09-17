// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Original action by Thore : https://hutonggames.com/playmakerforum/index.php?topic=21066.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Set the properties of a Capsule Collider 2D, Leave properties to none for no effect")]
    public class SetCapsuleCollider2dProperties : ComponentAction<CapsuleCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(CapsuleCollider2D))]
        [Tooltip("The GameObject with the Capsule Collider 2D.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The density of the collider used to calculate its mass (when auto mass is enabled).")]
        public FsmFloat density;
        
        [Tooltip("Is this collider configured as a trigger?")]
        public FsmBool isTrigger;
        
        [Tooltip("Whether the collider is used by an attached effector or not.")]
        public FsmBool usedByEffector;
        
        [Tooltip("Sets whether the Collider will be used or not used by a CompositeCollider2D.")]
        public FsmBool usedByComposite;
        
        [Tooltip("The local offset of the collider geometry.")]
        public FsmVector2 offset;
        
        [Tooltip("The width and height of the capsule area.")]
        public FsmVector2 size;
        
        [Tooltip("The direction that the capsule sides can extend.")]
        [ObjectType(typeof(CapsuleDirection2D))]
        public FsmEnum direction;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;

            density = new FsmFloat { UseVariable = true };
            isTrigger = new FsmBool { UseVariable = true };
            usedByEffector = new FsmBool { UseVariable = true };
            usedByComposite = new FsmBool { UseVariable = true };
            offset = new FsmVector2 { UseVariable = true };
            size = new FsmVector2 { UseVariable = true };
            direction = new FsmEnum() {UseVariable = true};
            
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
            DoSetCapsuleCollider2D();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            DoSetCapsuleCollider2D();
        }

        void DoSetCapsuleCollider2D()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                return;
            }

            if (!density.IsNone)  this.cachedComponent.density = density.Value;
            if (!isTrigger.IsNone) this.cachedComponent.isTrigger = isTrigger.Value;
            if (!usedByEffector.IsNone) this.cachedComponent.usedByEffector = usedByEffector.Value;
            if (!usedByComposite.IsNone) this.cachedComponent.usedByComposite = usedByComposite.Value;
            if (!offset.IsNone) this.cachedComponent.offset = offset.Value;
            if (!size.IsNone) this.cachedComponent.size = size.Value;

            if (!direction.IsNone)
            {
                this.cachedComponent.direction = (CapsuleDirection2D) direction.Value;
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
