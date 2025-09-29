using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestControlableShit : Selectable
{
    public override void Select()
    {
        base.Select();
        ControlInterface.Buttons[0].onClick.AddListener(SayBitch);
        ControlInterface.Buttons[1].onClick.AddListener(SayNigger);
        ControlInterface.Buttons[2].onClick.AddListener(SayBitch);
    }

    private void SayNigger()
    {
        Debug.Log("Nigger");
    }
    private void SayBitch()
    {
        Debug.Log("Bitch");
    }
    private void SayFuck()
    {
        Debug.Log("Fuck");
    }
}
