// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10114.0
// Keywords: Wait LateUpdate

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Time)]
	[Tooltip("LateUpdate is called every frame. LateUpdate is called after all Update functions have been called. This is useful for order execution.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10114.0")]
    public class WaitLateUpdate : FsmStateAction
    {
  
		public FsmEvent finishEvent;

        public override void Reset()
        {
 
            finishEvent = null;
        }

        public override void OnEnter()
        {

        }

		public override void OnLateUpdate()
        {
			if (finishEvent != null)   
			{
                    Fsm.Event(finishEvent);
                }

			Finish();
        }

    }
}

