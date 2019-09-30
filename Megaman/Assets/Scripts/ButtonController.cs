using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Text textField;

    private float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        delayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            textField.text = ((int)delayTime + 1) + "";
            if (delayTime <= 0)
            {
                textField.text = null;
                gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void SetDelay(float delay)
    {
        this.delayTime = delay;
        gameObject.GetComponent<Button>().interactable = false;
    }
}