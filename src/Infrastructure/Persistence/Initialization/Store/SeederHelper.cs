using FSH.WebApi.Domain.Store;

namespace FSH.WebApi.Infrastructure.Persistence.Initialization.Store;

public class SeederHelper
{
    // SUPPLIERS
    public static Supplier Supplier1 = new Supplier("PORTLANND LTD", "PORTLANND LIMITED is a global supplier in the Commodities Industries, including Ordinary Portland Cement (all grades and colours), Clinker, Sugar and Steel", "+44 7593-029-860", "hello@portlandltd.com", "https://www.europages.co.uk/PORTLANND-LTD/00000003992355-261132001.html", "https://www.europages.co.uk/filestore/opt/logo/5a/70/11221199_71bd55dd.png");
    public static Supplier Supplier2 = new Supplier("DIRECT CHEMICALS LTD", "Direct Chemicals are THE UK's leading independent distributor of Construction Chemicals, Formwork Chemicals, Waterproofing Systems, Concrete Accessories, Formwork Ancillaries, Joint Sealants, Filler Boards, Grouts & Anchors.", "+44 1234-567-891", "hello@directchemical.com", "http://www.dichem.co.uk/", "https://www.europages.co.uk/filestore/opt/logo/66/e4/16146712_8f612ad6.png");
    public static Supplier Supplier3 = new Supplier("ELKON GMBH", "ELKON is an international company that exports nearly 100% of its production all over the world. To date, we have installed more than 4000 concrete mixing systems in 130 countries. ", "+44 1345-678-901", "hello@concrete.com", "https://www.concretebatchingplants.com/en", "https://www.europages.co.uk/filestore/opt/logo/ce/fa/22607106_6d6a84c8.png");
    public static Supplier Supplier4 = new Supplier("SONNEN METAL", "SONNEN METAL is the application of the best German engineering experience in the design and production of various metal structures.", "+44 1456-789-012", "hello@metal.com", "https://sonnenmetal.com/", "https://www.europages.co.uk/filestore/opt/logo/c0/78/22539491_eff2ecc6.png");

    // PRODUCTS
    public static StoreProduct Product1 = new StoreProduct("Ground mounting system sm-st4h".ToUpper(), "Quick, easy installation thanks to a combination of lower weight (compared to analogs) and original fasteners, allowing panels to be mounted on both sides.", 290.9M, 12, 2, "STRUCTURE", 1M, "https://s.alicdn.com/@sc04/kf/H31789c4b928d4f3e8c5a9242a299c21e0.jpg_720x720q50.jpg");
    public static StoreProduct Product2 = new StoreProduct("Cement coating".ToUpper(), "Powder coating made of cement, sand, water, calcium carbonate, resin and various additives.", 24.84M, 141, 30, "SACK", 1M, "https://www.semin.fr/356-large_default/humiprotect.jpg");
    public static StoreProduct Product3 = new StoreProduct("Vibrating table".ToUpper(), "Compacting of cement mortar and other binding materials", 92.13M, 19, 20, "UNIT", 1M, "https://pagina.emar.com.mx/wp-content/uploads/2021/12/2-EM-E201DM.png");
    public static StoreProduct Product4 = new StoreProduct("Robinia squared timber".ToUpper(), "Our Robinia squared timber has a durability of 30 to 50 years even in the ground. ", 4.88M, 108, 95, "BRICK", 5M, "https://www.garciavarona.com/tienda/wp-content/uploads/2018/05/148-Poste-cuadrado-de-madera-tratada-para-exterior.jpg");
    public static StoreProduct Product5 = new StoreProduct("Gp-rpl050 110w".ToUpper(), "24V DC permanent magnet with 110W output power.", 1.23M, 29, 30, "UNIT", 1M, "https://www.posital.com/media/posital_fsimg/picture-cx-5-crx-mx-nnn.jpg");
    public static StoreProduct Product6 = new StoreProduct("Pocketevo®".ToUpper(), "Transmitter of a new generation", 78.36M, 9, 2, "BOX", 1M, "https://www.hoistmagazine.com/uploads/newsarticle/6035243/images/492156/large/control9.jpg");

    // CUSTOMERS
    public static Customer Customer1 = new Customer("INOXYDA SA", "Inoxyda is a sand casting foundry for large dimension technical parts in corrosion-resistant alloys: aluminium bronze or bronze. ", "+44 1111-222-333", "admin@inoxida.com", "http://www.inoxyda-foundries.com/", "https://www.europages.co.uk/filestore/opt/logo/fe/cf/19097925_56260cf4.png");
    public static Customer Customer2 = new Customer("INNOVARIA - H.E.P.", "Manufacture of copper parts, tubes, manifolds and Y branches for conditioning, refrigeration and heating.", "+44 8444-555-666", "hello@innovaria.com", "http://www.innovaria.it/", "https://www.europages.co.uk/filestore/opt/logo/f5/91/da66e38a-3397-4e4b-84fa-093ef8230142_f0138110.jpg");

}