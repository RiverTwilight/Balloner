using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    private Text textComponent;

    private string[] gameOverMessages = new string[] {
        "That was a good try!\n Want to go again?",
        "You were so close! Ready for another round?",
        "Don't worry, even the best stumble.\n Try again!",
        "Great effort!\n Let's beat that score next time.",
        "Keep your spirits high!\n Let's give it another shot.",
        "You're getting better each time.\n Keep going!",
        "A minor setback. You got this!",
        "Just a practice run.\n Time for the real thing!",
        "That was just a warm-up.\nLet's do it for real!",
        "Remember, practice makes perfect.\n Onward!"
    };

    public void Shuffle()
    {
        textComponent = GetComponent<Text>();

        // Set a random game over message
        textComponent.text = gameOverMessages[Random.Range(0, gameOverMessages.Length)];
    }
}
