// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Sets the various properties of a FixedJoint2D component.")]
    public class SetFixedJoint2DProperties : ComponentAction<FixedJoint2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(FixedJoint2D))]
        public FsmOwnerDefault gameObject;

        public FsmBool enableCollision;

        public FsmGameObject connectedBody;

      
        public FsmBool autoConfigureConnectedAnchor;

       
        public FsmVector2 anchor;

       
        public FsmVector2 connectedAnchor;


        public FsmFloat dampingRatio;

       
        public FsmFloat frequency;


        public FsmFloat breakForce;


        public FsmFloat breakTorque;


        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            enableCollision = new FsmBool() { UseVariable = true };
            anchor = new FsmVector2() { UseVariable = true };
            connectedAnchor = new FsmVector2() { UseVariable = true };
            autoConfigureConnectedAnchor = new FsmBool() { UseVariable = true };
            breakForce = new FsmFloat() { UseVariable = true };
            breakTorque = new FsmFloat() { UseVariable = true };
            dampingRatio = new FsmFloat() { UseVariable = true };
            frequency = new FsmFloat() { UseVariable = true };
            connectedBody = new FsmGameObject() { UseVariable = true };

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
                    this.cachedComponent.anchor = anchor.Value;
                }

                if (!connectedAnchor.IsNone)
                {
                    this.cachedComponent.connectedAnchor = connectedAnchor.Value;
                }

                if (!autoConfigureConnectedAnchor.IsNone)
                {
                    this.cachedComponent.autoConfigureConnectedAnchor = autoConfigureConnectedAnchor.Value;
                }

                if (!enableCollision.IsNone)
                {
                    this.cachedComponent.enableCollision = enableCollision.Value;
                }

                if (!breakForce.IsNone)
                {
                    this.cachedComponent.breakForce = breakForce.Value;
                }

                if (!breakTorque.IsNone)
                {
                   this.cachedComponent.breakTorque = breakTorque.Value;
                }

                if (!dampingRatio.IsNone)
                {
                    this.cachedComponent.dampingRatio = dampingRatio.Value;
                }

                if (!frequency.IsNone)
                {
                    this.cachedComponent.frequency = frequency.Value;
                }

                if (!connectedBody.IsNone)
                {
                    this.cachedComponent.connectedBody =  connectedBody.Value!=null? connectedBody.Value.GetComponent<Rigidbody2D>(): null;
                }
            }
        }
    }
}