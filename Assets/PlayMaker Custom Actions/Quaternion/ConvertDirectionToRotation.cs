// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: convert direction rotation lookat raycast

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Quaternion)]
    [Tooltip("Creates a rotation that looks along a direction")]
    public class ConvertDirectionToRotation : QuaternionBaseAction
    {
        [Tooltip("The Direction starting GameObject")]
        public FsmGameObject directionStart;

        [Tooltip("The Direction ending GameObject")]
        public FsmGameObject directionEnd;

        [Tooltip("Or The Direction as a vector")]
        public FsmVector3 orDirectionVector;

        [Tooltip("Invert the Direction")]
        public FsmBool invertDirection;

        [Tooltip("The up direction")]
        public FsmVector3 upVector;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the the rotation variable.")]
        public FsmQuaternion result;

        public FsmOwnerDefault rotateGameObject;

        Vector3 _dir;
        Vector3 _start;
        Vector3 _end;
        Quaternion _result;
        GameObject _applyToGo;

        public override void Reset()
        {
            directionStart = new FsmGameObject() { UseVariable = true };
            directionEnd = new FsmGameObject() { UseVariable = true };
            orDirectionVector = null;
            invertDirection = false;
            upVector = new FsmVector3() { UseVariable = true };
            result = null;
            rotateGameObject = null;
            everyFrame = true;
            everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
        }

        public override void OnEnter()
        {
            Execute();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrameOption == everyFrameOptions.Update)
            {
                Execute();
            }
        }
        public override void OnLateUpdate()
        {
            if (everyFrameOption == everyFrameOptions.LateUpdate)
            {
                Execute();
            }
        }

        public override void OnFixedUpdate()
        {
            if (everyFrameOption == everyFrameOptions.FixedUpdate)
            {
                Execute();
            }
        }

        void Execute()
        {
            if (!orDirectionVector.IsNone)
            {
                _dir = orDirectionVector.Value;
            }
            else
            {
                if (directionStart.Value!=null)
                {
                    _start = directionStart.Value.transform.position;
                }
                else
                {
                    _start = Vector3.zero;
                }

                if (directionEnd.Value != null)
                {
                    _end = directionEnd.Value.transform.position;
                }
                else
                {
                    _end = Vector3.zero;
                }

                _dir = _end - _start;
            }

            if (invertDirection.Value)
            {
                _dir = _dir * -1;
            }

            if (!upVector.IsNone)
            {
                _result = Quaternion.LookRotation(_dir, upVector.Value);
            }
            else
            {
                _result = Quaternion.LookRotation(_dir);
            }

            if (!result.IsNone)
            {
                result.Value = _result;
            }

            _applyToGo = Fsm.GetOwnerDefaultTarget(rotateGameObject);
            if (_applyToGo!=null)
            {
                _applyToGo.transform.rotation = _result;
            }
        }
    }
}

