// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Gets the RenderSettings Ambient Ground Color.")]
	public class GetAmbientGroundColor : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Ambient Ground Color")]
		public FsmColor ambientGroundColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			ambientGroundColor = null;
		}

		public override void OnEnter()
		{
			DoSetAmbientGroundColor();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoSetAmbientGroundColor();
		}


		void DoSetAmbientGroundColor()
		{
			ambientGroundColor.Value = RenderSettings.ambientGroundColor;
		}
	}
}