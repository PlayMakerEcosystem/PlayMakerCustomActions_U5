// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Particle system Shuriken


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Sets particleSystem main Module start size(s)")]
	public class MainModuleSetStartSize : FsmStateAction
	{
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;

		public ParticleSystemCurveMode mode;
		
		[HideIf("ModeIsNot_Constant")]
		[Tooltip("Start size")]
		public FsmFloat startSize;
		
		[HideIf("ModeIsNot_TwoConstant")]
		[Tooltip("Min start size. Leave to none for no effect")]
		public FsmFloat minStartSize;

		[HideIf("ModeIsNot_TwoConstant")]
		[Tooltip("Max start size. Leave to none for no effect")]
		public FsmFloat maxStartSize;

		public bool everyFrame;

		GameObject _go;
		ParticleSystem _ps;
		ParticleSystem.MainModule _mm;
		ParticleSystem.MinMaxCurve _mmc;


		public bool ModeIsNot_Constant()
		{
			return mode != ParticleSystemCurveMode.Constant;
		}
		
		public bool ModeIsNot_TwoConstant()
		{
			return mode != ParticleSystemCurveMode.TwoConstants;
		}
		
		public override void Reset()
		{
			gameObject = null;
			mode = ParticleSystemCurveMode.Constant;
			
			startSize = null;
			minStartSize  = null;
			maxStartSize  = null;
			
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
			_mm = _ps.main;

			_mmc = _mm.startSize;
			
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
			if (mode == ParticleSystemCurveMode.Constant)
			{
				_mmc.constant = startSize.Value;
			}else if (mode == ParticleSystemCurveMode.TwoConstants)
			{
				if (!minStartSize.IsNone) _mmc.constantMin = minStartSize.Value;
				if (!maxStartSize.IsNone) _mmc.constantMax = maxStartSize.Value;
			}

			_mm.startSize = _mmc;
		}


		public override string ErrorCheck()
		{
			if (mode == ParticleSystemCurveMode.Curve || mode == ParticleSystemCurveMode.TwoCurves)
			{
				return "Curve modes are not supported yet";
			}
			return "";
		}
	}
		
}
