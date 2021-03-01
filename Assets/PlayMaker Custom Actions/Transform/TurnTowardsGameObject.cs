using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    /// <summary>
    /// Author is Istvan Nemeth. <see cref="https://github.com/PlayMakerEcosystem/PlayMakerCustomActions_U5"/>
    /// </summary>
    public class TurnTowardsGameObject: FsmStateAction
    {
        private const float HalfCircle = 180;
        private const float WholeCircle = 360;

        [RequiredField] 
        [Tooltip("The gameobject what will be rotated toward the target gameobject.")]
        public FsmOwnerDefault Myself;

        [RequiredField]
        [UIHint(UIHint.FsmGameObject)]
        [Tooltip(
            "Target gameobject, where I will turn. So my Z axis will points towards the target gameobject's Z direct.")]
        public FsmGameObject TargetGameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Turning speed of the target gameobject." +
                 " How much time will the target game object will before it takes the next turning portion." +
                 " Negative value will be turned to its absolute value.")]
        public FsmFloat TurningSpeed;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip(
            "Turning degree of the target gameobject." +
            " How much value will be added to the previous rotation value?" +
            " Negative value will be turned to its absolute value.")]
        public FsmFloat TurningDegree;

        [UIHint(UIHint.Variable)] public FsmFloat yAngle;

        private Vector3 _rotation;


        public override void Reset()
        {
            TargetGameObject = null;
            TurningSpeed = null;
            TurningDegree = null;
        }

        public override void OnEnter()
        {
            var turnDegree = ChangeSign(TurningDegree.Value);
            TurningDegree.Value = turnDegree;

            var turnSpeed = ChangeSign(TurningSpeed.Value);
            TurningSpeed.Value = turnSpeed;

            var targetObject = TargetGameObject.Value;
            var myself = Fsm.GetOwnerDefaultTarget(Myself);

            if (null == targetObject)
                return;

            if (null == myself)
                return;

            if (!yAngle.IsNone)
                StartCoroutine(TurnVehicleTowardsTarget(myself, targetObject, turnDegree, turnSpeed));

            //Fsm.Event(FsmEvent.Finished);
        }

        private float ChangeSign(float toBeChanged)
        {
            return Math.Sign(toBeChanged) == -1 ? toBeChanged *= -1 : toBeChanged;
        }
        private IEnumerator TurnVehicleTowardsTarget(GameObject myself, GameObject target, float degree, float speed)
        {
            _rotation = myself.transform.localEulerAngles;

            var x = _rotation.x;
            var z = _rotation.z;
            var mySignedCurrYRot = SignedCurrentYRotation(_rotation.y);

            switch (Math.Sign(mySignedCurrYRot))
            {
                case -1:
                    for (var i = 0f; i < mySignedCurrYRot; i = (i + Math.Abs(degree)))
                    {
                        yield return SetMyAngles(myself, speed, x, i, z);
                    }
                    break;

                // if mySignedCurrYRot = 120;
                case 1:
                    for (var i = mySignedCurrYRot; i > 0; i = (i - Math.Abs(degree)))
                    {
                        yield return SetMyAngles(myself, speed, x, i, z);
                    }
                    break;

                case 0: break;
            }
        }
        private static object SetMyAngles(GameObject myself, float speed, float x, float y, float z)
        {
            myself.transform.eulerAngles = new Vector3(x, y, z);
            return new WaitForSeconds(Math.Abs(speed));
        }
        private static float SignedCurrentYRotation(float y)
        {
            float myCurrentYRotation;
            if (y > HalfCircle)
            {
                myCurrentYRotation = -(WholeCircle - y);
            }
            else
            {
                myCurrentYRotation = y;
            }

            return myCurrentYRotation;
        }
    }
}