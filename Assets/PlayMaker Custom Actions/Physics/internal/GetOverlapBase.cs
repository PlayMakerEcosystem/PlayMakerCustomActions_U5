using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    public abstract class GetOverlapBase : FsmStateAction
    {
        [Tooltip("Use gameObject position as Overlap origin")]
        public FsmOwnerDefault owner;

        [Tooltip("Use custom position as Overlap origin")]
        public FsmVector3 fromPosition;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.GameObject)]
        [Tooltip("Array to store gameObject(s) overlaps, Array Type has to be GameObject")]
        public FsmArray array;

        public FsmBool includeOwner;

        [UIHint(UIHint.Layer)]
        [Tooltip("Layermask name to filter the grabbed objects")]
        public FsmInt layerMask;

        public FsmEvent onFoundGameObject;

        public bool drawGizmo;

        public override void Reset()
        {
            fromPosition = new FsmVector3 { UseVariable = true };
            drawGizmo = false;
            array = null;
        }

        public override void OnEnter()
        {
            GetOverlappedGameObject();
        }

        protected virtual void GetOverlappedGameObject() { }
    }
}
