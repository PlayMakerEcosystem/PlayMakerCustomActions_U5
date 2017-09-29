// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//author : zeeawk

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Adds a collider mask to a Platform Effector 2D")]
    public class SetPlatformEffector2DColliderMask : FsmStateAction
    {
        public enum Operation
        {
            Enable,
            Disable
        }

        [RequiredField]
        [CheckForComponent(typeof(PlatformEffector2D))]
        [Tooltip("The GameObject with the PlatformEffector2D attached")]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.Layer)]
        [Tooltip("The collision layer to add or remove")]
        public int collisionLayer;

        [Tooltip("Add or Remove the collision layer")]
        public Operation operation = Operation.Enable;

        [Tooltip("Reset when exiting this state.")]
        public FsmBool resetOnExit;

        PlatformEffector2D pe;



        public override void Reset()
        {
            gameObject = null;
            collisionLayer = 0;
            operation = Operation.Enable;
            resetOnExit = false;
        }

        public override void OnEnter()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            pe = go.GetComponent<PlatformEffector2D>();
            if (pe != null)
            {
                switch (operation)
                {
                    case Operation.Enable:
                        Enable();
                        break;

                    case Operation.Disable:
                        Disable();
                        break;
                }
                Finish();
            }
        }

        public override void OnExit()
        {
            if (pe != null && resetOnExit.Value)
            {
                switch (operation)
                {
                    case Operation.Enable:
                        Disable();
                        break;

                    case Operation.Disable:
                        Enable();
                        break;
                }
            }

        }

        void Enable()
        {
            pe.colliderMask |= (1 << collisionLayer);
        }

        void Disable()
        {
            pe.colliderMask ^= (1 << collisionLayer);
        }
    }
}
