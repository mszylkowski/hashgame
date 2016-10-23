using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
    public string key;
    public Transform panel;
    public TextMesh text;
    public Keyboard keyboard;
    public bool showing = false;
    public float movement;

    public Buttons other;

	// Use this for initialization
	void Start () {
        if (key == "table") {
            text.text = "0-00  9-09  I-18  R-27\n1-01  A-10  J-19  S-28\n2-02  B-11  K-20  T-29\n3-03  C-12  L-21  U-30\n4-04  D-13  M-22  V-31\n5-05  E-14  N-23  W-32\n6-06  F-15  O-24  X-33\n7-07  G-16  P-25  Y-34\n8-08  H-17  Q-26  Z-35";
        } else {
            text.text = Levels.getHint(keyboard.level);
        }
	}

    public void hide() {
        if (showing) {
            showing = false;
            StartCoroutine(move(movement, 10));
        }
    }

    public void show() {
        if (key == "hint") {
            text.text = Levels.getHint(keyboard.level);
        }
        if (other.showing) {
            other.hide();
        }
        if (!showing) {
            showing = true;
            StartCoroutine(move(-movement, 10));
        }
    }

    IEnumerator move(float delta, int iterations) {
        for (int i = 0; i < iterations; i++) {
            yield return null;
            panel.transform.position += Vector3.up * delta;
        }
    }

    void OnMouseUpAsButton() {
        if (showing) {
            hide();
        } else {
            show();
        }
    }

    void OnMouseDown() {
        transform.localScale *= .9f;
    }

    void OnMouseUp() {
        transform.localScale /= .9f;
    }
}