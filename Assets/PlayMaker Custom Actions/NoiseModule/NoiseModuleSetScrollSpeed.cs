﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made by : Ubik3D
// Forum Link : http://hutonggames.com/playmakerforum/index.php?topic=15580.0;topicseen

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken/NoiseModule")]
	[Tooltip("Set Strength of a particleSystem's NoiseModule ( Shuriken)")]
	public class NoiseModuleSetScrollSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(ParticleSystem))]
		[Tooltip("The GameObject with the particleSystem")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The noise Strength")]
		public FsmFloat scrollSpeed;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		GameObject _go;
		ParticleSystem _ps;
		ParticleSystem.NoiseModule _nm;

		public override void Reset()
		{
			gameObject = null;
			scrollSpeed = 1f;
			everyFrame = false;
		}


		public override void OnEnter()
		{
			_go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go==null)
			{
				return;
			}
			_ps = _go.GetComponent<ParticleSystem> ();
			if (_ps==null)
			{
				return;
			}
			_nm = _ps.noise;

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
			_nm.scrollSpeed = scrollSpeed.Value;
		}

	}
}