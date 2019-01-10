// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// License: Attribution 4.0 International (CC BY 4.0)
// Author: nFigther: http://hutonggames.com/playmakerforum/index.php?topic=15643.0

/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
                        "Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
                      ]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Gets the Rotation of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
    public class GetNegativeRotation : FsmStateActionAdvanced
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.Variable)]
        public FsmFloat xAngle;
        [UIHint(UIHint.Variable)]
        public FsmFloat yAngle;
        [UIHint(UIHint.Variable)]
        public FsmFloat zAngle;

        GameObject _go;
        Vector3 _rotation;

        public override void Reset()
        {
            gameObject = null;
            xAngle = null;
            yAngle = null;
            zAngle = null;
        }

        public override void OnActionUpdate()
        {
            DoGetRotation();
        }

        void DoGetRotation()
        {
            _go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (_go == null)
            {
                return;
            }

            _rotation = _go.transform.localEulerAngles;

            if (!xAngle.IsNone)
            {
                if (_rotation.x > 180)
                {
                    xAngle.Value = -(360 - _rotation.x);
                }
                else
                {
                    xAngle.Value = _rotation.x;
                }
            }

            if (!yAngle.IsNone)
            {
                if (_rotation.y > 180)
                {
                    yAngle.Value = -(360 - _rotation.y);
                }
                else
                {
                    yAngle.Value = _rotation.y;
                }
            }

            if (!zAngle.IsNone)
            {
                if (_rotation.z > 180)
                {
                    zAngle.Value  = -(360 - _rotation.z);
                }
                else
                {
                    zAngle.Value = _rotation.z;
                }
            }

        }


    }
}