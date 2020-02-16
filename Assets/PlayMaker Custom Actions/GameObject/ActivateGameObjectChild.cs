// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10602.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Activates/deactivates a Game Object child with an exception. Use this to hide/show areas, or enable/disable many Behaviours at once.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10602.0")]
	public class ActivateGameObjectChild : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The Parent GameObject to activate/deactivate.")]
        public FsmOwnerDefault gameObject;

		[Tooltip("Except this child GameObject")]
		public FsmGameObject[] exceptThisChild;
		
		[RequiredField]
        [Tooltip("Check to activate, uncheck to deactivate Game Object.")]
        public FsmBool activate;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
		public bool everyFrame;

		GameObject activatedGameObject;
		bool equal;
		GameObject temp;

		public override void Reset()
		{
			gameObject = null;
			exceptThisChild = new FsmGameObject[1];
			activate = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoActivateGameObject();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoActivateGameObject();
		}



		void DoActivateGameObject()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go == null)
			{
				return;
			}
			    
             SetActiveRecursively(go, activate.Value);

        }


        public void SetActiveRecursively(GameObject go, bool state)
        {
			equal = false;

            foreach (Transform child in go.transform)
            {
				for(int i = 0; i<exceptThisChild.Length;i++){
					temp = exceptThisChild[i].Value;
					if (child.gameObject == temp) {
						 equal = true;
						break;
					}

					else{

						equal = false;
					}

				}

				if (equal == false) child.gameObject.SetActive(state);
				
				
            }
        } 

    }
}
