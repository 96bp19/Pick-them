using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputData Inputs;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Inputs.screenTouched = true;
        }
        else
        {
            Inputs.screenTouched = false;
        }
    }


}
public struct InputData
{
    public bool screenTouched;
}
