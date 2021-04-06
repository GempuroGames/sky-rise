using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int highScore;
    public void RaiseScore(int s){
      Player.score += s;
      if(highScore <= Player.score){
        highScore = Player.score;
      }
    }

	private void Awake()
	{
		if (manager == null)
		{
			manager = this;
			DontDestroyOnLoad(this);
      GameData data = SaveSystem.Load();
      if(data != null){
        highScore = data.highScore;
      }
		}
		else
		{
			Destroy(gameObject);
		}
	}


}
