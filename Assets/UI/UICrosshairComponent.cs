using System.Collections;
using UnityEngine;

public class UICrosshairComponent : MonoBehaviour
{
    internal enum ePushDirection : ushort
    {
        NORTH,
        EAST,
        WEST,
        SOUTH
    };

    [SerializeField] ePushDirection PushingDirection;

    Vector2 OriginalPosition;
    Vector2 PositionLimit;
    Vector2 MovePositionRate;

    RectTransform RectTf;
    [SerializeField] float MovingIntensity = 1.5f;
    [SerializeField] float MovingSpeed = 1.5f;
    float SpreadStep = 0.2f;
    [SerializeField] float MinSpread = 0.8f;
    [SerializeField] float MaxSpread = 1.5f;
    bool IsFiring = false;

    Coroutine CrosshairCoroutine;

    void Start()
    {
        RectTf = GetComponent<RectTransform>();
        OriginalPosition.Set(RectTf.anchoredPosition.x, RectTf.anchoredPosition.y);
        MovePositionRate = OriginalPosition * MovingIntensity;
        PositionLimit = OriginalPosition + MovePositionRate;
        CustomDebug.Log($"Current Moving Rate is [{MovePositionRate.x.ToString()}, {MovePositionRate.y.ToString()}].");
    }

    public void OnShotFired()
    {
        CrosshairCoroutine = StartCoroutine(_MoveCrosshair());
    }

    IEnumerator _MoveCrosshair()
    {
        // Open Crosshair to limit position.
        switch (PushingDirection)
        {
            case ePushDirection.NORTH:
                while (RectTf.anchoredPosition.y <= PositionLimit.y)
                {
                    RectTf.anchoredPosition += new Vector2(0.0f, MovePositionRate.y * MovingSpeed * Time.deltaTime);
                    yield return Yielder.GetCoroutine();
                }
                while (RectTf.anchoredPosition.y >= OriginalPosition.y)
                {
                    RectTf.anchoredPosition -= new Vector2(0.0f, MovePositionRate.y * MovingSpeed * Time.deltaTime);
                    yield return Yielder.GetCoroutine();
                }
                break;

            case ePushDirection.EAST:
                while (RectTf.anchoredPosition.x <= PositionLimit.x)
                {
                    RectTf.anchoredPosition += new Vector2(MovePositionRate.x * MovingSpeed * Time.deltaTime, 0.0f);
                    yield return Yielder.GetCoroutine();
                }
                while (RectTf.anchoredPosition.x >= OriginalPosition.x)
                {
                    RectTf.anchoredPosition -= new Vector2(MovePositionRate.x * MovingSpeed * Time.deltaTime, 0.0f);
                    yield return Yielder.GetCoroutine();
                }
                break;

            case ePushDirection.WEST:
                while (RectTf.anchoredPosition.x >= PositionLimit.x)
                {
                    RectTf.anchoredPosition += new Vector2(MovePositionRate.x * MovingSpeed * Time.deltaTime, 0.0f);
                    yield return Yielder.GetCoroutine();
                }
                while (RectTf.anchoredPosition.x <= OriginalPosition.x)
                {
                    RectTf.anchoredPosition -= new Vector2(MovePositionRate.x * MovingSpeed * Time.deltaTime, 0.0f);
                    yield return Yielder.GetCoroutine();
                }
                break;

            case ePushDirection.SOUTH:
                while (RectTf.anchoredPosition.y >= PositionLimit.y)
                {
                    RectTf.anchoredPosition += new Vector2(0.0f, MovePositionRate.y * MovingSpeed * Time.deltaTime);
                    yield return Yielder.GetCoroutine();
                }
                while (RectTf.anchoredPosition.y <= OriginalPosition.y)
                {
                    RectTf.anchoredPosition -= new Vector2(0.0f, MovePositionRate.y * MovingSpeed * Time.deltaTime);
                    yield return Yielder.GetCoroutine();
                }
                break;
        }
    }
};
