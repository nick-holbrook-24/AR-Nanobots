using UnityEditor;

[CustomEditor(typeof(LevelDesignManager))]
public class LevelDesignManagerEditor : Editor
{
    private SerializedProperty levelDesignData;
    private SerializedProperty entityFactory;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(levelDesignData);

        EditorGUILayout.PropertyField(entityFactory);
        if (entityFactory.objectReferenceValue != null)
        {
            if (!(entityFactory.objectReferenceValue is IEntityFactory))
            {
                EditorGUILayout.HelpBox("Assigned object does not implement IEntityFactory!", MessageType.Error);
                entityFactory.objectReferenceValue = null;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        levelDesignData = serializedObject.FindProperty("levelDesignData");
        entityFactory = serializedObject.FindProperty("entityFactory");
    }
}