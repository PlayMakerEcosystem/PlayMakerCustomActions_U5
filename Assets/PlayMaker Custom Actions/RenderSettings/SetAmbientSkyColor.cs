// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the RenderSettings Ambient Sky Color.")]
	public class SetAmbientSkyColor : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Ambient Sky Color")]
		public FsmColor ambientSkyColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;


		public override void Reset()
		{
			ambientSkyColor = null;
			everyFrame = false;
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
			RenderSettings.ambientSkyColor = ambientSkyColor.Value;
		}
	}
}