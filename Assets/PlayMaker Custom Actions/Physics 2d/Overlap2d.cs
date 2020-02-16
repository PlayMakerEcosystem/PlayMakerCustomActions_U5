// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Digitom: https://hutonggames.com/playmakerforum/index.php?topic=18337.0;topicseen
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Physics 2d/Editor/Overlap2dEditor.cs"
					  ]
}
EcoMetaEnd
---*/

using System;
using UnityEngine;
using HutongGames.PlayMaker;


namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Bool check to see if the overlap is intersecting with other colliders. ")]
	public class Overlap2d : FsmStateAction
	{

        public enum ShapeType { Circle, Box, Area, Capsule, Point }

        [Tooltip("Position of the overlap shape. default position will be owner's transform.")]
        public FsmOwnerDefault position;

        [Tooltip("Offset position of shape.")]
        public FsmVector2 offset;

        [Tooltip("Type of overlap shape.")]
        public ShapeType shape;

        //CIRCLE
        [Tooltip("Size of the circle.")]
        public FsmFloat radius;

        //BOX
        [Tooltip("Size of the box.")]
        public FsmVector2 boxSize;

        //AREA
        [Tooltip("One corner of the rectangle.")]
        public FsmVector2 pointA;
        [Tooltip("Diagonally opposite the point A corner of the rectangle.")]
        public FsmVector2 pointB;

        //CAPSULE
        [Tooltip("Size of the capsule.")]
        public FsmVector2 size;
        [Tooltip("Direction of the capsule.")]
        public CapsuleDirection2D direction;

        //POINT
        [Tooltip("point in space.")]
        public FsmVector2 point;

        //MUTUAL EFFECTORS
        [Tooltip("Only include objects with a Z coordinate (depth) greater than or equal to this value.")]
        public FsmFloat minDepth;
        [Tooltip("Only include objects with a Z coordinate (depth) less than or equal to this value.")]
        public FsmFloat maxDepth;

        [Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;

        [Tooltip("Store all GameObjects in the overlap area.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.GameObject)]
        public FsmArray storeGameObjects;

        [Tooltip("Repeat every frame. Typically this would be set to True.")]
        public bool everyFrame;
		
		[UIHint(UIHint.Layer)]
		[Tooltip("Detect only from these layers.")]
		public FsmInt layerMask;
		
		[Tooltip("Draw a debug shape. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;

        [Tooltip("The color to use for the debug line.")]
        public FsmColor debugColor;

        private Vector2 pos;
        private Collider2D hit;
        private Collider2D[] collidedObjects;

		public override void Reset()
		{

			position = null;
            shape = ShapeType.Box;
			storeDidHit = null;
            storeGameObjects = null;
			layerMask = new FsmInt();
			debugColor = Color.cyan;
			debug = true;
		}
		
		public override void OnEnter()
		{

			DoOverlap();

            if (!everyFrame)
            {
                Finish();
            }

        }

        public override void OnUpdate()
        {
            DoOverlap();
        }

        private void DoOverlap()
		{

            var go = Fsm.GetOwnerDefaultTarget(position);
            if (go)
            {
                pos = go.transform.position;
            }

            switch (shape)
            {
                case ShapeType.Circle:
                    collidedObjects = Physics2D.OverlapCircleAll(pos + offset.Value, radius.Value, layerMask.Value,minDepth.Value,maxDepth.Value);
                    break;
                case ShapeType.Box:
                    collidedObjects = Physics2D.OverlapBoxAll(pos + offset.Value, boxSize.Value, 0, layerMask.Value, minDepth.Value, maxDepth.Value);
                    break;
                case ShapeType.Area:
                    collidedObjects = Physics2D.OverlapAreaAll(pos + pointA.Value, pos + pointB.Value, layerMask.Value, minDepth.Value, maxDepth.Value);
                    break;
                case ShapeType.Capsule:
                    collidedObjects = Physics2D.OverlapCapsuleAll(pos + offset.Value, size.Value, direction , 0, layerMask.Value, minDepth.Value, maxDepth.Value);
                    break;
                case ShapeType.Point:
                    collidedObjects = Physics2D.OverlapPointAll(pos + point.Value, layerMask.Value, minDepth.Value, maxDepth.Value);
                    break;
                default:
                    break;
            }

            if (collidedObjects.Length > 0)
            {
                storeDidHit.Value = true;
                storeGameObjects.Values = new GameObject[collidedObjects.Length];
                for (int i = 0; i < storeGameObjects.Values.Length; i++)
                {
                    storeGameObjects.Values[i] = collidedObjects[i].gameObject;
                }
            }
            else
            {
                storeDidHit.Value = false;
            }

        }
    }
}

