// (c) copyright Hutong Games, LLC 2010-2019. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Convert)]
    [Tooltip("Store a Vector2 XY components into a Vector3 XY component. The Vector3 z component is also accessible for convenience")]
    public class ConvertVector2ToVector3 : FsmStateAction
    {

        [UIHint(UIHint.Variable)]
        [Tooltip("the vector2")]
        public FsmVector2 vector2;

        [UIHint(UIHint.Variable)]
        [Tooltip("the vector3")]
        public FsmVector3 vector3;

        [Tooltip("The vector3 z value")]
        public FsmFloat zValue;

        public bool everyFrame;

        public override void Reset()
        {
            vector2 = null;
            vector3 = null;
            everyFrame = false;

        }

        public override void OnEnter()
        {

            vector3.Value = new Vector3(vector2.Value.x, vector2.Value.y, zValue.Value);

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            vector3.Value = new Vector3(vector2.Value.x, vector2.Value.y, zValue.Value);
        }

    }
}