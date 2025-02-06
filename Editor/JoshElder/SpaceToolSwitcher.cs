namespace JoshElder.SpaceToolSwitcher
{
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    [InitializeOnLoad]
    public static class SpaceToolSwitcher
    {
        // Build an array of valid Tools (explicitly skipping 'None').
        private static readonly Tool[] cycleTools;
        
        // Tracks the current tool in the array.
        private static int currentToolIndex = 0;

        static SpaceToolSwitcher()
        {
            // Create the array of all enum values, skipping 'None'.
            cycleTools = ((Tool[])Enum.GetValues(typeof(Tool)))
                .Where(t => t != Tool.None)
                .ToArray();

            // Subscribe to the SceneView's event callback to detect key presses.
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            Event e = Event.current;

            // Check if the Spacebar was pressed.
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space)
            {
                // Increment and wrap the tool index within the valid range.
                currentToolIndex = (currentToolIndex + 1) % cycleTools.Length;

                // Set the active tool.
                Tools.current = cycleTools[currentToolIndex];

                // Mark the event as used so no other elements process it.
                e.Use();
            }
        }
    }
}
