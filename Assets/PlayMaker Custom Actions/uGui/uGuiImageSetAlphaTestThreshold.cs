// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("uGui")]
    [Tooltip("Set The alpha test minimum threshold on a uGui image")]
    public class uGuiSetImageAlphaTestThreshold : ComponentAction<Image>
    {
        [RequiredField]
        [CheckForComponent(typeof(Image))]
        [Tooltip("The GameObject with the Image ui component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [HasFloatSlider(0, 1)]
        [Tooltip("The alpha threshold specifies the minimum alpha a pixel must have for the event to considered a \"hit\" on the Image.")]
        public FsmFloat alphaThreshold;

        Image _image;

        public override void Reset()
        {
            gameObject = null;
            alphaThreshold = 0.5f;
        }

        public override void OnEnter()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go != null)
            {
                _image = _go.GetComponent<UnityEngine.UI.Image>();
            }

            ExecuteAction();


            Finish();
        }

        void ExecuteAction()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                return;
            }

            this.cachedComponent.alphaHitTestMinimumThreshold = alphaThreshold.Value;

        }


    }
}