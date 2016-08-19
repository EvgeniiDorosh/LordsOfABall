using UnityEngine;

public static class Settings {

	private static int musicLevel = 50;
	public static int MusicLevel {

		get {return musicLevel; }
		set {musicLevel = value; }
	}

	private static int fxLevel = 50;
	public static int FXLevel {

		get {return fxLevel; }
		set {fxLevel = value; }
	}

	public static int levelSceneOffset = 1;
}
