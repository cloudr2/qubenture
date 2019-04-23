using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour {

    public delegate void FunctionPrototype();

    public GameObject consolePrefab;
    public Text output;
    public InputField input;
    public KeyCode openConsole;

    public static Console instance;

    public Dictionary<string, FunctionPrototype> commandDictionary = new Dictionary<string, FunctionPrototype>();

    public List<string> commandList = new List<string>();
    public List<string> commandsDescriptions = new List<string>();

    private void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        RegisterCommand("Help", Help);
    }

    void Update() {
        if (Input.GetKeyDown(openConsole))
            consolePrefab.SetActive(!consolePrefab.activeSelf);

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (consolePrefab.activeSelf) {
                Write(input.text);

                if (commandDictionary.ContainsKey(input.text))
                    commandDictionary[input.text].Invoke();
                else
                    Write("Comando no encontrado");
            }
        }
    }

    public void RegisterCommand(string commandName, FunctionPrototype command) {
        commandDictionary.Add(commandName, command);
    }

    private void Write(string txt) {
        output.text += txt + "\n";
    }

    public void Help() {
        foreach (var dictionaryItem in commandDictionary) {
            Write(dictionaryItem.Key + ": " + dictionaryItem.Value);
        }
    }
}
