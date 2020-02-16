// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Created by : Thore
// Url : http://hutonggames.com/playmakerforum/index.php?topic=19487.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Allows to change Body Type and Simulated parameters of Rigidbody.")]
    public class SetRigidBodyType2D : ComponentAction<Rigidbody2D>
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

        public override void Reset()
        {
            gameObject = null;
            simulated = null;
            bodyType = BodyType.DontChange;
        }
        public override void OnEnter()
        {
            if (this.UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
             
                this.cachedComponent.simulated = simulated.Value;
                if (bodyType != BodyType.DontChange)
                {
                    this.cachedComponent.bodyType = (RigidbodyType2D)bodyType;
                }
            }
            Finish();
        }
    }
}