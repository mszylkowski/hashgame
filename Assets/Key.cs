using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    public TextMesh keyLabel;
    public string keyValue;
    public Keyboard keyboard;

    // Use this for initialization
    void Start () {
       keyLabel.text = keyValue;
       gameObject.name = "Key " + keyValue;
    }

    public void SetKeyLabel(string label) {
        keyValue = label;
        keyLabel.text = keyValue;
        gameObject.name = "Key " + keyValue;
    }

    void OnMouseUpAsButton() {
        keyboard.InputKey(keyValue);
    }

    void OnMouseDown() {
        transform.localScale *= .9f;
    }

    void OnMouseUp() {
        transform.localScale /= .9f;
    }
}