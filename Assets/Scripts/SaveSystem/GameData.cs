[System.Serializable]
public class GameData{
  public int highScore;
  public GameData(GameManager game){
    highScore = game.highScore;
  }
}
