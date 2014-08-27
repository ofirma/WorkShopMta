using System;
using PicPoint.Models;

public class DBEntities
{
    private static Database1Entities1 instance;

    private DBEntities() { }

    public static Database1Entities1 Proxy
    {
        get
        {
            if (instance == null)
            {
                instance = new Database1Entities1();
            }
            return instance;
        }
    }

    public static string[] ParseString(string id)
    {
        return id.Split(' ');
    }
}