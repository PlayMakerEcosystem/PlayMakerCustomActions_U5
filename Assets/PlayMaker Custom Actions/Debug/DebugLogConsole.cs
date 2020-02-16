// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10048.0
// Keywords: Debug
// v2
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Sends a log message to Unity Console Log Window.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10048.0")]
	public class DebugLogConsole : FsmStateAction
	{
        [Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

        [Tooltip("Text to print to the PlayMaker log window.")]
		public FsmString text;

		public FsmBool pauseGame;

		private string fullLabel;
		private string name;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			text = "";
			pauseGame = false;
		}

		public override void OnEnter()
		{
			fullLabel = Fsm.GetFullFsmLabel(this.Fsm);
			name = Fsm.ActiveStateName;

			if (!string.IsNullOrEmpty(text.Value))


			switch (logLevel){
				case LogLevel.Info:
				Debug.Log(text.Value+"<color=#6B8E23ff>   *****   <b>Fsm</b> Path=  </color>"+fullLabel+" : "+name, this.Owner);
				break;

				case LogLevel.Error:
				Debug.LogError(text.Value+"<color=#6B8E23ff>   *****   <b>Fsm</b> Path=  </color>"+fullLabel+" : "+name, this.Owner);
				break;

				case LogLevel.Warning:
				Debug.LogWarning(text.Value+"<color=#6B8E23ff>   *****   <b>Fsm</b> Path=  </color>"+fullLabel+" : "+name, this.Owner);
				break;
			}

			if (pauseGame.Value == true){
				Debug.Break();
			}


			Finish();
		}
	}
}

