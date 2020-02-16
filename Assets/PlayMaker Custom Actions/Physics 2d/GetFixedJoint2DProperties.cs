// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Gets the various properties of a FixedJoint2D component.")]
    public class GetFixedJoint2DProperties : ComponentAction<FixedJoint2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(FixedJoint2D))]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmBool enableCollision;

        [UIHint(UIHint.Variable)]
        public FsmGameObject connectedBody;

        [UIHint(UIHint.Variable)]
        public FsmBool autoConfigureConnectedAnchor;

        [UIHint(UIHint.Variable)]
        public FsmVector2 anchor;

        [UIHint(UIHint.Variable)]
        public FsmVector2 connectedAnchor;

        [UIHint(UIHint.Variable)]
        public FsmFloat dampingRatio;

        [UIHint(UIHint.Variable)]
        public FsmFloat frequency;

        [UIHint(UIHint.Variable)]
        public FsmFloat breakForce;

        [UIHint(UIHint.Variable)]
        public FsmFloat breakTorque;

        [UIHint(UIHint.Variable)]
        public FsmVector2 reactionForce;

        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            enableCollision = null;
            anchor = null;
            connectedAnchor = null;
            reactionForce = null;
            autoConfigureConnectedAnchor = null;
            breakForce = null;
            breakTorque = null;
            dampingRatio = null;
            frequency = null;
            connectedBody = null;

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
                if (!anchor.IsNone)
                {
                    anchor.Value = this.cachedComponent.anchor;
                }

                if (!connectedAnchor.IsNone)
                {
                    connectedAnchor.Value = this.cachedComponent.connectedAnchor;
                }

                if (!reactionForce.IsNone)
                {
                    reactionForce.Value = this.cachedComponent.reactionForce;
                }

                if (!autoConfigureConnectedAnchor.IsNone)
                {
                    autoConfigureConnectedAnchor.Value = this.cachedComponent.autoConfigureConnectedAnchor;
                }

                if (!enableCollision.IsNone)
                {
                    enableCollision.Value = this.cachedComponent.enableCollision;
                }

                if (!breakForce.IsNone)
                {
                    breakForce.Value = this.cachedComponent.breakForce;
                }

                if (!breakTorque.IsNone)
                {
                    breakTorque.Value = this.cachedComponent.breakTorque;
                }

                if (!dampingRatio.IsNone)
                {
                    dampingRatio.Value = this.cachedComponent.dampingRatio;
                }

                if (!frequency.IsNone)
                {
                    frequency.Value = this.cachedComponent.frequency;
                }

                if (!connectedBody.IsNone)
                {
                    connectedBody.Value = this.cachedComponent.connectedBody!=null?this.cachedComponent.connectedBody.gameObject:null;
                }
            }
        }
    }
}