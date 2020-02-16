// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Gets the RenderSettings Ambient Sky Color.")]
	public class GetAmbientSkyColor : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Ambient Sky Color")]
		public FsmColor ambientSkyColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			ambientSkyColor = null;
		}

		public override void OnEnter()
		{
			DoSetAmbientSkyColor();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoSetAmbientSkyColor();
		}


		void DoSetAmbientSkyColor()
		{
			ambientSkyColor.Value = RenderSettings.ambientSkyColor;
		}
	}
}