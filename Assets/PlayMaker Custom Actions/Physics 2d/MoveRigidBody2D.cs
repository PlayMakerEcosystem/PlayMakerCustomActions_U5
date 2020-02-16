// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Move a rigidbody2d to another GameObject using easing properties. Collisions are respected during move.")]
    public class MoveRigidBody2D : EaseFsmAction
    {
        [RequiredField]
        public FsmOwnerDefault objectToMove;

        [RequiredField]
        public FsmGameObject destination;

        private FsmVector3 fromValue;
        private FsmVector3 toVector;
        private FsmVector3 fromVector;

		private bool finishInNextStep;

        Rigidbody2D _rb2d;
        public override void Reset()
        {
            base.Reset();
            fromValue = null;
            toVector = null;
            finishInNextStep = false;
            fromVector = null;
        }

        public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            var go = Fsm.GetOwnerDefaultTarget(objectToMove);
            _rb2d = go.GetComponent<Rigidbody2D>();

            fromVector = go.transform.position;
            toVector = destination.Value.transform.position;

            fromFloats = new float[3];
            fromFloats[0] = fromVector.Value.x;
            fromFloats[1] = fromVector.Value.y;
            fromFloats[2] = fromVector.Value.z;

            toFloats = new float[3];
            toFloats[0] = toVector.Value.x;
            toFloats[1] = toVector.Value.y;
            toFloats[2] = toVector.Value.z;
            resultFloats = new float[3];

            resultFloats[0] = fromVector.Value.x;
            resultFloats[1] = fromVector.Value.y;
            resultFloats[2] = fromVector.Value.z;

            finishInNextStep = false;
        }

        public override void OnFixedUpdate()
        {
    
            base.OnUpdate();
            _rb2d.MovePosition(new Vector2(resultFloats[0], resultFloats[1]));

            if (finishInNextStep)
            {
                Finish();
                if (finishEvent != null) Fsm.Event(finishEvent);
            }

            if (finishAction && !finishInNextStep)
            {
                _rb2d.MovePosition(new Vector2(reverse.IsNone ? toVector.Value.x : reverse.Value ? fromValue.Value.x : toVector.Value.x,
                reverse.IsNone ? toVector.Value.y : reverse.Value ? fromValue.Value.y : toVector.Value.y
                ));
                finishInNextStep = true;
            }
        }
    }
}