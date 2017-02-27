// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Scene Is Valid
// require minimum 5.3.4

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Tells if the scene is valid.A scene can be invalid if you eg. tried to open a scene that does not exists in that case the scene returnen from EditorSceneManager.OpenScene would be invalid.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SceneIsValid : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene Name")]
		public FsmString sceneName;
		 
		[ActionSection("Output")]
		public FsmBool isValid;
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
			isValid = false;
			isTrue = null;
			isFalse = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			isValid.Value= false;

			target = SceneManager.GetSceneByName(sceneName.Value);

			isValid.Value = target.IsValid();
		

			Fsm.Event(isValid.Value ? isTrue : isFalse);

			if (!everyFrame.Value)
			{
				Finish();
			}


			#else

			Debug.LogWarning("<b>[SceneIsValid]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}

		public override void OnUpdate()
		{
			isValid.Value = target.IsValid();

			if (!everyFrame.Value)
			{
				Finish();
			}
		}


	}
}
