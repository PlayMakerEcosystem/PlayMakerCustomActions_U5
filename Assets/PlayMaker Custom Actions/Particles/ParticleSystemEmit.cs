// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=16359.msg76389#msg76389
// Keywords: Shuriken

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("particles")]
	[Tooltip("Emits a given number of particle from a Particles System. NOTE: You need to set the emit of this particle system to 0 at the editor, so it will not emit at all during play-mode by itself")]
	public class ParticleSystemEmit : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the particleSystem to play.")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Emit that much particles")]
		public FsmInt countParticules;

		[Tooltip("Repeat every frames")]
		public bool everyFrame;

		GameObject go;
		GameObject previousGo;
		ParticleSystem _ps;

		public override void Reset()
		{
			gameObject = null;
			countParticules = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			emitirParticulas();
			if (!everyFrame) Finish();
		}

		public override void OnUpdate()
		{
			emitirParticulas();	
		}


		public void emitirParticulas()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go != previousGo)
			{
				previousGo = go;
				_ps = go.GetComponent<ParticleSystem>();
			}

			if (_ps == null) return;

			_ps.Emit (countParticules.Value);
		}
	}
}
