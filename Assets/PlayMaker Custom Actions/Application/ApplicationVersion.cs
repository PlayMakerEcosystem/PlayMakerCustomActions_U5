// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("gets the mobile application version and place into a string")]
	public class ApplicationVersion : FsmStateAction
	{
        [Tooltip("add an optional pre text, for example 'Alpha Version'")]
        public FsmString versionName;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString store;


        public override void Reset()
		{
            store = null;
            versionName = new FsmString() { UseVariable = true };
        }

		public override void OnEnter()
		{
#if (UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
			    store.Value = Application.version;

            if (versionName != null) store.Value = versionName.Value + " " + store.Value;
#else
            store.Value = "This is not a mobile" ;
#endif
                Finish();
		}
	}
}