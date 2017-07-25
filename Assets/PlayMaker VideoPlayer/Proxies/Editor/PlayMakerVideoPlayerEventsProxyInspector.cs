using System;
using System.Collections;

using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlayMakerVideoPlayerEventsProxy))]
public class PlayMakerVideoPlayerEventsProxyInspector : UnityEditor.Editor {

	private SerializedObject m_object;

	PlayMakerVideoPlayerEventsProxy _target;

	GUIStyle controlLabelStyle; // for richText check

	public void OnEnable()
	{
		m_object = new SerializedObject(target);
		_target = (PlayMakerVideoPlayerEventsProxy)target;
	}

	public override void OnInspectorGUI()
	{
		// set style to use rich text.
		if (controlLabelStyle==null)
		{
			controlLabelStyle = GUI.skin.GetStyle("ControlLabel");
			controlLabelStyle.richText = true;
		}

		m_object.Update();



		EditorGUILayout.PropertyField(m_object.FindProperty("debug"));

		EditorGUILayout.PropertyField(m_object.FindProperty("videoPlayer"),new GUIContent("Video Player"), null);

		EditorGUILayout.PropertyField(m_object.FindProperty("eventTarget"));
			
		EditorGUILayout.PropertyField(m_object.FindProperty("onStarted"));

		EditorGUILayout.PropertyField(m_object.FindProperty("onErrorReceived"));

		EditorGUILayout.PropertyField(m_object.FindProperty("onFrameDropped"));

		EditorGUILayout.PropertyField(m_object.FindProperty("onFrameReady"));

		if (!_target.onFrameReady.isNone)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.LabelField( "<color=red>WARNING</color>",
							"<color=red>CPU Intense, use with care</color>"
						);
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.PropertyField(m_object.FindProperty("onLoopPointReached"));

		EditorGUILayout.PropertyField(m_object.FindProperty("onPrepareCompleted"));

		EditorGUILayout.PropertyField(m_object.FindProperty("onSeekCompleted"));




		m_object.ApplyModifiedProperties();

	}
}
