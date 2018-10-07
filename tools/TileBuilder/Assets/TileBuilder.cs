using System.Collections;
using System.IO;

using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    public GameObject tiles;
    public RenderTexture rt;
    public string prefix;


    public void buildTile()
    {
        int contador = 1;

        Debug.Log(Application.dataPath);
        foreach (Transform child in tiles.transform)
        {
            StartCoroutine(activeObject(child.gameObject, (0.1f * contador) - 0.05f ));
            contador++;
        }
    }

    IEnumerator activeObject(GameObject child, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Debug.Log("activo.");
        child.gameObject.SetActive(true);

        StartCoroutine(screenshot(child, 0.05f));
    }

    IEnumerator screenshot(GameObject child, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        var oldRT = RenderTexture.active;

        var tex = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();

        File.WriteAllBytes(Application.dataPath + "/build/" + prefix + child.name + ".png", tex.EncodeToPNG());
        RenderTexture.active = oldRT;

        Debug.Log("inactivo.");
        child.SetActive(false);
    }
}
