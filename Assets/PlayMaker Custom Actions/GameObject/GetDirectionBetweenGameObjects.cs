// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
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
	[Tooltip("Get a Direction Vector Between two gameobjects, return both 3d or 2d direction, in world or self space")]
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
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction from gameobject to target, express in 'Space' reference")]
		public FsmVector3 direction;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The 2d direction from gameobject to target, express in 'Space' reference")]
		public FsmVector2 direction2d;
		

		private GameObject _source;
		private GameObject _target;

		public override void Reset()
		{
			base.Reset ();
			gameObject = null;
			target = new FsmOwnerDefault();
			target.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			space = Space.World;
			direction = null;
			direction2d = null;
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
				if (!direction.IsNone)	direction.Value = _source.transform.position - _target.transform.position;

				if (!direction2d.IsNone) direction2d.Value =  _source.transform.position - _target.transform.position;
			}
			else
			{
				if (!direction.IsNone)	direction.Value = _source.transform.InverseTransformDirection(_target.transform.position);

				if (!direction2d.IsNone) direction2d.Value = _source.transform.InverseTransformDirection(_target.transform.position);

			}
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

