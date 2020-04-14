// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Original action: https://hutonggames.com/playmakerforum/index.php?topic=21655.msg95032#msg95032
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Gets the world transform velocity and direction of a GameObject")]
    public class GetTransformVelocity : FsmStateAction
    {
        public enum UpdateType { Update, FixedUpdate, LateUpdate}

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmOwnerDefault gameObject;

        [Tooltip("Store the direction / velocity")]
        [UIHint(UIHint.Variable)]
        public FsmVector3 storeDirection;

        [Tooltip("Store local direction or world?")]
        public Space space;

        [Tooltip("Store the magnitude / speed")]
        [UIHint(UIHint.Variable)]
        public FsmFloat storeMagnitude;
       
        [Tooltip("Store the square magnitude / speed. Takes less performances")]
        [UIHint(UIHint.Variable)]
        public FsmFloat storeSquareMagnitude;
       
        public UpdateType updateType;
       
        private Vector3 _lastPos;
        private Vector3 _direction;
        private GameObject _go;

        public override void Reset()
        {
            gameObject = null;
            storeDirection = null;
            storeMagnitude = null;
            storeSquareMagnitude = null;
        }

        public override void OnPreprocess()
        {
            if (updateType == UpdateType.FixedUpdate)
                Fsm.HandleFixedUpdate = true;
            else if (updateType == UpdateType.LateUpdate)
                Fsm.HandleLateUpdate = true;
        }

        public override void OnEnter()
        {
            _go = Fsm.GetOwnerDefaultTarget(gameObject);
            _lastPos = _go.transform.position;
        }

        public override void OnUpdate()
        {
            if (updateType == UpdateType.Update)
                CalculateSpeed();
        }

        public override void OnFixedUpdate()
        {
            if (updateType == UpdateType.FixedUpdate)
                CalculateSpeed();
        }

        public override void OnLateUpdate()
        {
            if (updateType == UpdateType.LateUpdate)
                CalculateSpeed();
        }

        void CalculateSpeed()
        {
           
            _direction = _go.transform.position - _lastPos;
           
            if (!storeSquareMagnitude.IsNone) storeSquareMagnitude.Value = Mathf.Round((_direction / Time.deltaTime).sqrMagnitude * 100f) / 100f;

            if (!storeMagnitude.IsNone) storeMagnitude.Value = Mathf.Round((_direction / Time.deltaTime).magnitude * 100f) / 100f;

            _direction = _direction.normalized;
           
            if (space == Space.Self) _direction = _go.transform.InverseTransformDirection(_direction);
           
            if (!storeDirection.IsNone) storeDirection.Value = _direction;
           
            _lastPos = _go.transform.position;
        }

    }
}