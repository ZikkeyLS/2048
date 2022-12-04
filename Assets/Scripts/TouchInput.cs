using Lean.Touch;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public enum Direction : byte
    {
        none = 0,
        left,
        right,
        bottom,
        top
    }

    [SerializeField] private GridController _content;
    [SerializeField] private LeanTouch _touch;

    private void Start()
    {
        LeanTouch.OnFingerInactive += (finger) => 
        {
            if(GlobalData.InMenu == false)
            {
                Direction direction = Direction.none;
                Vector2 scaledDelta = finger.SwipeScaledDelta;

                float xAbs = Mathf.Abs(scaledDelta.x);
                float yAbs = Mathf.Abs(scaledDelta.y);

                if(xAbs > 50 || yAbs > 50)
                {
                    if (xAbs > yAbs)
                    {
                        if (scaledDelta.x > 0)
                            direction = Direction.right;
                        else if (scaledDelta.x < 0)
                            direction = Direction.left;
                    }
                    else if (xAbs < yAbs)
                    {
                        if (scaledDelta.y > 0)
                            direction = Direction.top;
                        else if (scaledDelta.y < 0)
                            direction = Direction.bottom;
                    }

                    if (direction != Direction.none)
                        _content.Swipe(direction);
                }
            }
        };
    }
}
