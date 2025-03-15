using UnityEditor;
using UnityEngine;

[ CustomEditor( typeof( FollowPathComponent ) ) ]
public class FollowPathComponentEditor : Editor
{
	private const string              SetOriginButtonName = "Reset Points to local Origin";
	private       FollowPathComponent _component;
	private       SerializedProperty  _points;

	private void OnEnable()
	{
		_component = ( FollowPathComponent )target;
		_points    = serializedObject.FindProperty( "points" );
	}


	// updates the "points" array of the target component
	private void OnSceneGUI()
	{
		if( !_component )
			return;

		EditorGUI.BeginChangeCheck();

		for( int i = 0; i < _points.arraySize; i++ )
		{
			SerializedProperty point       = _points.GetArrayElementAtIndex( i );
			Vector3            position    = point.vector3Value;
			Vector3            newPosition = DrawHandle( position, i );

			point.vector3Value = newPosition;
		}

		if( EditorGUI.EndChangeCheck() )
			serializedObject.ApplyModifiedProperties();
	}

	// provides a button to reset the handle positions to the current target object
	public override void OnInspectorGUI()
	{
		if( !_component )
			return;

		serializedObject.Update();
		DrawDefaultInspector();

		if( GUILayout.Button( SetOriginButtonName ) )
		{
			Vector3 origin = _component.transform.position;

			foreach( SerializedProperty point in _points )
				point.vector3Value = origin;
		}

		serializedObject.ApplyModifiedProperties();
	}

	// draws handles that can be dragged and returns the updated position
	private static Vector3 DrawHandle( Vector3 position, int index )
	{
		float size = HandleUtility.GetHandleSize( position ) * 0.1f;

		Handles.color = new( 0.25f, 0.6f, 1.0f );
		Handles.DrawSolidDisc( position, Vector3.forward, size );

		Handles.color = Color.white;
		Handles.Label( position + new Vector3( size * -0.5f, 0, 0 ), index.ToString() );

		return Handles.FreeMoveHandle( position, size, Vector3.zero, Handles.CircleHandleCap );
	}
}