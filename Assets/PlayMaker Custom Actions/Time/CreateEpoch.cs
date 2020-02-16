// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Keywords: timestamp

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Time)]
[Tooltip("Create a epoch timestamp (Unix)")]
[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=13541.0")]
public class CreateEpoch : FsmStateAction
{
		[ActionSection("Output")]
		[Tooltip("Result of Unix epoch/timestamp")]
		public FsmInt getEpoch;

		[ActionSection("Offset in hours")]
		[Tooltip("Optional. Positive or Negative hours added")]
		public FsmInt offset;

		public override void Reset()
		{
			
			getEpoch = null;
			offset = 0;
		}

	public override void OnEnter()
	{
			TimeSpan t = new TimeSpan();
			double x = (int) offset.Value;
				
			t = DateTime.UtcNow.AddHours(x) - new DateTime(1970, 1, 1);
				getEpoch.Value = (int)t.TotalSeconds;


		Finish();
	}


}

}

