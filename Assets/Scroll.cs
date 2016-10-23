using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
    public Transform consoleText;
    public float height;
    public float speed;
    public float previousProgress;
    public float firstTouch;
    public float progress;
    public bool pressing;
    public float lastPress;
    public float maxHeight;
    public float minHeight;
    public string cursor = " ";

    private string textValue;

    public float ratio;

    public Camera camera;

    public Keyboard keyboard;

	// Use this for initialization
	void Start () {
	   consoleText = transform.GetChild(0);
       maxHeight = -1.83f;
       ratio = camera.pixelHeight * .1f;
       StartCoroutine(CursorBlink());
	}
	
	// Update is called once per frame
	void Update () {
        if (pressing) {
            float posy;
            if (Application.platform == RuntimePlatform.WindowsEditor) {
                posy = Input.mousePosition.y;
            } else {
                posy = Input.touches[0].position.y;
            }
            posy /= camera.pixelHeight*.1f;
            progress = previousProgress + posy - firstTouch;
            if (speed == 0) {
                speed = lastPress - posy;
            }
            speed = speed * .9f + .1f * (lastPress - posy);
            lastPress = posy;
        } else {
            speed *= .95f;
            progress -= speed;
            previousProgress = progress;
            if (Mathf.Abs(speed) < .01) {speed = 0;}
        }
        progress = Mathf.Clamp(progress, minHeight, maxHeight);
        consoleText.transform.position = Vector3.up * progress + Vector3.left * 2.7f;
	}

    public void UpdateString(string text) {
        textValue = text;
        if (keyboard.encoding) {
            cursor = " ";
        }
        updateCursor();
        height = consoleText.GetComponent<TextMesh>().text.Split('\n').Length * .5f;
        minHeight = maxHeight - height + 6.2f;
        if (minHeight > maxHeight) {
            minHeight = maxHeight;
        }
        StartCoroutine(ScrollDown());
    }

    void OnMouseDown() {
        pressing = true;
        float posy;
            if (Application.platform == RuntimePlatform.WindowsEditor) {
                posy = Input.mousePosition.y;
            } else {
                posy = Input.touches[0].position.y;
            }
            posy /= camera.pixelHeight*.1f;
            firstTouch = posy;
            progress = previousProgress + posy - firstTouch;
            lastPress = posy;
    }

    void OnMouseUp() {
        pressing = false;
    }

    IEnumerator ScrollDown() {
        for (int i = 0; i < 10; i++) {
            yield return null;
            progress = maxHeight* .5f + previousProgress * .5f;
            previousProgress = progress;
        }
    }

    IEnumerator CursorBlink() {
        while(true) {
            if (cursor == " " && !keyboard.encoding) {
                cursor = "_";
                updateCursor();
                yield return new WaitForSeconds(.8f);
            } else {
                cursor = " ";
                updateCursor();
                yield return new WaitForSeconds(.5f);
            }
            
            
        }
    }

    private void updateCursor() {
        consoleText.GetComponent<TextMesh>().text = textValue + cursor;
    }
}
