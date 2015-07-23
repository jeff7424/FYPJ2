﻿#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(EnemyWaves))]
public class EnemyWavesEditor : Editor {

	int levelIndex = 0;
	List<int> waveIndex = new List<int>();
	List<List<int>> subwaveIndex = new List<List<int>>();

	void Awake(){
		levelIndex = EditorPrefs.GetInt("levelIndex", 0);

		//Waves
		if(!EditorPrefs.HasKey("waveIndex"))
			return;
		int[] waveArray = EditorPrefsX.GetIntArray("waveIndex");
		for(int i = 0; i < waveArray.Length; ++i)
			waveIndex.Add(waveArray[i]);

		//Subwaves
		int j = 0;
		while(EditorPrefs.HasKey("subwaveIndex"+j)){
			List<int> subwaveIndex_temp = new List<int>();
			int[] subwaveArray = EditorPrefsX.GetIntArray("subwaveIndex"+j);
			for(int k = 0; k < subwaveArray.Length; ++k)
				subwaveIndex_temp.Add(subwaveArray[k]);
			subwaveIndex.Add(subwaveIndex_temp);
			++j;
		}
	}



	void ResetValues(){
		EditorPrefs.DeleteKey("levelIndex");
		EditorPrefs.DeleteKey("waveIndex");
		int i = 0;
		while(EditorPrefs.HasKey("subwaveIndex"+i)){
			EditorPrefs.DeleteKey("subwaveIndex"+i);
			++i;
		}
	}



	void OnDestroy(){
		EditorPrefs.SetInt("levelIndex", levelIndex);

		//Waves
		if(waveIndex.Count < 1)
			return;
		int[] waveArray = waveIndex.ToArray();
		EditorPrefsX.SetIntArray("waveIndex", waveArray);

		//Subwaves
		for(int i = 0; i < subwaveIndex.Count; ++i){
			int[] subwaveArray = null;

			if(subwaveIndex[i].Count > 0)
				subwaveArray = subwaveIndex[i].ToArray();
			else{
				subwaveArray = new int[1];
				subwaveArray[0] = 0;
			}

			EditorPrefsX.SetIntArray("subwaveIndex"+i, subwaveArray);
		}
	}



