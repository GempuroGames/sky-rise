using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{
  private static readonly string path = Application.persistentDataPath + "/game.sav";
  public static void Save(){
    BinaryFormatter formatter = new BinaryFormatter();
    FileStream stream = new FileStream(path, FileMode.Create);
    GameData data = new GameData(GameManager.manager);
    formatter.Serialize(stream, data);
    stream.Close();
  }

  public static GameData Load(){
    if(File.Exists(path)){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        GameData data = formatter.Deserialize(stream) as GameData;
        stream.Close();
        return data;
    }else{
      return null;
    }
  }
}
