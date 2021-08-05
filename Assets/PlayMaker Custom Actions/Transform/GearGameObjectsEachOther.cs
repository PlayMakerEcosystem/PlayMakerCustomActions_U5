using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    /*
     * This Action is useful if the target gameobject is looking in the Z axis direction.
     */
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Gears two gameobjects to each other, using by their 'Y' rotation. This Action is useful if the target gameobject is looking in the Z axis direction.")]
    public class GearGameObjectsEachOther : FsmStateAction
    {
        private const float OneCircleConst = 360;
        private const float RightAngleConst = 90;

        private const string RightConst = "Right";

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Base GameObject which 'Y' rotation's value is taken as base value.")]
        public FsmGameObject BaseGameObject;

        [RequiredField]
        [Tooltip("Target GameObject which 'Y' rotation's value equals with base gameobject's Y rotation value.")]
        public FsmOwnerDefault TargetGameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Turning speed of the target gameobject. How much time will the target game object will before it takes the next turning portion. Negative value will be turned to its absolute value.")]
        public FsmFloat TurningSpeed;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Turning degree of the target gameobject. How much value will be added to the previous rotation value? Negative value will be turned to its absolute value.")]
        public FsmFloat TurningDegree;

        public override void Reset()
        {
            BaseGameObject = null;
            TargetGameObject = null;
            TurningSpeed = null;
            TurningDegree = null;
        }

        public override void OnEnter()
        {
            if(GearEachOther())
                Fsm.Event(FsmEvent.Finished);
        }

        private bool GearEachOther()
        {
            var targetGameObjectVector = Fsm.GetOwnerDefaultTarget(TargetGameObject).gameObject.transform.eulerAngles;

            var baseGameObjectVector = BaseGameObject.Value.transform.eulerAngles;

            var baseGameObjectY = baseGameObjectVector.y;
            
            if (BaseGameObject.Value.HasTag(RightConst))
                baseGameObjectY -= OneCircleConst;

            StartCoroutine(TurnTargetToBaseYRotation(NormalisedYRotation(baseGameObjectY), targetGameObjectVector));

            return baseGameObjectY.Equals(targetGameObjectVector.y);
        }

        private float NormalisedYRotation(float baseY)
        {
            if (baseY >= OneCircleConst)
            {
                return baseY - (OneCircleConst * ((int) (baseY / OneCircleConst)));
            }
            return baseY;
        }

        private IEnumerator TurnTargetToBaseYRotation(float baseY, Vector3 target)
        {
            var x = target.x;
            var z = target.z;

            float targetRotationToBe;
            float targetY = NormalisedYRotation(target.y);

            switch (Math.Sign(baseY))
            {
                // NEGATIVE CASE 
                case -1:

                    targetRotationToBe = baseY - RightAngleConst;

                    for (float k = 0; k > targetRotationToBe; k = (k - Math.Abs(TurningDegree.Value)))
                    {
                        var y = Mathf.Abs(targetY - k) * -1;

                        Fsm.GetOwnerDefaultTarget(TargetGameObject).gameObject.transform.eulerAngles =
                            new Vector3(x, y, z);
                        //Quaternion.Euler(x, y, z);
                        yield return new WaitForSeconds(Math.Abs(TurningSpeed.Value));
                    }

                    break;

                case 0:
                    break;


                // POSITIVE CASE
                case 1:
                    targetRotationToBe = baseY + RightAngleConst;

                    for (float k = 0; k < targetRotationToBe; k = (k + Math.Abs(TurningDegree.Value)))
                    {
                        Fsm.GetOwnerDefaultTarget(TargetGameObject).gameObject.transform.rotation =
                            Quaternion.Euler(x, (targetY + k), z);
                        yield return new WaitForSeconds(Math.Abs(TurningSpeed.Value));
                    }

                    break;
            }
        }
    }
}