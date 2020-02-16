// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Gets the RenderSettings Ambient Intensity.")]
	public class GetAmbientIntensity : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Ambient Intensity")]
		public FsmFloat ambientIntensity;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			ambientIntensity = null;
		}

		public override void OnEnter()
		{
			DoGetValue();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoGetValue();
		}


		void DoGetValue()
		{
			ambientIntensity.Value = RenderSettings.ambientIntensity;
		}
	}
}