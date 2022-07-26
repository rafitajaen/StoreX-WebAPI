using FSH.WebApi.Domain.Store;

namespace FSH.WebApi.Infrastructure.Store;

public class SeederHelper
{
    // SUPPLIERS
    public static Supplier Supplier1 = new Supplier("Google LLC.", "Google LLC is an American multinational technology company that focuses on artificial intelligence.", "123-456-789", "tom@google.com", "http://google.es", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/77/Google_Images_2015_logo.svg/800px-Google_Images_2015_logo.svg.png");
    public static Supplier Supplier2 = new Supplier("Amazon Inc.", "American multinational technology company which focuses on e-commerce, cloud computing", "234-567-891", "jeff@amazon.com", "http://amazon.com", "https://m.media-amazon.com/images/I/31%2BDgxPWXtL.jpg");
    public static Supplier Supplier3 = new Supplier("Facebook", "Online social media and social networking service owned by American company Meta Platforms", "345-678-901", "mark@facebook.com", "http://facebook.com", "https://www.redeszone.net/app/uploads-redeszone.net/2022/02/comprobar-inicios-sesion-facebook.jpg");
    public static Supplier Supplier4 = new Supplier("Tesla Inc.", "American multinational automotive and clean energy company headquartered in Austin, Texas", "456-789-012", "elon@tesla.com", "http://tesla.com", "https://openexpoeurope.com/wp-content/uploads/2016/12/tesla-logo-red-300x225.png");

    // PRODUCTS
    public static StoreProduct Product1 = new StoreProduct("ACABADO SILICATO ESTANDAR GRANO 1 mm BOTE 15 KG", string.Empty, 29.9M, 12, "BOTE", 1M, string.Empty);
    public static StoreProduct Product2 = new StoreProduct("AMORT. CANAL SE-TAV-500/11R", string.Empty, 24.84M, 141, "UNIDAD", 1M, string.Empty);
    public static StoreProduct Product3 = new StoreProduct("CASONETO 70X203", string.Empty, 92.13M, 233, "UNIDAD", 2.05M, string.Empty);
    public static StoreProduct Product4 = new StoreProduct("BROCA HILTI 8X160", string.Empty, 4.88M, 108, "UNIDAD", 1M, string.Empty);
    public static StoreProduct Product5 = new StoreProduct("CINTA ESTANCA 70X30X3 AKIFIX", string.Empty, 0.23M, 29, "ROLLO", 1M, string.Empty);
    public static StoreProduct Product6 = new StoreProduct("FOSEADO Z 250X150X150 2000", string.Empty, 7.36M, 216, "UNIDAD", 1M, string.Empty);

}