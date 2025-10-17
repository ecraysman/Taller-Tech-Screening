using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Properties;

public static class RESTAdapter
{
    public static ProductDto GetProductDetails([FromRoute]int productId, [FromServices]SOAPMock soap)
    {
        //Call the Mock SOAP API
        var soapBody = soap.GetProduct(productId);
        
        var deserialized = new XmlSerializer(typeof(SOAPMock.GetProductsResponse));
        var response = (SOAPMock.GetProductsResponse)deserialized.Deserialize(new StringReader(soapBody))!;

        if (response != null)
        {
            var product = response.Products.FirstOrDefault();
            if (product != null) return new ProductDto(product.Id, product.Name, product.Description, product.Price);
        }
        
        throw new Exception("Product not found");
    }
    
}