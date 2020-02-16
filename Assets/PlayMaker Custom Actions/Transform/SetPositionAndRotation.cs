// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
                        "Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
                    ],
"version":"1.1.0"
}
EcoMetaEnd
---*/

using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Sets the Position and rotation of a Game Object. To leave any axis unchanged, set variable to 'None'.\n Advanced features allows selection of update type.")]
    public class SetPositionAndRotation : FsmStateAction
    {

        [RequiredField]
        [Tooltip("The GameObject to position.")]
        public FsmOwnerDefault gameObject;

        [ActionSection("Position")]
        [UIHint(UIHint.Variable)]
        [Tooltip("Use a stored Vector3 position, and/or set individual axis below.")]
        public FsmVector3 position;

        public FsmFloat x;
        public FsmFloat y;
        public FsmFloat z;

		[ActionSection("Rotation")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Use a stored quaternion, or vector angles below.")]
		public FsmQuaternion quaternion;

		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		[Tooltip("Use euler angles stored in a Vector3 variable, and/or set each axis below.")]
		public FsmVector3 euler;

		public FsmFloat xAngle;
		public FsmFloat yAngle;
		public FsmFloat zAngle;


        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;

        public override void Reset()
        {

            gameObject = null;
            position = null;
            // default axis to variable dropdown with None selected.
            x = new FsmFloat { UseVariable = true };
            y = new FsmFloat { UseVariable = true };
            z = new FsmFloat { UseVariable = true };

			quaternion = null;

			euler = new FsmVector3 { UseVariable = true };
			xAngle = new FsmFloat { UseVariable = true };
			yAngle = new FsmFloat { UseVariable = true };
			zAngle = new FsmFloat { UseVariable = true };


            everyFrame = false;

            updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
        }

        public override void OnPreprocess()
        {
            if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
            {
                Fsm.HandleFixedUpdate = true;
            }

#if PLAYMAKER_1_8_5_OR_NEWER
            if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
            {
                Fsm.HandleLateUpdate = true;
            }
#endif
        }

        public override void OnUpdate()
        {
            if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
            {
                ExecuteAction();
            }

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnLateUpdate()
        {
            if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
            {
                ExecuteAction();
            }

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
            {
                ExecuteAction();
            }

            if (!everyFrame)
            {
                Finish();
            }
        }


        void ExecuteAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            // position

			Vector3 _position;

            if (position.IsNone)
            {
                _position = go.transform.position;
            }
            else
            {
                _position = position.Value;
            }

            // override any axis

            if (!x.IsNone) _position.x = x.Value;
            if (!y.IsNone) _position.y = y.Value;
            if (!z.IsNone) _position.z = z.Value;

			// rotation
			Vector3 _rotation;

			if (!quaternion.IsNone)
			{
				_rotation = quaternion.Value.eulerAngles;
			}
			else if (!euler.IsNone)
			{
				_rotation = euler.Value;
			}
			else
			{
				// use current rotation of the game object

				_rotation = go.transform.eulerAngles;
			}	

			// Override each axis

			if (!xAngle.IsNone)
			{
				_rotation.x = xAngle.Value;
			}

			if (!yAngle.IsNone)
			{
				_rotation.y = yAngle.Value;
			}

			if (!zAngle.IsNone)
			{
				_rotation.z = zAngle.Value;
			}

            // apply

			go.transform.SetPositionAndRotation(_position,Quaternion.Euler(_rotation));

        }


    }
}
