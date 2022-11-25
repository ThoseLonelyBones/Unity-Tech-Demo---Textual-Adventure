using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObjects = MonoBehaviour but not attached to GameObjects. Can live in memory or as an asset.
[CreateAssetMenu(menuName = "TechDemo/Scenario")]
public class ScenarioScript : ScriptableObject
{
    [TextArea]
    public string scenario_text;
    public string scenario_name;
    [TextArea]
    public string scenario_alt_text;
}
