using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public event Action CatchPressed;
    public event Action CatchReleased;

    public Vector2 Controls { get; private set; }

    private void Update()
    {
        Controls = Input.GetAxis(HORIZONTAL) * Vector2.up + Input.GetAxis(VERTICAL) * Vector2.right;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            CatchPressed?.Invoke();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            CatchReleased?.Invoke();
    }

}