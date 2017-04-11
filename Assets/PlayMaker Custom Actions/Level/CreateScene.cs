// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Create Scene
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Create an empty new scene with the given name additively")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class CreateScene : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene name")]
		public FsmString stringInput;
		 
	
		public override void Reset()
		{
			stringInput = null;
		
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			SceneManager.CreateScene(stringInput.Value);
			Finish();

			#else

			Debug.LogWarning("<b>[CreateScene]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
