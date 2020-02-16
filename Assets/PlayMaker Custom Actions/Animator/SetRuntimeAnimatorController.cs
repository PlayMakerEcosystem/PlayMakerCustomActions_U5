// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Set or change the runtimeAnimatorController of an Animator Component to switch the animation playing.")]
	public class SetRuntimeAnimatorController : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[ObjectType(typeof(RuntimeAnimatorController))]
		[Tooltip("The new controller to insert into the Animator.")]
		public FsmObject setController;

		//[Tooltip("Change the Wrap Mode of the Animation (if it should play only once, forever, back and forth, ...).")]
		//public WrapMode _wrapMode;

		[Tooltip("Enable/Disable the component before playing.")]
		public FsmBool enable;

		[Tooltip("Repeat every frame. Useful when using normalizedTime to manually control the animation.")]
		public FsmBool everyFrame;

		Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			setController = null;
			enable = true;
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
			if(!everyFrame.Value)
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
			if(!_animator)
				return;

			_animator.enabled = enable.Value;

			_animator.runtimeAnimatorController = (RuntimeAnimatorController)setController.Value;
		}
	}
}