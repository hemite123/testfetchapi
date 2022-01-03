using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Category
{
    public string id;
    public string nama;
    public string image;
    public string show_at_home;
}

public class CategoryList
{
    [SerializeField]
    public List<Category> category = new List<Category>();
}
