// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
                        "Assets/PlayMaker Custom Plugins/Plugins/WebGL/PlayMakerOpenWindow.jslib"
                      ]
}
EcoMetaEnd
---*/

using System.Runtime.InteropServices;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Application)]
    [Tooltip("Opens a new browser window for a given url, works only in Webgl")]
    public class OpenWebglWindow : FsmStateAction
    {
        [Tooltip("The Url to open")]
        public FsmString url;

        public override void Reset()
        {
            url = null;
        }

        public override void OnEnter()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            openWindow(url.Value);
#else
            Application.OpenURL(url.Value);
#endif
        }

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void openWindow(string url);
#endif
    }
}