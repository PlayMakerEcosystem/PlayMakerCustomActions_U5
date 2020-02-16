// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("gets the application internet Reachability status")]
	public class ApplicationInternetReachability : FsmStateAction
	{
		[Tooltip("true if internet is deemed available")]
		[UIHint(UIHint.Variable)]
        public FsmBool reachable;
        
        [Tooltip("the actual networkReachability value")]
        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(UnityEngine.NetworkReachability))]
        public FsmEnum networkReachability;

        [Tooltip("event fired when internet is reachable")]
        public FsmEvent reachableEvent;
        
        [Tooltip("event fired when internet is reachable via carrier data")]
        public FsmEvent CarrierDataNetworkEvent;
        
        [Tooltip("event fired when internet is reachable via local network")]
        public FsmEvent LocalAreaNetworkEvent;
        
        [Tooltip("event fired when internet is not reachable")]
        public FsmEvent notReachableEvent;

        private NetworkReachability _reachability;
        bool _reachable;
        
        public override void Reset()
        {
	        reachable = null;
	        networkReachability = null;
	        CarrierDataNetworkEvent = null;
	        LocalAreaNetworkEvent = null;
	        notReachableEvent = null;
        }

		public override void OnEnter()
		{

			_reachability =	Application.internetReachability;
			networkReachability.Value = _reachability;

		
			_reachable = _reachability != NetworkReachability.NotReachable;

			if (!reachable.IsNone) reachable.Value = _reachable;
			
			this.Fsm.Event(_reachable ? reachableEvent : notReachableEvent);

			if (CarrierDataNetworkEvent !=null && _reachability == NetworkReachability.ReachableViaCarrierDataNetwork)
				this.Fsm.Event(CarrierDataNetworkEvent);
			
			if (LocalAreaNetworkEvent !=null && _reachability == NetworkReachability.ReachableViaLocalAreaNetwork)
				this.Fsm.Event(LocalAreaNetworkEvent);
			
			Finish();
		}
	}
}