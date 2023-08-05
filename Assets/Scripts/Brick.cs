using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;

    public int PointValue;
    private int hitsLeft;

    void Start()
    {
        hitsLeft = Settings.Instance.ActiveSettings.multiHitBricks ? PointValue : 1;
        SetColor(PointValue);
    }

    private void SetColor(int colorIndex)
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (colorIndex)
        {
            case 0:
                block.SetColor("_BaseColor", Color.white);
                break;
            case 1:
                block.SetColor("_BaseColor", Color.green);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        --hitsLeft; if (hitsLeft == 0)
        {
            onDestroyed.Invoke(PointValue);
            StopCoroutine(nameof(ResetColor));

            //slight delay to be sure the ball have time to bounce
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 0.2f);
        }
        else
        {
            SetColor(0);
            StartCoroutine(ResetColor());
        }
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.2f);
        SetColor(PointValue);
    }
}
