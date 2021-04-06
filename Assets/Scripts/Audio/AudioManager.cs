using System;
using UnityEngine;

public class AudioManager: MonoBehaviour{
  public static new AudioManager audio;
  public Sound[] sounds;
  public void Awake(){
    if(audio == null){
      audio = this;
      DontDestroyOnLoad(gameObject);
    }else{
      Destroy(gameObject);
    }
    ConfigureSounds();

  }

  private void ConfigureSounds(){
    foreach(Sound s in sounds){
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.loop = s.loop;
    }
  }

  public void Play(string name){
    Sound s = Array.Find<Sound>(sounds, x => x.name == name);
    s.source.Play();
  }
}
