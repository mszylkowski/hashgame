using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
    public int level = 1;
    public int progress = 0;
    public string hash;
    public TextMesh hashText;
    public GameObject keyPref;
    public string field;
    public float separation;
    public TextMesh consoleText;
    public Transform progressBar;
    public bool encoding;
    public Scroll scroll;
    public string consoleValue;

    public Buttons button1;
    public Buttons button2;

    // Use this for initialization
    void Start () {
        string[] numberKeys = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
        string[] upperKeys = {"Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P"};
        string[] middleKeys = {"A", "S", "D", "F", "G", "H", "J", "K", "L"};
        string[] lowerKeys = {"Z", "X", "C", "V", "B", "N", "M", "DEL", ""};

        string[][] keys = {numberKeys, upperKeys, middleKeys, lowerKeys};

        int countHorizontal = 0;
        int countVertical = 0;
        foreach (string[] layer in keys) {
            countHorizontal = 0;
            foreach (string keyValue in layer) {
                GameObject key = Instantiate(keyPref, Vector3.left * separation * (layer.Length/2f-countHorizontal - .5f) + Vector3.down * (countVertical * separation * 1.2f + 2.3f), Quaternion.identity) as GameObject;
                key.GetComponent<Key>().keyboard = this;
                key.GetComponent<Key>().SetKeyLabel(keyValue);
                countHorizontal++;
                key.transform.SetParent(gameObject.transform);
                if (key.GetComponent<Key>().keyLabel.text == "DEL") {
                    key.transform.localScale += Vector3.right * .54f;
                    key.transform.GetChild(0).localScale -= Vector3.right * .55f;
                    key.transform.position += Vector3.right * separation * .5f;
                } else if (key.GetComponent<Key>().keyLabel.text == "") {
                    Destroy(key);
                }
            }
            countVertical++;
        }
        consoleValue = "Enter 6 characters as\ninput and match the\nhash output to\nbeat the level\n\n------ LEVEL 01 ------\n> ";
        scroll.UpdateString(consoleValue);
        StartCoroutine(changeProgress());
    }

    public void InputKey(string key) {
        if (encoding) return;
        if (key == "DEL") {
            if (field.Length > 0) {
                field = field.Substring(0, field.Length-1);
            }
            scroll.UpdateString(consoleValue + field.ToLower());
        } else {
            field += key;
            scroll.UpdateString(consoleValue + field.ToLower());
        }

        if (field.Length > 6) {
            field = key;
        }
        
        if (field.Length == 6) {
            StartCoroutine(encode(new string(Levels.encodelevel(level, field.ToCharArray()))));
        }
    }
        
    IEnumerator encode(string result) {
        button1.hide();
        button2.hide();
        consoleValue += field.ToLower();
        encoding = true;
        for (int index = 0; index < result.Length; index++) {
            for (int rep = 0; rep < index/2 + 3; rep++) {
                yield return null;yield return null;yield return null;
                string last = result.Substring(0, index).ToLower() + RandomString(result.Length - index);
                scroll.UpdateString(consoleValue + "\nEncoding: " + last.ToLower());
            }
        }
        consoleValue += "\nEncoded: " + result.ToLower() + "\n";
        scroll.UpdateString(consoleValue);
        if (hash == result) {
            progress++;
            if (progress >= 2) {
                level++;
            } else {
                consoleValue += "> ";
            }
            StartCoroutine(changeProgress());
        } else {
            consoleValue += "> ";
        }
        scroll.UpdateString(consoleValue);
        field = "";
        encoding = false;
    }

    IEnumerator changeProgress() {
        while (Mathf.Abs(progressBar.localScale.x - progress * .5f)>.011f) {
            if (progressBar.localScale.x > progress * .5f) {
                progressBar.localScale -= Vector3.right * .02f;
                yield return null;
            } else {
                progressBar.localScale -= Vector3.left * .02f;
                yield return null;
            }
        }
        hash = new string(Levels.encodelevel(level, RandomString(6).ToCharArray()));
        hashText.text = "Hash: " + hash.ToLower();

        if (progress == 2) {
            progress = 0;
            consoleValue += "\n----- LEVEL " + level.ToString("00") + " -----\n> ";
            scroll.UpdateString(consoleValue);
            StartCoroutine(changeProgress());
        }
    }

    static string RandomString(int len) {
        string last = "";
        for (int i = 0; i < len; i++) {
            last += Levels.ItoC(Random.Range(0, 36));
        }
        return last;
    }
}