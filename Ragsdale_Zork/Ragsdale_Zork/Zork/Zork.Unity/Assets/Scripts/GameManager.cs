using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork.Common;
using TMPro;



//INCREMENT SCORE BY INPUTTING 'r' INTO THE COMMAND


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string GameFileAssetName = "Zork";

    [SerializeField]
    private UnityOutputService OutputService;

    [SerializeField]
    private UnityInputService InputService;

    [SerializeField]
    private TMP_Text ScoreText;

    [SerializeField]
    private TMP_Text LocationText;

    [SerializeField]
    private TMP_Text MovesText;

    private Game Game 
    { 
        get; 
        set; 
    }
    // Start is called before the first frame update
    void Awake()
    {
        TextAsset gameFileAsset = Resources.Load<TextAsset>(GameFileAssetName);
        Game = Game.Load(gameFileAsset.text, OutputService, InputService);

        LocationText.text = $"Location: {Game.Player.Location}";

        Game.Player.ScoreChanged += ScoreChanged;
        Game.Player.PlayerMoved += MoveChanged;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (string.IsNullOrWhiteSpace(InputService.InputField.text) == false)
            {
                Game.Output.WriteLine($">{InputService.InputField.text}");
                InputService.ProcessInput();
                LocationText.text = $"Location: {Game.Player.Location}";
            }
            InputService.InputField.text = string.Empty;
            InputService.InputField.Select();
            InputService.InputField.ActivateInputField();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void MoveChanged(object sender, int i)
    {
        MovesText.text = $"Moves: {Game.Player.timesMoves}";
    }

    private void ScoreChanged(object sender, int i)
    {
        ScoreText.text = $"Score: {Game.Player.Score}";
    }
}
