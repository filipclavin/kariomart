using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    public List<SceneAsset> trackScenes;
    public SceneAsset mainMenuScene;
    public SceneAsset pauseMenuScene;

    public string goalTag;
    public string boostTag;
    public string checkpointTag;

    public Dictionary<string, int> raceProgress = new();
}