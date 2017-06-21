// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Shuriken


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Play particles. ")]
    public class particleSystemPlay: FsmStateAction
	{
		[RequiredField]
		[ActionSection("Setup")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;

		public FsmBool withChildren;

		private ParticleSystem ps;
		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			withChildren = false;
		}


		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				ps = go.GetComponent<ParticleSystem> ();
				if (ps != null)
				{
					ps.Play (withChildren.Value);
				}
			}
		}

	}
}

