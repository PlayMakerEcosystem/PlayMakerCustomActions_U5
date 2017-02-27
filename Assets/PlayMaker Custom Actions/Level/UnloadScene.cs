// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Unload Scene
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Unloads all GameObjects associated with the given scene. Note that assets are currently not unloaded, in order to free up asset memory call Resources.UnloadAllUnusedAssets")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class UnloadScene : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene name")]
		public FsmString name;
		public FsmBool isUnloaded;
	
		private Scene sceneTarget;

		public override void Reset()
		{
			name = null;
			isUnloaded = false;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			isUnloaded.Value = false;

			isUnloaded.Value = SceneManager.UnloadScene(name.Value);

			if (isUnloaded.Value == true){
				Finish();
			}
	

			#else
			isLoaded.Value = false;
			Debug.LogWarning("<b>[UnloadScene]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}

		public override void OnUpdate()
		{
			isUnloaded.Value = SceneManager.UnloadScene(name.Value);
			if (isUnloaded.Value == true){
				Finish();
			}

		}
	}
}
