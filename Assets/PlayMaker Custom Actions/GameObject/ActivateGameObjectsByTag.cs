// License: Attribution 4.0 International (CC BY 4.0)
//Original Action : https://hutonggames.com/playmakerforum/index.php?topic=19971.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Activates/deactives all Game Object with a given tag.")]
	public class ActivateObjectsByTag : FsmStateAction
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
			 gos = GameObject.FindGameObjectsWithTag(tag.Value);
			
	        foreach (GameObject go in gos) 
			{
				go.SetActive(activate.Value);
	        }

			Finish();
		}
	}
}