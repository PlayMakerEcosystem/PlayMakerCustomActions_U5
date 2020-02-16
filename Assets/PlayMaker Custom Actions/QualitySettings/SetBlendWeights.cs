// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10565.0

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("QualitySettings")]
	[Tooltip("The Blend weights can be set to either One Bone, Two Bones or Four Bones.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10565.0")]
	public class SetBlendWeights : FsmStateAction
	{
		
		[UIHint(UIHint.FsmInt)]
		public FsmInt level;
		

		public override void Reset()
		{
			level = 2;
		}
		
		public override void OnEnter()
		{

			if (level.Value != 1 || level.Value != 2 || level.Value != 4){

				Debug.LogError("The BlendWeights can be set to either 1,2 or 4");
				Finish();

			}

			if (level.Value == 1) QualitySettings.blendWeights = BlendWeights.OneBone;
			if (level.Value == 2) QualitySettings.blendWeights = BlendWeights.TwoBones;
			if (level.Value == 4) QualitySettings.blendWeights = BlendWeights.FourBones;

				Finish();
			
		}

		
	}
}
