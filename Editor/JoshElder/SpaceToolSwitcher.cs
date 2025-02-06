using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class SpaceToolSwitcher
{
    private static readonly Tool[] cycleTools = new Tool[]
    {
        Tool.Move,
        Tool.Rotate,
        Tool.Scale
    };

    // This integer will determine which tool in the array we are currently on.
    private static int currentToolIndex = 0;

    static SpaceToolSwitcher()
    {
        // This subscribes to the SceneView's event callback so we can detect key presses in the Scene.
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        // Check if the user pressed a key and if that key is the Spacebar.
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space)
        {
            // Advance our index (wrap around using the modulus operator).
            currentToolIndex = (currentToolIndex + 1) % cycleTools.Length;

            // Set the current tool in the Unity Editor to the selected tool.
            Tools.current = cycleTools[currentToolIndex];

            // Consume the event so other things in Unity don’t also try to use the Space key.
            e.Use();
        }
    }
}
