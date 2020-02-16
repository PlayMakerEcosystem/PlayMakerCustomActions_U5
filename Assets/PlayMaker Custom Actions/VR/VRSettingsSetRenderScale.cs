// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.VR;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VR")]
	[Tooltip("Alter the render scale. This controls the texel : pixel ratio before lens correction, meaning that we trade performance for sharpness. Higher numbers = better quality, but trades performance")]
	public class VRSettingsSetRenderScale : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The RenderScale of the VR. The render scale. Higher numbers = better quality, but trades performance")]
		public FsmFloat renderScale;

		public override void Reset()
		{
			renderScale = 1f;
		}

		public override void OnEnter()
		{
			VRSettings.renderScale = renderScale.Value;

			Finish ();
		}

	}
}