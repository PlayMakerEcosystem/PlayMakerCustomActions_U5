// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Open a Url link in the browser")]
	public class ApplicationOpenUrl : FsmStateAction
	{
		
		[RequiredField]
		public FsmString url;
		
		[Tooltip("When in webPlayer or webgl, will open a new window, define the name of that window here.")]
		public FsmString WebWindowTitle;
		
		public override void Reset()
		{
			url ="";
		}

        public override void OnEnter()
        {
#if UNITY_4 || UNITY_5
            if (Application.isWebPlayer)
            {
#if UNITY_IPHONE
#else
                Application.ExternalEval("window.open('" + url + "','" + WebWindowTitle.Value + "')");
#endif
            }
            else

#endif
            {
#if UNITY_WEBGL
       Application.ExternalEval("window.open('" + url + "','" + WebWindowTitle.Value + "')");
	           #else 
	            Application.OpenURL(url.Value);
#endif
	           
            }
            Finish();
        }
    }
}