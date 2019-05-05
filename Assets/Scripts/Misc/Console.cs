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
        RegisterCommand("endgame", Endgame, "Win the stage.");
        RegisterCommand("aggro", Aggro, "All enemies targets player.");
        RegisterCommand("clear", ClearConsole, "Clear console log.");
        RegisterCommandWithParams("godmode", GodMode, "Player is invulnerable.");

        ClearConsole();
    }

    private void Endgame() {
    }

    private void GodMode(string arg) {
        if (arg == "" || arg == null)
            Write("Error: Godmode must receive params 'on' or 'off'.");
        else if (arg == "off")
            Write("godmode: off");
        else if (arg == "on")
            Write("godmode: on");
        else
            Write("the argument " + arg + " is not valid.");
    }
    private void Aggro() {
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

    private void Write(string txt) {
        output.text += ">" + txt + "\n";
    }

    public void Help() {
        foreach (var dictionaryItem in commandDescriptions) {
            Write(dictionaryItem.Key + ": " + dictionaryItem.Value);
        }
    }
}
