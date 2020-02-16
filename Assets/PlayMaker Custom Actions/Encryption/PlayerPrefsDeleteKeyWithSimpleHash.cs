// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10526.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs Encryption")]
	[Tooltip("Removes key with hash and its corresponding value from the preferences.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10526.0")]
	public class PlayerPrefsDeleteKeyWithSimpleHash : FsmStateAction
	{
		public FsmString key;
		private FsmString tempString;
				
		public override void Reset()
		{
			key = "";
		}

		public override void OnEnter()
		{
			if(!key.IsNone && !key.Value.Equals("")) PlayerPrefs.DeleteKey(key.Value);
			tempString =key.Value+"_hash";
			if(!tempString.IsNone && !tempString.Value.Equals("")) PlayerPrefs.DeleteKey(tempString.Value);

			Finish();
		}
	}
}
