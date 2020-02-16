// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the runtimeAnimatorController of an Animator Component.")]
	public class GetRuntimeAnimatorController : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(RuntimeAnimatorController))]
		[Tooltip("The current controller of the specified Animator.")]
		public FsmObject getController;

		[Tooltip("Repeat every frame. Useful when using normalizedTime to manually control the animation.")]
		public bool everyFrame;

		Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			getController = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			// get the animator component
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			if(go == null)
			{
				Finish();
				return;
			}

			_animator = go.GetComponent<Animator>();

			DoAnimatorPlay();
			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAnimatorPlay();
		}

		void DoAnimatorPlay()
		{
			if(_animator != null)
			{
				getController.Value = _animator.runtimeAnimatorController;

				if(getController.Value == null)
				{
					LogWarning("Animator " + _animator.name + " doesn't have a runtimeAnimatorController!");
				}
			}
		}
	}
}