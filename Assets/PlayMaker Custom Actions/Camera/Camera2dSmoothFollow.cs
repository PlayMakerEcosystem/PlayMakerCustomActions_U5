// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Camera)]
    [Tooltip("Camera 2d  SmoothFollow")]
    public class Camera2dSmoothFollow : FsmStateAction
    {
        [ActionSection("Setup")]
        [RequiredField]
        public FsmGameObject gameObject;

        [ActionSection("Camera")]

        [Tooltip("Approximately the time it will take to reach the target. A smaller value will reach the target faster.")]
        public FsmFloat dampTime;
       
        [Tooltip("The target to follow")]
        public FsmGameObject targetGameObject;
        [Tooltip("The target position to follow")]
        private Transform target;
        [Tooltip("Offset x value from target")]
        public FsmFloat xOffset;
        [Tooltip("Offset y value from target")]
        public FsmFloat yOffset;

        [ActionSection("Other")]

        [Tooltip("Disable Follow, this is usefull if you need to stay in the same state. you can use a set fsm bool action from another fsm to disable/enable follow")]
        public FsmBool disable;

        [Tooltip("use the xOffset")]
        public FsmBool followX;
        [Tooltip("use the yOffset")]
        public FsmBool followY;

        public FsmBool useFixedUpdate;

        private float xValue;
        private float yValue;
        private Vector3 velocity = Vector3.zero;

        public override void Reset()
        {
            dampTime = 0.15f;
            useFixedUpdate = false;
            xOffset = 0.5f;
            yOffset = 0.5f;
            disable = false;
            followX = true;
            followY = true;
        }

        public override void OnPreprocess()
        {
            if (useFixedUpdate.Value) Fsm.HandleFixedUpdate = true;

#if PLAYMAKER_1_8_5_OR_NEWER
            if (!useFixedUpdate.Value)
            {
                Fsm.HandleLateUpdate = true;
            }
#endif
        }

        public override void OnEnter()
        {
            if (useFixedUpdate.Value) OnFixedUpdate();
            else OnLateUpdate();
        }

        public override void OnFixedUpdate()
        {
            if (useFixedUpdate.Value && disable.Value == false) SmoothCamera();

        }

        public override void OnLateUpdate()
        {
            if (!useFixedUpdate.Value && disable.Value == false) SmoothCamera();
        }

        void SmoothCamera()
        {
            target = targetGameObject.Value.GetComponent<Transform>();
            Vector3 point = gameObject.Value.GetComponent<Camera>().WorldToViewportPoint(target.position);
            if (followX.Value)
            {
                xValue = xOffset.Value;
            }
            else
            {
                xValue = point.x;
            }
            if (followY.Value)
            {
                yValue = yOffset.Value;
            }
            else
            {
                yValue = point.y;
            }
            Vector3 delta = target.position - gameObject.Value.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(xValue, yValue, point.z));
            Vector3 destination = gameObject.Value.transform.position + delta;
            gameObject.Value.transform.position = Vector3.SmoothDamp(gameObject.Value.transform.position, destination, ref velocity, dampTime.Value);
        }
    }
}