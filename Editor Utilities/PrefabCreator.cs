using UnityEngine;
using UnityEditor;

public static class PrefabCreator
{
    //---Menu Items---
    [MenuItem("GameObject/Custom Objects/Prefab0")]
    public static void CreatePrefab0()
    {
        InstantiatePrefab("Prefab0");
    }
  
    [MenuItem("GameObject/Custom Objects/Prefab1")]
    public static void CreatePrefab1()
    {
        InstantiatePrefab("Prefab1");
    }
  
    [MenuItem("GameObject/Custom Objects/Prefab2")]
    public static void CreatePrefab2()
    {
        InstantiatePrefab("Prefab2");
    }
  
    //---Class methods
    //Instantiates the prefab
    private static void InstantiatePrefab(string prefabName)
    {
        string[] guids = AssetDatabase.FindAssets($"{prefabName} t:prefab");

        if (guids.Length == 0)
        {
            Debug.LogError($"{prefabName} prefab not found.");
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        if(prefab == null)
        {
            Debug.LogError($"failed to load {prefabName} prefab at path {path}.");
        }
        else
        {
            GameObject prefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            prefabInstance.transform.parent = Selection.activeTransform;
            prefabInstance.transform.position = Vector3.zero;

            Selection.activeObject = prefabInstance;
        }
    }
}
