// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
// Keywords : 2d 3d raycast ray
---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Get a Direction Vector Between two gameobjects, return both 3d or 2d direction, in world or self space, direction can be normalized as well")]
	public class GetDirectionBetweenGameObjects : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The GameObject")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The target GameObject")]
		public FsmOwnerDefault target;

		[ActionSection("Result")]
		[Tooltip("In what reference to express the direction")]
		public Space space;

		[Tooltip("if true, normalize the direction result")]
		public bool normalize;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction from gameobject to target, express in 'Space' reference")]
		public FsmVector3 direction;

		[UIHint(UIHint.Variable)]
		[Tooltip("The 2d direction from gameobject to target, express in 'Space' reference")]
		public FsmVector2 direction2d;

		[UIHint(UIHint.Variable)]
		[Tooltip("The distance between the two gameobjects, make sense when normaized is false")]
		public FsmFloat distance;
		

		private GameObject _source;
		private GameObject _target;

		private Vector3 _dir;

		public override void Reset()
		{
			base.Reset ();
			gameObject = null;
			target = new FsmOwnerDefault();
			target.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			space = Space.World;
			normalize = true;
			direction = null;
			direction2d = null;
			distance = null;
		}

		public override void OnEnter()
		{
			OnActionUpdate ();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

	
		public override void OnActionUpdate()
		{
			ExecuteAction();
		}
		
		void ExecuteAction()
		{
			_source = Fsm.GetOwnerDefaultTarget(gameObject);
			_target = Fsm.GetOwnerDefaultTarget(target);

			if (_source == null || _target == null)
			{
				return;
			}

			if (space == Space.World)
			{
				_dir = _source.transform.position - _target.transform.position;
				if (normalize) _dir.Normalize();
				
				if (!direction.IsNone)	direction.Value = _dir;
				if (!direction2d.IsNone) direction2d.Value =  _dir;
			}
			else
			{
				_dir = _source.transform.InverseTransformDirection(_target.transform.position);
				if (normalize) _dir.Normalize();
				
				if (!direction.IsNone)	direction.Value = _dir;
				if (!direction2d.IsNone) direction2d.Value = _dir;
			}

			if(!distance.IsNone)	distance = _dir.magnitude;
		}

		public override string ErrorCheck()
		{
			_source = Fsm.GetOwnerDefaultTarget(gameObject);
			_target = Fsm.GetOwnerDefaultTarget(target);

			if (_source!=null && _source == _target)
			{
				return "Source and Target can not be the same GameObject";
			}
			
			return "";
		}
	}
}

