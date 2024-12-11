using System;

public class Product
{
    public int ID { get; set; }    
    public int CarPartID { get; set; }
    public int CarID { get; set; }    
    public string OEM { get; set; } = string.Empty;    
    public decimal Price { get; set; }   
    public DateTime DateAvailable { get; set; }       
    public string Condition { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public bool ItemAvailable { get; set; }
    public string Name { get; set; } = string.Empty;
}


