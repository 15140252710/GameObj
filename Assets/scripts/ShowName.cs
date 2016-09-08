using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowName : MonoBehaviour {

    private Text text;

    void OnMouseUpAsButton() {
        if (text == null) {
            text = GameObject.Find("TestText").GetComponent<Text>();
        }
        text.text = gameObject.name;
    }

}
