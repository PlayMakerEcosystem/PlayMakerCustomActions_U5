// License: Attribution 4.0 International (CC BY 4.0)
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
        public FsmFloat dampTime;
        private Vector3 velocity = Vector3.zero;
        public FsmGameObject targetGameObject;
        private Transform target;
        public FsmFloat xOffset;
        public FsmFloat yOffset;
        [ActionSection("Other")]
        public FsmBool useFixedUpdate;
        public FsmBool disable;
        public FsmBool followX;
        public FsmBool followY;

        private float xValue;
        private float yValue;

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