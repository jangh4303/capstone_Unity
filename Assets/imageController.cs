using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class imageController : MonoBehaviour
{
    public ObjectEvent obevent;
    public MeshRenderer quad;



    void Start()
    {
        quad = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obevent.touched)
        {
            NativeGallery.GetImageFromGallery((file) =>
            {
                FileInfo selected = new FileInfo(file);

                // 용량 제한
                if (selected.Length > 50000000)
                {
                    obevent.touched = false;
                    return;
                }

                //불러오기
                if (!string.IsNullOrEmpty(file))
                {
                    StartCoroutine(LoadImage(file));
                } 

            });


            obevent.touched = false;
        }
    }

    IEnumerator LoadImage(string path)
    {
        yield return null;
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/Image";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }


        File.WriteAllBytes(savePath+fileName+".png",fileData);

        var temp = File.ReadAllBytes(savePath + fileName + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);
        quad.material.mainTexture = tex;

    }

}
