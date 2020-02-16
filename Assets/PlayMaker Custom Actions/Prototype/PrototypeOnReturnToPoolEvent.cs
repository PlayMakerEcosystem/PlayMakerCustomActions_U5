// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

using Com.InkleStudios;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Prototype")]
    [ActionTarget(typeof(GameObject), "gameObject", true)]
	[Tooltip("Watch OnReturntoPool Event. If on the instance itself, you'll have one frame before the instance is disabled")]
	public class PrototypeOnReturnToPoolEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Prototype))]
		[Tooltip("GameObject to create. Must be a Prototype")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Event Sent when gameobject is returned to pool")]
		public FsmEvent OnReturnToPool;


		GameObject _go;
		Prototype _proto;
		Transform _t;

		public override void Reset()
		{
			gameObject = null;
			OnReturnToPool = null;
		}

		public override void OnEnter()
		{
			_go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (_go != null) {
				_proto = _go.GetComponent<Prototype> ();
			}

			if (_proto != null)
			{
				_proto.OnReturnToPool += _proto_OnReturnToPool;

				if (_go == this.Owner) {
					_proto.OneFrameReturnDelay = true;
				}
			}


		}

		public override void OnExit ()
		{
			if (_proto != null)
			{
				_proto.OnReturnToPool -= _proto_OnReturnToPool;
			}
		}

		void _proto_OnReturnToPool ()
		{
			Fsm.Event(OnReturnToPool);
			Finish ();
		}

	}
}