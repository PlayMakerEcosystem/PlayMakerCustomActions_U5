// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

using Com.InkleStudios;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Prototype")]
    [ActionTarget(typeof(GameObject), "gameObject", true)]
	[Tooltip("Disable a Prototype Object, returning it to its pool. use OnReturntoPool Event action to get an event from a return call.")]
	public class PrototypeOnReturnToPool : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Prototype))]
		[Tooltip("GameObject to create. Must be a Prototype")]
		public FsmOwnerDefault gameObject;

		GameObject _go;
		Prototype _proto;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			_go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (_go != null) {
				_proto = _go.GetComponent<Prototype> ();
			}

			if (_proto != null)
			{
				_proto.ReturnToPool ();
			}

			Finish ();
		}
	}
}