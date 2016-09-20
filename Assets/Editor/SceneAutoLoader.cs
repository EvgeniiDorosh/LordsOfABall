using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
static class SceneAutoLoader {

	private const string shortcut = "%1";

	//static constructor subscribes to event (this is the reason for [InitializeOnLoad] at the top)  
	static SceneAutoLoader () {    
		EditorApplication.playmodeStateChanged += ReloadIfPlayModeHasStopped; 
	}

	static void ReloadIfPlayModeHasStopped() {

		if( EditorApplication.isPlayingOrWillChangePlaymode ) { 
			return;    
		}    // we are only interested in playmode-stop
		if (!StartedPerShortcut)                                { 
			return;    
		}    // if we didn't start per shortcut, there is nothing to do here

		if(!EditorApplication.isPlaying) {
			EditorSceneManager.OpenScene(PreviousScenePath);    //reload previous scene
			StartedPerShortcut = false;                            //reset
		}
	}

	[MenuItem("Edit/StartFromMasterScene/Play "+ shortcut)]
	public static void PlayFromMasterScene(){

		//Load Master before play-mode starts
		if ( EditorApplication.isPlaying == false ) {
			// kindly ask for scene save
			if(!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
				return; //abort if canceled
			} 

			PreviousScenePath = EditorSceneManager.GetActiveScene().path;    // save PATH of current scene (LoadScene needs name, OpenScene needs path)          
			EditorSceneManager.OpenScene(MasterScenePath);                    // open Master scene before starting          
			EditorApplication.isPlaying = true;                                // start
			StartedPerShortcut = true;                                        // remember that we started per shortcut

			Debug.Log("Previous" + PreviousScenePath);

			//shortcut can be used as stop-button while play-mode is active
		} else {
			EditorApplication.isPlaying = false;
		}
	}

	//    Menu item to choose the path of the master-scene
	[MenuItem("Edit/StartFromMasterScene/Select Master Scene...")]
	private static void SelectMasterScene(){
		string masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
		if (!string.IsNullOrEmpty(masterScene)){
			MasterScenePath = masterScene;
		}
	}

	// Properties need to be remembered as editor preferences, otherwise they would be lost between playmode on-offs

	//path of master scene
	private const string cEditorPrefMasterScene = "SceneAutoLoader.MasterScene";  
	private static string MasterScenePath{
		get { return EditorPrefs.GetString(cEditorPrefMasterScene, "none"); }
		set { EditorPrefs.SetString(cEditorPrefMasterScene, value); }
	}

	//path of previous scene
	private const string cEditorPrefPreviousScene = "SceneAutoLoader.PreviousScenePath";
	private static string PreviousScenePath{
		get { return EditorPrefs.GetString(cEditorPrefPreviousScene, "none"); }
		set { EditorPrefs.SetString(cEditorPrefPreviousScene, value); }
	}

	//remember if editor started per shortcut
	private const string cEditorPrefStartedPerShortcut = "SceneAutoLoader.StartedPerShortcut";
	private static bool StartedPerShortcut{
		get { return EditorPrefs.GetBool(cEditorPrefStartedPerShortcut, false); }
		set { EditorPrefs.SetBool(cEditorPrefStartedPerShortcut, value); }
	}
}