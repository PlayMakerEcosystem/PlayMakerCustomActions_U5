// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Created by : Thore
// Url : http://hutonggames.com/playmakerforum/index.php?topic=19487.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Allows to change Body Type and Simulated parameters of Rigidbody.")]
    public class SetRigidBodyType2D : ComponentAction<Rigidbody>
    {
        [RequiredField]
        [CheckForComponent(typeof(Rigidbody2D))]
        public FsmOwnerDefault gameObject;

        public enum BodyType
        {
            Dynamic,
            Kinematic,
            Static,
            DontChange
        }

        public BodyType bodyType;

        public FsmBool simulated;

        private GameObject go;
        private Rigidbody2D rb;

        public override void Reset()
        {
            gameObject = null;
            simulated = null;
            bodyType = BodyType.DontChange;
        }
        public override void OnEnter()
        {
            go = Fsm.GetOwnerDefaultTarget(gameObject);
            rb = go.GetComponent<Rigidbody2D>();

            rb.simulated = simulated.Value;
            if (bodyType != BodyType.DontChange)
            {
                rb.bodyType = (RigidbodyType2D)bodyType;
            }
            Finish();
        }
    }
}