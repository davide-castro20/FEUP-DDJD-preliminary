using UnityEngine;

public class ResetablePressurePlateScript : PressurePlateScript
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(_plateReseters.Contains(other.gameObject.tag))
        {
            if (--nColisions == 0)
            {
                _spriteRenderer.sprite = not_pressed;
                _bs.Deactivate();
                _isPressed = false;
            }
        }
    }
}
