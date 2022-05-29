using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BaseObject
{
    Dictionary<string, object> properties;
    public BaseObject()
    {
        properties = new Dictionary<string,object>();
    }
    public void SetProperty(string propertyName, object data)
    {
        properties[propertyName] = data;
    }

    public object GetProperty(string propertyName)
    {
        if (properties.ContainsKey(propertyName))
            return properties[propertyName];
        else return null;
    }

    public void RemoveProperty(string propertyName)
    {
        if (properties.ContainsKey(propertyName))
            properties.Remove(propertyName);
    }
}

