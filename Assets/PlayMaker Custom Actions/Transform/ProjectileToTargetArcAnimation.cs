using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("Custom")]
    public class ProjectileToTargetArcAnimation : FsmStateAction
    {

        [RequiredField]
        [Tooltip("The GameObject to rotate.")]
        public FsmOwnerDefault gameObject;
        
        public FsmVector3 startPos;
        
        [Tooltip("Position we want to hit")]
        public FsmVector3 targetPos;

        [Tooltip("Horizontal speed, in units/sec")]
        public FsmFloat speed;

        [Tooltip("How high the arc should be, in units")]
        public FsmFloat arcHeight = 1;

        public FsmVector3 nextPos;

        public FsmFloat archfloat;

        public FsmEvent targetReached;
        
        [Tooltip("Repeat every frame.")]
        public bool everyFrame = true;
        
        private GameObject _go;

        public override void Reset()
        {
            gameObject = null;
            targetPos = null;
            speed = 10f;
            arcHeight = 1f;
            archfloat = -0.25f;
            targetReached = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            _go = this.Fsm.GetOwnerDefaultTarget(gameObject);
            
            // Cache our start position, which is really the only thing we need
            // (in addition to our current position, and the target).
            if (startPos.IsNone)
            {
                startPos.Value = _go.transform.position;
            }
        }


        public override void OnUpdate()
        {
            // Compute the next position, with arc added in
            FsmFloat x0 = startPos.Value.x;
            FsmFloat x1 = targetPos.Value.x;
            FsmFloat dist = x1.Value - x0.Value;
            FsmFloat nextX = Mathf.MoveTowards(_go.transform.position.x, x1.Value, speed.Value * Time.deltaTime);
            FsmFloat baseY = Mathf.Lerp(startPos.Value.y, targetPos.Value.y, (nextX.Value - x0.Value) / dist.Value);
            FsmFloat arc = arcHeight.Value * (nextX.Value - x0.Value) * (nextX.Value - x1.Value) / (archfloat.Value * dist.Value * dist.Value);
            nextPos = new Vector3(nextX.Value, baseY.Value + arc.Value, _go.transform.position.z);

            // Rotate to face the next position, and then move there
            if (nextPos.Value != _go.transform.position)
            {
                _go.transform.rotation = LookAt2D(nextPos.Value - _go.transform.position);
                _go.transform.position = nextPos.Value;
            }

            if (Mathf.Approximately((_go.transform.position-targetPos.Value).sqrMagnitude,0f))
            {
                _go.transform.position = targetPos.Value;
                Fsm.Event(targetReached);
                Finish();
            }
                
        }
        

        ///
        /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
        /// that makes the local +X axis point in the given forward direction.
        ///
        /// forward direction
        /// Quaternion that rotates +X to align with forward
        static Quaternion LookAt2D(Vector2 forward)
        {
            
            return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
        }
    }
}