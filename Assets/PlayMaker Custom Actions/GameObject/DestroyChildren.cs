// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Destroys all children from the specified GameObject.")]
	public class DestroyChildren : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to destroy children from.")]
		public FsmOwnerDefault gameObject;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			DoDestroyChildren(Fsm.GetOwnerDefaultTarget(gameObject));

			Finish();
		}

		public void DoDestroyChildren(GameObject go)
		{
			if(go != null)
			{
				Transform t = go.transform;

				bool isPlaying = Application.isPlaying;

				while(t.childCount != 0)
				{
					Transform child = t.GetChild(0);

					if(isPlaying)
					{
						child.parent = null;
						UnityEngine.Object.Destroy(child.gameObject);
					} else
						UnityEngine.Object.DestroyImmediate(child.gameObject);
				}
			}
		}
	}
}
