// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken/TrailModule")]
	[Tooltip("Set the random constant value lifetime of a particleSystem's TrailModule ( Shuriken)")]
	public class TrailModuleSetRandomConstantLifetime : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(ParticleSystem))]
		[Tooltip("The GameObject with the particleSystem")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The minimum lifetime bound")]
		public FsmFloat minLifetime;

		[Tooltip("The maximum lifetime bound")]
		public FsmFloat maxLifetime;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		GameObject _go;
		ParticleSystem _ps;
		ParticleSystem.TrailModule _tm;
		ParticleSystem.MinMaxCurve _mmc;

		public override void Reset()
		{
			gameObject = null;
			minLifetime = 0f;
			maxLifetime = 1f;
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
			_tm = _ps.trails;

			_mmc = _tm.lifetime;

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
			if (_mmc.mode != ParticleSystemCurveMode.TwoConstants) {
				_mmc.mode = ParticleSystemCurveMode.TwoConstants;
			}

			_mmc.constantMin = minLifetime.Value;
			_mmc.constantMax = maxLifetime.Value;
			_tm.lifetime = _mmc;
		}

	}
}