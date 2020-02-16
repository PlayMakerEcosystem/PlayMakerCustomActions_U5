// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken/TrailRenderer")]
	[Tooltip("Get the position of a vertex of a particleSystem's TrailRenderer ( Shuriken)")]
	public class TrailRendererGetPosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(TrailRenderer))]
		[Tooltip("The GameObject with the particleSystem")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The index of the vertex")]
		public FsmInt index;

		[Tooltip("The position at the given index")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 position;

		[UIHint(UIHint.Variable)]
		[Tooltip("false if index is not found")]
		public FsmBool outOfRange;

		[Tooltip("event sent if index is not found")]
		public FsmEvent outOfRangeEvent;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		GameObject _go;
		TrailRenderer _tr;

		public override void Reset()
		{
			gameObject = null;
			index = null;
			position = null;
			outOfRange = null;

			everyFrame = false;
		}


		public override void OnEnter()
		{
			_go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go==null)
			{
				return;
			}
			_tr = _go.GetComponent<TrailRenderer> ();
			if (_tr==null)
			{
				return;
			}

			DoExecute();

			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			DoExecute();
		}

		void DoExecute()
		{
			if (_tr == null) {
				return;
			}

			if (_tr.positionCount > 0 && index.Value >= 0 && index.Value < _tr.positionCount) {
				outOfRange.Value = true;
				position.Value = _tr.GetPosition (index.Value);
			} else {
				outOfRange.Value = false;
				Fsm.Event (outOfRangeEvent);
			}
		}

	}
}