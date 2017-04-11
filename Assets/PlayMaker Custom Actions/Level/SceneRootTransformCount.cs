// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Scene Root Transform Count
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("The number of root transforms of this scene.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SceneRootTransformCount : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene Name")]
		public FsmString sceneName;
		 
		[ActionSection("Output")]
		public FsmInt count;


		private Scene target;

		public override void Reset()
		{
			sceneName = null;
			count = 0;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER


			target = SceneManager.GetSceneByName(sceneName.Value);

			count.Value = target.rootCount;
		
		

			#else

			Debug.LogWarning("<b>[SceneRootTransformCount]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
