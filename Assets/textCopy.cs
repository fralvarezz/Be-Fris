using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textCopy : MonoBehaviour
{
    public Transform otherText;
    public TextMeshProUGUI text;
    TextMeshProUGUI myText;
    private void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = otherText.position + Vector3.one * 10;
        transform.rotation = otherText.rotation;
        myText.text = text.text;
        transform.localScale = otherText.localScale;
    }
}