	public override void OnInspectorGUI(){
		EnemyWaves myTarget = (EnemyWaves) target;

		//Check if subwaveIndex is empty
		if(subwaveIndex.Count == 0 && myTarget.levels.Count > 0){
			int j = 0;
			while(EditorPrefs.HasKey("subwaveIndex"+j)){
				List<int> subwaveIndex_temp = new List<int>();
				int[] subwaveArray = EditorPrefsX.GetIntArray("subwaveIndex"+j);
				for(int k = 0; k < subwaveArray.Length; ++k)
					subwaveIndex_temp.Add(subwaveArray[k]);
				subwaveIndex.Add(subwaveIndex_temp);
				++j;
			}
		}

		GUILayout.Space(10.0f);

		//Level select
		////////////////////////////////////////////////////////////////////////////////////////////
		GUILayout.BeginHorizontal();

		string[] theLevels = new string[myTarget.levels.Count];
		for(int i = 0; i < theLevels.Length; ++i){
			theLevels[i] = "Level " + (i+1).ToString();
		}
		levelIndex = GUILayout.Toolbar(levelIndex, theLevels, EditorStyles.toolbarButton);

		GUILayout.Space(10);

		//"+" button to add levels
		if(GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(40))){
			myTarget.levels.Add(new EnemyWaves.Level());
			//Increase waveIndex array
			waveIndex.Add(0);
			//Increase subwaveIndex array
			subwaveIndex.Add(new List<int>());
			subwaveIndex[myTarget.levels.Count-1].Add(0);
		}
		//"-" button to remove levels
		if(GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(40)) && myTarget.levels.Count > 0){
			myTarget.levels.RemoveAt(myTarget.levels.Count-1);
			waveIndex.RemoveAt(waveIndex.Count-1);
			subwaveIndex.RemoveAt(subwaveIndex.Count-1);
			if(levelIndex > myTarget.levels.Count-1)
				--levelIndex;		// Lower levelIndex if player had it selected upon deletion
			if(levelIndex < 0)
				levelIndex = 0;
		}

		GUILayout.EndHorizontal();
		////////////////////////////////////////////////////////////////////////////////////////////

		//Level terrain
		////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginVertical(EditorStyles.textField);
		GUILayout.Space(3);

		//The grid buttons
		for(int i = 0; i < 9; ++i){
			GUILayout.BeginHorizontal();

			for(int j = 0; j < 9; ++j){
				if(GUILayout.Button("", EditorStyles.toolbarButton, GUILayout.Width(18))){

				}
				GUILayout.Space(3);
			}

			GUILayout.EndHorizontal();
			GUILayout.Space(3);
		}

		//The terrain type buttons
		GUILayout.BeginHorizontal();
		//None
		GUI.backgroundColor = new Color(1.0f,1.0f,1.0f,1.0f);
		if(GUILayout.Button("None", EditorStyles.toolbarButton)){
			
		}
		//Obstacle
		GUI.backgroundColor = new Color(0.0f,1.0f,0.0f,1.0f);
		if(GUILayout.Button("Obstacle", EditorStyles.toolbarButton)){

		}
		//Tunnel
		GUI.backgroundColor = new Color(1.0f,1.0f,0.0f,1.0f);
		if(GUILayout.Button("Tunnel", EditorStyles.toolbarButton)){
			
		}
		//Platform
		GUI.backgroundColor = new Color(0.25f,0.25f,1.0f,1.0f);
		if(GUILayout.Button("Platform", EditorStyles.toolbarButton)){
			
		}
		GUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
		////////////////////////////////////////////////////////////////////////////////////////////

		GUILayout.Space(5);

		//Waves select
		////////////////////////////////////////////////////////////////////////////////////////////
		GUI.backgroundColor = new Color(0.95f,0.99f,1.0f,1.0f);
		if ( myTarget.levels.Count < 1 || levelIndex >= myTarget.levels.Count )
			return;        // Quit if there are no levels yet

		GUILayout.BeginHorizontal();

		string[] theWaves = new string[myTarget.levels[levelIndex].waves.Count];
		for(int i = 0; i < theWaves.Length; ++i)
			theWaves[i] = "Wave " + (i+1).ToString();
		waveIndex[levelIndex] = GUILayout.Toolbar (waveIndex[levelIndex], theWaves, EditorStyles.toolbarButton);

		GUILayout.Space(10);

		//"+" button to add waves
		if(GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(40))){
			myTarget.levels[levelIndex].waves.Add(new EnemyWaves.Wave());
			subwaveIndex[levelIndex].Add(0);
		}

		//"-" button to remove waves
		if(GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(40)) && myTarget.levels[levelIndex].waves.Count > 0){
			myTarget.levels[levelIndex].waves.RemoveAt(myTarget.levels[levelIndex].waves.Count-1);
			subwaveIndex[levelIndex].RemoveAt(subwaveIndex[levelIndex].Count-1);
			if(waveIndex[levelIndex] > myTarget.levels[levelIndex].waves.Count-1)
				--waveIndex[levelIndex];		// Lower waveIndex if player had it selected upon deletion
			if(waveIndex[levelIndex] < 0)
				waveIndex[levelIndex] = 0;
		}

		GUILayout.EndHorizontal();
		////////////////////////////////////////////////////////////////////////////////////////////

		//Start wave details box
		////////////////////////////////////////////////////////////////////////////////////////////
		if ( myTarget.levels[levelIndex].waves.Count < 1 || myTarget.levels[levelIndex].waves[waveIndex[levelIndex]] == null )
			return;		// Quit if there's no wave for this level yet
		EditorGUILayout.BeginVertical(EditorStyles.textField);
		GUILayout.Space(5);

		//Total enemies count
		GUILayout.BeginHorizontal();
		GUI.backgroundColor = new Color(0.99f,0.95f,1.0f,1.0f);
		GUILayout.Label ("Total Enemies", GUILayout.Width(100));
		GUILayout.Label (myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].TotalEnemies().ToString(), GUILayout.Width(40));
		GUILayout.EndHorizontal();

		//Subwave count
		GUILayout.BeginHorizontal();
		GUI.backgroundColor = new Color(0.99f,0.95f,1.0f,1.0f);
		GUILayout.Label ("Subwaves", GUILayout.Width(100));
		GUILayout.Label (myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Count.ToString(), GUILayout.Width(40));

		//"+" button add subwaves
		if ( GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(30)) )
			myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Add(new EnemySubwave());

		//"-" button remove subwaves
		if ( GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30)) && myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Count > 0)
		{
			myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.RemoveAt(myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Count -1);
			if ( subwaveIndex[levelIndex][waveIndex[levelIndex]] > myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Count -1 )
				--subwaveIndex[levelIndex][waveIndex[levelIndex]];		// Lower subwaveIndex if player had it selected upon deletion
			if ( subwaveIndex[levelIndex][waveIndex[levelIndex]] < 0 )
				subwaveIndex[levelIndex][waveIndex[levelIndex]] = 0;
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		////////////////////////////////////////////////////////////////////////////////////////////

		//Subwave dropdown
		////////////////////////////////////////////////////////////////////////////////////////////
		if ( myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Count < 1 ||
		    myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves[subwaveIndex[levelIndex][waveIndex[levelIndex]]] == null )
			return;		// Quit if there are no subwaves yet
		var parsedSubwaves = new string[myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves.Count];
		for (int i = 0; i < parsedSubwaves.Length; i++ )
		{ parsedSubwaves[i] = "Subwave " + (i+1).ToString(); }
		subwaveIndex[levelIndex][waveIndex[levelIndex]] = EditorGUILayout.Popup (subwaveIndex[levelIndex][waveIndex[levelIndex]], parsedSubwaves, EditorStyles.toolbarDropDown);

		////////////////////////////////////////////////////////////////////////////////////////////

		//Subwave details
		////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginVertical(EditorStyles.textField);
		GUILayout.Space(5);
		GUI.backgroundColor = Color.white;
		EditorGUIUtility.labelWidth = 0;
		EditorGUIUtility.fieldWidth = 0;
		EnemySubwave currSubwave = myTarget.levels[levelIndex].waves[waveIndex[levelIndex]].Subwaves[subwaveIndex[levelIndex][waveIndex[levelIndex]]];
		while(currSubwave.Enemies.Count < (int)Enemy.enemyType.TYPE_MAX)
			currSubwave.Enemies.Add(0);
		while(currSubwave.Enemies.Count > (int)Enemy.enemyType.TYPE_MAX)
			currSubwave.Enemies.RemoveAt(currSubwave.Enemies.Count-1);

		EditorGUILayout.LabelField("Total Enemies", currSubwave.getTotalEnemies().ToString());
		currSubwave.activateTime = EditorGUILayout.FloatField("Start Time", currSubwave.activateTime);
		currSubwave.spawnRate = EditorGUILayout.FloatField("Spawn Rate", currSubwave.spawnRate);
		string[] enemyNames = System.Enum.GetNames(typeof(Enemy.enemyType));
		for(int i = 0; i < currSubwave.Enemies.Count; ++i){
			currSubwave.Enemies[i] = EditorGUILayout.IntField(enemyNames[i], currSubwave.Enemies[i]);
			if(currSubwave.Enemies[i] < 0)
				currSubwave.Enemies[i] = 0;
		}

		EditorGUILayout.EndVertical();
		////////////////////////////////////////////////////////////////////////////////////////////
	}

}

#endif
