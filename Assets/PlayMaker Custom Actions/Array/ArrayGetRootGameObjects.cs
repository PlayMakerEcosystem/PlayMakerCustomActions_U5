// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Array Get Root GameObjects
// require minimum 5.3

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
#define UNITY_PRE_5_3
#endif

using UnityEngine;
#if !UNITY_PRE_5_3
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
	[Tooltip("Get all the root GameObjects in the scene. Wait two frames for scene to load all objects for accurate results.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class ArrayGetRootGameObjects : FsmStateAction
    {
        [RequiredField] 
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array Variable to use.")] 
        public FsmArray array;

		[ActionSection("Scene Setup")]

		[RequiredField]
		[Tooltip("Scene Name")]
		public FsmString sceneName;

		private Scene target;


        public override void Reset()
        {
            array = null;
			sceneName = null;

        }

        public override void OnEnter()
        {

			#if UNITY_PRE_5_3
			Debug.LogWarning("<b>[ArrayGetRootGameObjects]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif


			target = SceneManager.GetSceneByName(sceneName.Value);


			DoAction();
		

			Finish();
        }



		private void DoAction()
        {

			GameObject[] rootGameObjects = target.GetRootGameObjects();

			int length = rootGameObjects.Length;
		
			array.Resize(length);

			length = length-1;

			for(int x = 0; x <=length; x++)
			{  
				
				array.Set(x, rootGameObjects[x]);

			}

			return;

        }
			

    }
}

