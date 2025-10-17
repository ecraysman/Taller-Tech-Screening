using System.Xml.Serialization;
using Microsoft.Net.Http.Headers;

namespace WebApplication3.Properties;

public class SOAPMock
{
// Mock the call to SOAP API

    public string GetProduct(int Id)
    {
        var response = new GetProductsResponse
        {
            Products = new List<Product>
            {
            }
        };

        switch (Id)
        {
            case 1: 
                response.Products.Add(new()
                {
                    Id = 1, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse with 2.4GHz receiver",
                    Price = 29.99m
                });
                break;
            case 2: 
                response.Products.Add(new()
                {
                    Id = 2, Name = "Mechanical Keyboard", Description = "RGB backlit mechanical keyboard with blue switches", Price = 89.50m 
                });
                break;
            case 3: 
                response.Products.Add(new()
                {
                    Id = 3, Name = "USB-C Hub", Description = "7-in-1 USB-C hub with HDMI, USB 3.0, and SD card reader", Price = 45.00m
                });
                break;
        }
        
        var serializer = new XmlSerializer(typeof(GetProductsResponse));
        using var writer = new StringWriter();
        serializer.Serialize(writer, response);
        string soapBody = writer.ToString();
        
        return soapBody;
    }


    #region SoapMockup

    [Serializable]
    [XmlRoot("GetProductsResponse", Namespace = "http://example.com/products")]
    public class GetProductsResponse
    {
        [XmlArray("Products")]
        [XmlArrayItem("Product")]
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    #endregion   
}