// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get an AudioClip based on the value of an Integer Variable.")]
	public class IntSwitchAudioClip : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		[CompoundArray("Int Switches", "Compare Int", "Assigned Audio Clip")]
		public FsmInt[] compareTo;
		[ObjectType(typeof(AudioClip))]
		public FsmObject[] assignedAudioClip;

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(AudioClip))]
		public FsmObject result;
		public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			compareTo = new FsmInt[1];
			assignedAudioClip = new FsmObject[1];
			result = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIntSwitch();

			if(!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoIntSwitch();
		}

		void DoIntSwitch()
		{
			if(intVariable.IsNone)
				return;

			for(int i = 0; i < compareTo.Length; i++)
			{
				if(intVariable.Value == compareTo[i].Value)
				{
					result = assignedAudioClip[i];
					return;
				}
			}
		}
	}
}
