using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelDesigner))]
public class LevelDesignerEditor : Editor {

    [MenuItem("Game Data/Level/LevelDesigner")]
    public static void ShowLevelDesigner()
    {
        GameObject go = new GameObject();
        go.name = "Level Designer";
        go.AddComponent<LevelDesigner>();
        GameObject[] selected = new GameObject[1];
        selected[0] = go;
        Selection.objects = selected;
    }

	LevelDesigner script;
	BatchMode batchmode = BatchMode.None;
    //bool leftControl;
	Vector2 oldTilePos = new Vector2();
	
	enum BatchMode
	{
		Create,
		Delete,
		None
	}
	
	void OnEnable()
	{
		script = (LevelDesigner) target;
		
		if(!Application.isPlaying)
		{
			if (SceneView.lastActiveSceneView != null)
			{
				Tools.current = Tool.View;
				SceneView.lastActiveSceneView.orthographic = true;
				SceneView.lastActiveSceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot,Quaternion.identity);
				batchmode = BatchMode.None;
			}
		}
	}
	
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel ("Prefab");
		script.prefab = (GameObject) EditorGUILayout.ObjectField(script.prefab,typeof(GameObject), false);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel ("Depth");
		script.depth = EditorGUILayout.Slider (script.depth,-5,5);
		EditorGUILayout.EndHorizontal();
		
		script.rotation = EditorGUILayout.Vector3Field("Rotation", script.rotation);
		
		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}
	
	void OnSceneGUI()
	{
		Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
		Vector2 tilePos = new Vector2();
		tilePos.x = Mathf.RoundToInt(ray.origin.x);
		tilePos.y = Mathf.RoundToInt(ray.origin.y);
		
		if (tilePos != oldTilePos)
		{
			script.gizmoPosition = tilePos;
			SceneView.RepaintAll();
			oldTilePos = tilePos;
		}
		
		Event current = Event.current;
		if (current.keyCode == KeyCode.C)
		{
			if (current.type == EventType.keyDown)
			{
				batchmode = BatchMode.Create;
			}
			else if (current.type == EventType.KeyUp)
			{
				batchmode = BatchMode.None;
			}
		}
		
		if (current.keyCode == KeyCode.D)
		{
			if (current.type == EventType.keyDown)
			{
				batchmode = BatchMode.Delete;
			}
			else if (current.type == EventType.KeyUp)
			{
				batchmode = BatchMode.None;
			}
		}
		
		if ((current.type == EventType.mouseDown) || (batchmode != BatchMode.None))
		{
			string name = string.Format(script.prefab.name + "_{0}_{1}_{2}", script.depth,tilePos.y,tilePos.x);
			if ((current.button == 0) || (batchmode == BatchMode.Create))
			{
				//Create
				CreateTile (tilePos, name);
			}
		
			if ((current.button == 1) || (batchmode == BatchMode.Delete))
			{
				//Delete
				DeleteTile (name);
			}
		
			if (current.type == EventType.mouseDown)
			{
				Tools.current = Tool.View;
				SceneView.lastActiveSceneView.orthographic = true;
				SceneView.lastActiveSceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot,Quaternion.identity);
			}
		}
		
		SetGizmosColor();
		
		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}

	void CreateTile(Vector2 tilePos, string name)
	{
		if (!GameObject.Find (name))
		{
			Vector3 pos = new Vector3(tilePos.x, tilePos.y,script.depth);
			Quaternion quat = new Quaternion();
			quat.eulerAngles = script.rotation;
			GameObject go = (GameObject) Instantiate (script.prefab,pos, quat);
			go.name = name;
		}
	}

	void DeleteTile(string name)
	{
		GameObject go = GameObject.Find (name);

        if (go != null)
            DestroyImmediate(go);
	}

	void SetGizmosColor()
	{
		switch (batchmode)
		{
			case BatchMode.None:
				script.gizmosColor = Color.grey;
				break;
			case BatchMode.Create:
				script.gizmosColor = Color.white;
				break;
			case BatchMode.Delete:
				script.gizmosColor = Color.red;
				break;
		}
	}
}