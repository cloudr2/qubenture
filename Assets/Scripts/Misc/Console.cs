using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour {

    public delegate void FunctionPrototype();
    public delegate void FunctionPrototypeParams(string args);
    
    public GameObject consolePrefab;
    public Text output;
    public InputField input;
    public KeyCode openConsole;

    public static Console instance;

    public Dictionary<string, string> commandDescriptions = new Dictionary<string, string>();
    public Dictionary<string, FunctionPrototype> commandDictionary = new Dictionary<string, FunctionPrototype>();
    public Dictionary<string, FunctionPrototypeParams> commandParamsDictionary = new Dictionary<string, FunctionPrototypeParams>();

    private void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        RegisterCommand("help", Help, "Show all commands.");
        RegisterCommand("clear", ClearConsole, "Clear console log.");

        ClearConsole();
    }

    private void Start()
    {
        RegisterCommand("aggro", GameManager.instance.Aggro, "All enemies targets player.");
        RegisterCommand("endgame", GameManager.instance.DestroyEnemies, "Destroy all enemies.");
        RegisterCommandWithParams("godmode", GameManager.instance.GodMode, "Player is invulnerable.");
    }

    private void ClearConsole() {
        output.text = "";
    }

    public void TriggerConsole() {
        consolePrefab.SetActive(!consolePrefab.activeSelf);
        if (consolePrefab.activeSelf) {
            ClearConsole();
            input.Select();
        }
    }

    void Update() {
        if (Input.GetKeyDown(openConsole)) {
            TriggerConsole();
        }

        if (consolePrefab.activeSelf) {
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Return) && input.text != "") {
                Write(input.text);
                string[] splitText = input.text.Split(' ');

                if (splitText.Length == 1 && commandDictionary.ContainsKey(input.text))
                    commandDictionary[input.text].Invoke();
                else if (splitText.Length == 2 && commandParamsDictionary.ContainsKey(splitText[0]))
                    commandParamsDictionary[splitText[0]].Invoke(splitText[1]);
                else
                    Write("Command: " + input.text + " is not found.");

                input.text = "";
                input.Select();
            }
        }
        else
            Time.timeScale = 1f;           
    }

    public void RegisterCommand(string commandName, FunctionPrototype command, string description) {
        commandDictionary.Add(commandName, command);
        commandDescriptions.Add(commandName, description);
    }

    public void RegisterCommandWithParams(string commandName, FunctionPrototypeParams command, string description) {
        commandParamsDictionary.Add(commandName, command);
        commandDescriptions.Add(commandName, description);
    }

    public void Write(string txt) {
        output.text += ">" + txt + "\n";
    }

    public void Help() {
        foreach (var dictionaryItem in commandDescriptions) {
            Write(dictionaryItem.Key + ": " + dictionaryItem.Value);
        }
    }
}
