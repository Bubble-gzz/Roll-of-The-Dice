using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest
{
    public static void Save(LevelInfo levelInfo)
    {
        string js = JsonUtility.ToJson(levelInfo);
        Debug.Log(js);
        string fileUrl = Application.streamingAssetsPath + "\\jsonInfo.json";
        //打开或者新建文档
        using (StreamWriter sw = new StreamWriter(fileUrl))
        {
            //保存数据
            sw.WriteLine(js);
            //关闭文档
            sw.Close();
            sw.Dispose();
        }
    }

    public static LevelInfo Read()
    {
        //string类型的数据常量
        string readData;
        //获取到路径
        string fileUrl = Application.streamingAssetsPath + "\\jsonInfo.json";
        //读取文件
        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //数据保存
            readData = sr.ReadToEnd();
            sr.Close();
        }

        LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(readData);
        return levelInfo;
    }
}
