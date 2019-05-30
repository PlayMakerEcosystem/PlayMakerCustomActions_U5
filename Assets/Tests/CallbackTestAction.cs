
// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	public class CallbackTestAction : FsmStateAction
	{


		public override void OnEnter()
		{
			CallbackTest.Instance.TriggerSomething(MyCallback);
		}

		void MyCallback(string value)
		{
			UnityEngine.Debug.Log("CallbackTestAction got called back! : " +value);
		}
	}
}