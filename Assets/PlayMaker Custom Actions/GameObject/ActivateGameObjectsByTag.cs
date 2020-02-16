// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Original Action : https://hutonggames.com/playmakerforum/index.php?topic=19971.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Activates/deactives all Game Object with a given tag.")]
	public class ActivateGameObjectsByTag : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Tag)]
        [Tooltip("The Tag")]
        public FsmString tag;

        [Tooltip("The active state to set all GameObject with this tag")]
        public FsmBool activate;

        GameObject[] gos;

        public override void Reset()
		{
            activate = null;
            tag = null;
		}

		public override void OnEnter()
		{
            if (activate.Value == false)
            {
                gos = GameObject.FindGameObjectsWithTag(tag.Value);

                foreach (GameObject go in gos)
                {
                    go.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.hideFlags != HideFlags.None)
                        continue;

#if UNITY_EDITOR
                    if (PrefabUtility.GetPrefabType(go) == PrefabType.Prefab || PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefab)
                        continue;
#endif

                    if (go.tag == tag.Value)
                    {
                        go.SetActive(true);
                    }
                }
            }

          

			Finish();
		}
	}
}