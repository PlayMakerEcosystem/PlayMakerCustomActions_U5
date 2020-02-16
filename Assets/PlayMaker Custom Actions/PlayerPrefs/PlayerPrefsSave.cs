// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Saves the Player Prefs. You should only need this on some platform like PS4")]
	public class PlayerPrefsSave : FsmStateAction
	{
		public override void OnEnter()
		{
            PlayerPrefs.Save();
			Finish();
		}
	}
}