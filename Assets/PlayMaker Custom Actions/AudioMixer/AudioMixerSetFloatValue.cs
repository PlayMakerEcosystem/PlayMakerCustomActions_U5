// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// original actions from Tropical: http://hutonggames.com/playmakerforum/index.php?topic=11218.msg52963#msg52963
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;
using UnityEngine.Audio;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the float value of an exposed parameter for a Unity Audio Mixer.")]
	public class AudioMixerSetFloatValue : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AudioMixer))]
		[Tooltip("The Audio Mixer with the exposed parameter.")]
		public FsmObject theMixer;

		[RequiredField]
		[Tooltip("The name of the exposed parameter.")]
		[Title("Name of Parameter")]
		public FsmString exposedFloatName;

		[Title("Float Value")]
		public FsmFloat floatvalue;
		public bool everyFrame;
		
		public override void Reset()
		{
			theMixer = null;
			exposedFloatName = null;
			floatvalue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMixerFloatValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetMixerFloatValue();
		}
		
		public void DoSetMixerFloatValue()
		{
			var _TheMixer = theMixer.Value as AudioMixer;

			if (_TheMixer!=null && !string.IsNullOrEmpty(exposedFloatName.Value))
			{
				var _ExposedFloatName = exposedFloatName.Value;
				var _Floatvalue = floatvalue.Value;

				_TheMixer.SetFloat (_ExposedFloatName, _Floatvalue);
			}
		}
	}
}