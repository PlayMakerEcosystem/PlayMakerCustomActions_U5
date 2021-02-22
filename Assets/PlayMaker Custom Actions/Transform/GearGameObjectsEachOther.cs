using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    /*
     * This Action is useful if the target gameobject is looking in the Z axis direction.
     */
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Gears two gameobjects to each other, using by their 'Y' rotation. ")]
    public class GearGameObjectsEachOther : FsmStateAction
    {
        private const float ONE_CIRCLE = 360;
        private const float RIGHT_ANGLE = 90;
        
        private static ILogger LOGGER = Debug.unityLogger;
        private static string loggerName = "GearGameObjectsEachOther";

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Base GameObject which 'Y' rotation's value is taken as base value.")]
        public FsmGameObject baseGameObject;

        [RequiredField]
        [Tooltip("Target GameObject which 'Y' rotation's value equals with base gameobject's Y rotation value.")]
        public FsmOwnerDefault targetGameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip(
            "Turning speed of the target gameobject. How much time will the target game object will before it takes the next turning portion. Negative value will be turned to its absolute value.")]
        public FsmFloat turningSpeed;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Turning degree of the target gameobject. How much value will be added to the previous rotation value? Negative value will be turned to its absolute value.")]
        public FsmFloat turningDegree;


        public override void Reset()
        {
            baseGameObject = null;
            targetGameObject = null;
            turningSpeed = null;
            turningDegree = null;
        }

        public override void OnEnter()
        {
            gearEachOther();

        }

        void gearEachOther()
        {
            LOGGER.Log(loggerName, "Base game object's transform.rotation is " + baseGameObject.Value.transform.rotation);
            
            float base_Y = Mathf.Ceil(baseGameObject.Value.transform.rotation.y);

            switch (Math.Sign(base_Y))
            {
                case -1:
                    negativeBranch();
                    break;

                case 0:
                    zeroBranch();
                    break;

                case 1:
                    positiveBranch(base_Y);
                    break;
            }
        }

        private void positiveBranch(float base_Y)
        {
            int rounds = (int) (base_Y / ONE_CIRCLE);

            float baseNormalisedYRotation = base_Y - (ONE_CIRCLE * rounds) + RIGHT_ANGLE;
            
            Quaternion targetQuaternion = Fsm.GetOwnerDefaultTarget(targetGameObject).gameObject.transform.rotation;
            
            float x = Mathf.Ceil(targetQuaternion.x);
            float z = Mathf.Ceil(targetQuaternion.z);

            float targetRotationToBe = baseNormalisedYRotation - Mathf.Ceil(targetQuaternion.y);

            float targetY = 0.0f;
            for (float k = 0; k < targetRotationToBe; k = (k + Math.Abs(turningDegree.Value)))
            {
                targetY = targetY + targetQuaternion.y + k;
                StartCoroutine(wait(x, targetY, z));
            }
            LOGGER.Log(loggerName, "time after iteration is " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.SS"));
        }

        private void zeroBranch()
        {
            throw new NotImplementedException();
        }

        private void negativeBranch()
        {
            throw new NotImplementedException();
        }
        
        
        private IEnumerator wait(float x, float y, float z)
        {
            yield return new WaitForSecondsRealtime(Math.Abs(turningSpeed.Value));
            Fsm.GetOwnerDefaultTarget(targetGameObject).gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
    }
}