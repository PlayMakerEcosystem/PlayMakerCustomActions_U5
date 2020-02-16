// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the side of a point compared to a transform")]
	public class GetTransformPointSide : FsmStateAction
	{
		public enum Sides {Unknown,Front,Back,Top,Bottom,Left,Right}
		
		[RequiredField]
		[Tooltip("The GameObject transform acting as the center for check which side the point it")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The point position. Leave to none if you are setting 'point' ")]
		public FsmOwnerDefault pointGameObject;

		[Tooltip("The position. Leave to none for no effect, else overrides pointTransform")]
		public FsmVector3 point;

		[Tooltip("This is used for point to define its reference")]
		public Space space;

		[Tooltip("Threshold value, a value of 1 means that the point must be perfectly perpendicular to side face. If you get unknown results, lower the threshold")]
		[HasFloatSlider(0.5f,1f)]
		public FsmFloat threshold;
		
		[ActionSection("Result")]

		[UIHint(UIHint.Variable)]
		[Tooltip("The side")]
		[ObjectType(typeof(Sides))]
		public FsmEnum side;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The side as string")]
		public FsmString sideAsString;

		[Tooltip("Event sent side is front")]
		public FsmEvent frontEvent;
		
		[Tooltip("Event sent side is back")]
		public FsmEvent backEvent;
		
		[Tooltip("Event sent side is top")]
		public FsmEvent topEvent;
		
		[Tooltip("Event sent side is bottom")]
		public FsmEvent bottomEvent;
		
		[Tooltip("Event sent side is left")]
		public FsmEvent leftEvent;
		
		[Tooltip("Event sent side is right")]
		public FsmEvent rightEvent;
		
		[Tooltip("Event sent side is unknown, likely because center and point are the same")]
		public FsmEvent unknownEvent;
		
		[Tooltip("Repeat every frame. Note: Events will be sent only when side has changed")]
		public bool everyFrame;


	//	public float up;
	//	public float forward;
	//	public float right;
		
		private Collider _goCollider;
		
		private GameObject _goReference;
		private GameObject _goPosition;
		private Vector3 _position;
		private Sides _previousSide;
		private Sides _side;
		
		public override void Reset()
		{
			gameObject = null;
			pointGameObject = null;
			point = new FsmVector3{UseVariable = true};
			space = Space.World;
			threshold = 0.7f;
			side = null;
			sideAsString = null;
			frontEvent = null;
			backEvent = null;
			topEvent = null;
			bottomEvent = null;
			leftEvent = null;
			rightEvent = null;
			unknownEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_previousSide = Sides.Unknown;
			
			ExecuteAction();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			ExecuteAction();
		}
		
		void ExecuteAction()
		{
			_goReference = Fsm.GetOwnerDefaultTarget(gameObject);

			if (_goReference == null)
			{
				if (!side.IsNone)
				{
					side.Value = _side;
				}

				if (!sideAsString.IsNone)
				{
					sideAsString.Value = _side.ToString();
				}
				
				return;
			}
			
			if (!point.IsNone)
			{
				_position = space == Space.Self ? _goReference.transform.TransformPoint(_position):_position;
			}
			else
			{
				_goPosition = Fsm.GetOwnerDefaultTarget(pointGameObject);
				_position = _goPosition.transform.position;
			}

			_side = GetSide(_goReference.transform, _goReference.transform.position, _position);
			
			if (!side.IsNone)
			{
				side.Value = _side;
			}

			if (!sideAsString.IsNone)
			{
				sideAsString.Value = _side.ToString();
			}
			
			if (_previousSide != _side)
			{
				if (_side == Sides.Front)
				{
					Fsm.Event(frontEvent);
				}
				if (_side == Sides.Back)
				{
					Fsm.Event(backEvent);
				}
				if (_side == Sides.Top)
				{
					Fsm.Event(topEvent);
				}
				if (_side == Sides.Bottom)
				{
					Fsm.Event(bottomEvent);
				}
				if (_side == Sides.Left)
				{
					Fsm.Event(leftEvent);
				}
				if (_side == Sides.Right)
				{
					Fsm.Event(rightEvent);
				}
			}
			
		}

		private float dotUp;
		private float dotForward;
		private float dotRight;
		
		Sides GetSide(Transform transform,Vector3 center,Vector3 point)
		{
			Vector3 _normal = point-center;
			_normal.Normalize();
			dotUp = Vector3.Dot(_normal, transform.up);
			dotForward = Vector3.Dot(_normal, transform.forward);
			dotRight = Vector3.Dot(_normal, transform.right);

			//up = dotUp;
			//forward = dotForward;
			//right = dotRight;
			
			if(dotUp < -threshold.Value)
			{
				return Sides.Bottom;
			}else if(dotUp > threshold.Value)
			{
				return Sides.Top;
			}else if (dotForward < -threshold.Value)
			{
				return Sides.Back;
			}else if(dotForward > threshold.Value)
			{
				return Sides.Front;
                            
			}else if (dotRight < -threshold.Value)
			{
				return Sides.Left;
			}else if (dotRight > threshold.Value)
			{
				return Sides.Right;
			}

			return Sides.Unknown;
		}
	}
}
