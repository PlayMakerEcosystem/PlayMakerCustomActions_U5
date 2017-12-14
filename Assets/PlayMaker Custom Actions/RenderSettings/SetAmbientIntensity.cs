// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the RenderSettings Ambient Intensity.")]
	public class SetAmbientIntensity : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Ambient Intensity")]
		public FsmFloat ambientIntensity;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;


		public override void Reset()
		{
			ambientIntensity = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetValue();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoSetValue();
		}

		
		void DoSetValue()
		{
			RenderSettings.ambientIntensity = ambientIntensity.Value;
		}
	}
}