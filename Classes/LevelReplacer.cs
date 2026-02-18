namespace EllaKillea.Classes;

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReplacer : MonoBehaviour
{
    public void Awake() => SceneManager.sceneLoaded += LoadScene;

    public void LoadScene(Scene _, LoadSceneMode __) => StartCoroutine(LoadSceneDelayed());

    public IEnumerator LoadSceneDelayed()
    {
        yield return null;

        if (SceneHelper.CurrentScene == "Tutorial")
            ChangeLighting(new Color(0.4f, .5f, 1));
        else if (SceneHelper.CurrentScene == "Level 0-1")
            ChangeLighting(new Color(0.5f, .5f, 1));
        else if (SceneHelper.CurrentScene == "Level 0-2")
            ChangeLighting(new Color(0.6f, .5f, 1));
        else if (SceneHelper.CurrentScene == "Level 0-3")
            ChangeLighting(new Color(0.7f, .5f, 1));
        else if (SceneHelper.CurrentScene == "Level 0-4")
        {
            foreach (var obj in FindObjectsOfType<MeshRenderer>(true))
                if (obj.name == "Heat" && obj.transform.parent != null)
                {
                    obj.GetComponent<MeshRenderer>().material.color = new Color(.05f, .1f, .4f, 1f);
                    var p = obj.transform.parent.GetComponent<MeshRenderer>();
                    var pp = obj.transform.parent.parent.GetComponent<Spin>();
                    if (p != null)
                        foreach (var m in p.materials)
                            m.color = new Color(0, 0, 0, 1);
                    if (pp != null)
                    {
                        pp.transform.localEulerAngles += pp.spinDirection * Random.Range(0f, 100f);
                        pp.enabled = false;
                        p.transform.Find("HurtZone").gameObject.SetActive(false);
                    }
                }
            foreach (var die in FindObjectsOfType<FogEnabler>(true))
                Destroy(die);

            ChangeLighting(new Color(0.8f, .5f, 1));
        }
        else if (SceneHelper.CurrentScene == "Level 0-5")
            ChangeLighting(new Color(0.9f, .5f, 1));
        else if (SceneHelper.CurrentScene == "Level 0-S")
            ChangeLighting(new Color(0f, .3f, 1));
    }

    public static void ChangeLighting(Color lightColor)
    {
        foreach (Light l in FindObjectsOfType<Light>(true))
        {
            l.color = lightColor;
        }
        RenderSettings.fogColor = lightColor;
    }
}