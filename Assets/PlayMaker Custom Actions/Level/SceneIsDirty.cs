// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Scene Is Dirty
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Returns true if the scene is modifed.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SceneIsDirty : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene Name")]
		public FsmString sceneName;
		 
		[ActionSection("Output")]
		public FsmBool isDirty;
		[Tooltip("Event to send if the Bool variable is True.")]
		public FsmEvent isTrue;

		[Tooltip("Event to send if the Bool variable is False.")]
		public FsmEvent isFalse;

		[Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;

		private Scene target;

		public override void Reset()
		{
			sceneName = null;
			isDirty = false;
			isTrue = null;
			isFalse = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			isDirty.Value= false;

			target = SceneManager.GetSceneByName(sceneName.Value);

			isDirty.Value = target.isDirty;
		

			Fsm.Event(isDirty.Value ? isTrue : isFalse);

			if (!everyFrame.Value)
			{
				Finish();
			}


			#else

			Debug.LogWarning("<b>[SceneIsDirty]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}

		public override void OnUpdate()
		{
			isDirty.Value = target.isDirty;

			if (!everyFrame.Value)
			{
				Finish();
			}
		}


	}
}
