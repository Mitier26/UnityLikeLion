using UnityEngine;


public interface IItemVisitor
{
    void Visit(IItem item);
}

public interface IItem
{
    string Name { get; set; }
    
    void Accept(IItemVisitor visitor);
    void ShowInfo();
    
    void SetitemName(string name);
}



public class WeaponItem : IItem
{
    public string Name { get; set; }

    public void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void ShowInfo()
    {
        Debug.Log(Name);
    }

    public void SetitemName(string name)
    {
        Name = name;
    }
}


public class ArmorItem : IItem
{
    public string Name { get; set; }

    public void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void ShowInfo()
    {
        Debug.Log(Name);
    }

    public void SetitemName(string name)
    {
        Name = name;

    }
}


public class SuwonVisitor : IItemVisitor
{
    public void Visit(IItem item)
    {
        if (item is WeaponItem)
            item.SetitemName("장난감");
        
        if (item is ArmorItem)
        {
            item.SetitemName("망또");
        }
    }
}

public class SeaulVisitor : IItemVisitor
{
    public void Visit(IItem item)
    {
        if (item is WeaponItem)
            item.SetitemName("칼");

        if (item is ArmorItem)
        {
            item.SetitemName("갑옷");
        }
    }
}