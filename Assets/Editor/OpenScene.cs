using UnityEngine;
using UnityEditor;
using System.Collections;


/// <summary>
/// Written by Jamie Brown. Enjoy and distribute as you please.
/// Twitter: @_DevelopMentat
/// Reddit : DevelopMentat
/// 
/// Allows File Menu Access to Project's Scenes. 
/// </summary>
public class OpenScene: MonoBehaviour {
	
	/* Follow Comments For Easy Customization. Enjoy! */
	
	/* 1. Change string literal to the path for your Scenes folder */
	static string scenesPath = "Assets/";
	
	/* 2. Add Sub Directories or even Alternate Directories as additional properties. */
	static string prefix_Levels = "Levels/";
	static string prefix_Tests  = "TestScenes/";
	
	/* 3. Create a Menu Item and a Unique Static Method per scene. */
	/*
	• MenuItem string parameter must be unique. 
	• Using a '/' in the MenuItem parameter creates sub directories in the menu's dropdown.
	• Method Names must be unique - i.e. "OpenLevel_001()", "OpenLevel_002()" etc.
    */
	
	[MenuItem("Open Scene/Levels/MainMenu")] static void OpenLevel_000() { Open( prefix_Levels + "MainMenu" ); }
	
	[MenuItem("Open Scene/Levels/HomeIsland")] static void OpenLevel_001() { Open( prefix_Levels + "HomeIsland" ); }
	[MenuItem("Open Scene/Levels/HomeInterior")] static void OpenLevel_002() { Open( prefix_Levels + "HomeInterior" ); }
	[MenuItem("Open Scene/Levels/WaterfallIslands")] static void OpenLevel_003() { Open( prefix_Levels + "WaterfallIslands" ); }
	[MenuItem("Open Scene/Levels/GrandparentsIsland")] static void OpenLevel_004() { Open( prefix_Levels + "GrandparentsIsland" ); }
	
//	[MenuItem("Open Scene/TestScenes/Test1")] static void OpenLevel_Test_1() { Open( prefix_Tests + "TestScene_1" ); }
//	[MenuItem("Open Scene/TestScenes/Test2")] static void OpenLevel_Test_2() { Open( prefix_Tests + "TestScene_2" ); }
//	
	/* 4. Add as many Scenes as you wish. */
	
	
	/* Note: I've found it's best to keep things grouped by like kind and to keep the number of items per menu to 
	 * less than about 20. But that's largely personal preference when working with a ton of scenes.*/
	
	/* Helper Method To Reduce Code Clutter */
	static void Open( string sceneName ) {
		if(EditorApplication.SaveCurrentSceneIfUserWantsTo()) {
			if( !EditorApplication.OpenScene(scenesPath + sceneName + ".unity" ) ) {
				Debug.Log("Scene:  " + sceneName + "  Was Not Found");
			}
		}
	}
}
