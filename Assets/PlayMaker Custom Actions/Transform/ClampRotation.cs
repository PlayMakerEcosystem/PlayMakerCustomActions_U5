// (c) Copyright// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Clamp a rotation around a local axis, optionally define the default orientation, useful to control the initial rotation to clamp from. Clamp is done on Lateupdate")]
	public class ClampRotation : FsmStateAction
	{
		public enum ConstraintAxis {x = 0,y,z};
		
		[RequiredField]
		[Tooltip("The GameObject to clamp its rotation.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The default rotation. If none, will use the gameobject target.")]
		public FsmVector3 defaultRotation;

		[ObjectType(typeof(ConstraintAxis))]
		[Tooltip("The axis to constraint the rotation")]
		public FsmEnum constraintAxis;

		[Tooltip("The minimum angle allowed")]
		public FsmFloat minAngle;

		[Tooltip("The maximum angle allowed")]
		public FsmFloat maxAngle;

        public FsmBool withinRange;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;


		ConstraintAxis axis;
		int axisIndex;


        bool _withinRange;

		float RealAngle;
        
		float Min;
		float Max;
		float Angle;
		float RangeMidOpposite;
        
		public override void Reset()
		{
			gameObject = null;
			defaultRotation = new FsmVector3 (){UseVariable = true};

			constraintAxis = null;
			minAngle = -45f;
			maxAngle = 45f;

			everyFrame = false;
		}
		
		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		Vector3 _defaultRotation;

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

            _withinRange = true;
            axis = (ConstraintAxis)constraintAxis.Value;
			axisIndex = (int)axis;
		

			if (!defaultRotation.IsNone) {
				_defaultRotation = defaultRotation.Value;

			} else {
				_defaultRotation = go.transform.localRotation.eulerAngles;
			}

		

		}
		public override void OnLateUpdate()
		{
			DoClampRotation();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoClampRotation()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

            float _originalrotation = go.transform.eulerAngles[axisIndex] + _defaultRotation[axisIndex];
            float rotation = ClampAngle(_originalrotation, minAngle.Value, maxAngle.Value);

          
            Vector3 _euler = go.transform.eulerAngles;
            _euler[axisIndex] = rotation;


             go.transform.eulerAngles = _euler;

        }



        private float ClampAngle(float angle, float min, float max)
        {
	      //  RealAngle = angle;

	        min = 360+min;
	        float _oppositeRangeMidAngle = (min-max)/2 + max;

	        /*
            Min = min;
            Max = max;
            RangeMidOpposite = _oppositeRangeMidAngle;
            Angle = angle;
*/
	        
            if (angle > min)
            {
	            return angle;
            }

            if (angle > _oppositeRangeMidAngle)
            {
	            return min;
            }
            
            if (angle > max )
            {
	            if (angle > _oppositeRangeMidAngle)
	            {
		            return min;
	            }
	            
	            return max;
            }

            return angle;
        }
	}
}