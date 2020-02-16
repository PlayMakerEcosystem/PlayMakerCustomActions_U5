// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMakerEditor
{
    [CustomActionEditor(typeof(Overlap2d))]
    public class Overlap2dEditor : CustomActionEditor
    {
        private Vector2 pos;
        private GameObject go;
        private Overlap2d action;

        //Area vars
        private Vector2 handleAreaPointA;
        private Vector2 handleAreaPointB;
        private Vector2 center;
        private Vector2 size;
        private float sizeX;
        private float sizeY;

        //Capsule vars
        private Vector2 disc1Center;
        private Vector2 disc2Center;
        private float discDistance;
        private Vector2 capEdgeSize;

        //point vars
        private Vector2 handlePoint;


        public override void OnEnable()
        {
            base.OnEnable();

            action = target as Overlap2d;

            if (action.Fsm != null)
                go = action.Fsm.GetOwnerDefaultTarget(action.position);


            if (go)
            {
                pos = go.transform.position;
                //Initial setup for Area points
                handleAreaPointA = action.pointA.Value + pos;
                handleAreaPointB = action.pointB.Value + pos;
                //Initial setup for point
                handlePoint = action.point.Value + pos;
            }

        }

        public override bool OnGUI()
        {
            GUILayout.Label("Setup:", EditorStyles.boldLabel);
            EditField("position");

            GUILayout.Space(10);

            EditField("shape");

            switch (action.shape)
            {
                case Overlap2d.ShapeType.Circle:
                    EditField("radius");
                    EditField("offset");
                    break;
                case Overlap2d.ShapeType.Box:
                    EditField("boxSize");
                    EditField("offset");
                    break;
                case Overlap2d.ShapeType.Area:
                    EditField("pointA");
                    EditField("pointB");
                    break;
                case Overlap2d.ShapeType.Capsule:
                    EditField("direction");
                    EditField("size");
                    EditField("offset");
                    break;
                case Overlap2d.ShapeType.Point:
                    EditField("point");
                    break;
            }

            GUILayout.Space(10);

            EditField("minDepth");
            EditField("maxDepth");

            GUILayout.Label("Results:", EditorStyles.boldLabel);
            EditField("storeDidHit");
            EditField("storeGameObjects");
            EditField("everyFrame");

            GUILayout.Label("Filter:", EditorStyles.boldLabel);
            int temp = EditorGUILayout.MaskField("Mask", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(action.layerMask.Value), InternalEditorUtility.layers);
            action.layerMask.Value = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(temp);

            GUILayout.Label("Debug:", EditorStyles.boldLabel);
            EditField("debug");
            if (action.debug.Value)
                EditField("debugColor");


            return GUI.changed;

        }

        public override void OnSceneGUI()
        {

            if (!go || !action.debug.Value)
                return;

            Handles.color = action.debugColor.Value;

            switch (action.shape)
            {
                case Overlap2d.ShapeType.Circle:
                    Handles.DrawWireDisc(pos + action.offset.Value, Vector3.back, action.radius.Value);
                    break;
                case Overlap2d.ShapeType.Box:
                    Handles.DrawWireCube(pos + action.offset.Value, action.boxSize.Value);
                    break;
                case Overlap2d.ShapeType.Area:
                    DrawAreaShape();
                    break;
                case Overlap2d.ShapeType.Capsule:
                    DrawCapsuleShape();
                    break;
                case Overlap2d.ShapeType.Point:
                    DrawPointShape();
                    break;
            }

            pos = go.transform.position;
        }

        void DrawCapsuleShape()
        {
            float discRadius = action.size.Value.x * 0.5f;
            if (action.direction == CapsuleDirection2D.Vertical)
            {
                //disc 1
                disc1Center = new Vector2(action.offset.Value.x + pos.x, (action.offset.Value.y + pos.y) + (action.size.Value.y * 0.5f));
                //disc2
                disc2Center = new Vector2(action.offset.Value.x + pos.x, (action.offset.Value.y + pos.y) - (action.size.Value.y * 0.5f));
                //rectangle
                discDistance = Vector2.Distance(disc1Center, disc2Center);
                capEdgeSize = new Vector2(action.size.Value.x, discDistance);
            }
            else
            {
                //disc 1
                disc1Center = new Vector2((action.offset.Value.x + pos.x) + (action.size.Value.y * 0.5f), action.offset.Value.y + pos.y);
                //disc2
                disc2Center = new Vector2((action.offset.Value.x + pos.x) - (action.size.Value.y * 0.5f), action.offset.Value.y + pos.y);
                //rectangle
                discDistance = Vector2.Distance(disc1Center, disc2Center);
                capEdgeSize = new Vector2(discDistance, action.size.Value.x);
            }
            Handles.DrawWireDisc(disc1Center, Vector3.back, discRadius);
            Handles.DrawWireDisc(disc2Center, Vector3.back, discRadius);
            Handles.DrawWireCube(action.offset.Value + pos, capEdgeSize);

        }

        void DrawAreaShape()
        {
            EditorGUI.BeginChangeCheck();
            //have handles follow go posiiton if we move it in the scene
            if (handleAreaPointA != action.pointA.Value || handleAreaPointB != action.pointB.Value)
            {
                handleAreaPointA = Handles.PositionHandle(action.pointA.Value + pos, Quaternion.identity);
                handleAreaPointB = Handles.PositionHandle(action.pointB.Value + pos, Quaternion.identity);
            }
            else //move the handles normally in the scene
            {
                handleAreaPointA = Handles.PositionHandle(handleAreaPointA, Quaternion.identity);
                handleAreaPointB = Handles.PositionHandle(handleAreaPointB, Quaternion.identity);
            }

            if (EditorGUI.EndChangeCheck())//update the scipt values after dragging
            {
                action.pointA.Value = handleAreaPointA - pos;
                action.pointB.Value = handleAreaPointB - pos;
            }

            //draw pin points on the corners
            Handles.DrawSolidDisc(pos + action.pointA.Value, Vector3.back, 0.03f);
            Handles.DrawSolidDisc(pos + action.pointB.Value, Vector3.back, 0.03f);

            //parameters for the cube layout
            center = Vector2.Lerp(pos + action.pointA.Value, pos + action.pointB.Value, 0.5f);
            sizeX = Vector2.Distance(Vector2.left * action.pointA.Value.x, Vector2.left * action.pointB.Value.x);
            sizeY = Vector2.Distance(Vector2.up * action.pointA.Value.y, Vector2.up * action.pointB.Value.y);
            size = new Vector2(sizeX, sizeY);

            //draw the cube
            Handles.DrawWireCube(center, size);
        }

        void DrawPointShape()
        {
            EditorGUI.BeginChangeCheck();
            if (handlePoint != action.point.Value) //point follows go if we move go
                handlePoint = Handles.PositionHandle(action.point.Value + pos, Quaternion.identity);
            else //move point normally in scene view
                handlePoint = Handles.PositionHandle(handlePoint, Quaternion.identity);

            if (EditorGUI.EndChangeCheck()) //update script values after dragging
                action.point.Value = handlePoint - pos;

            //draw a cross-hair to see the point better
            Handles.DrawLine(pos + action.point.Value - (Vector2.left * 0.3f), pos + action.point.Value + (Vector2.left * 0.3f));
            Handles.DrawLine(pos + action.point.Value - (Vector2.up * 0.3f), pos + action.point.Value + (Vector2.up * 0.3f));
            Handles.DrawWireDisc(pos + action.point.Value, Vector3.back, 0.075f);
        }
    }
}
