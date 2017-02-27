// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Get Build Scene Index
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Returns the index of the scene in the Build Settings. Always returns -1 if the scene was loaded through an AssetBundle..")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class GetBuildSceneIndex : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene Name")]
		public FsmString sceneName;
		 
		[ActionSection("Output")]
		public FsmInt index;


		private Scene target;

		public override void Reset()
		{
			sceneName = null;
			index = 0;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER


			target = SceneManager.GetSceneByName(sceneName.Value);

			index.Value = target.buildIndex;
		

			#else

			Debug.LogWarning("<b>[GetBuildSceneIndex]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}
			

	}
}
