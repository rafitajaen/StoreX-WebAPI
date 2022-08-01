# StoreX WebAPI Changelog

## v0.0.8 - [Create Request for Office Side Models]()

**branch :** _storex_

    Add:
        - src/Core/Application/Store : Customer, Project, Quotation, QuotationProduct, Delivery, DeliveryProduct, Invoice, InvoiceProduct
        - src/Host/Controllers/Store : Customer, Project, Quotation, QuotationProduct, Delivery, DeliveryProduct, Invoice, InvoiceProduct
        - src/Core/Shared/Authorization/FSHPermissions.cs : Add Permissions for new Models.
        - src/Infrastructure/Persistance/Configuration/ : Add EntityTypeConfiguration
        - src/Infrastructure/Persistance/Context/ : Add new Tables and Relationships to Context

    Edit:
        - src/Core/Domain/Store : Client Model is now Customer.dot
        - src/Migrators : Update InitializeDb Migrator
        - src/Infrstructure/Store : Update Seeder

## v0.0.7 - [Create Models for Office Side](https://github.com/rafitajaen/StoreX-WebAPI/tree/3d4adddcd583832e50e28c39e091a177c979e824)

**branch :** _storex_

    Add:
        - src/Core/Domain/Store : Client, Project, Quotation, QuotationProduct, Delivery, DeliveryProduct, Invoice, InvoiceProduct

## v0.0.6 - [Bugfixes in StoreProducts](https://github.com/rafitajaen/StoreX-WebAPI/tree/5d7e5e920b1ec5a1cf18aac20bbd5ecfb5a33878)

**branch :** _storex_

    Edit:
        - src/Core/Application/Store : Small bugfixes StoreProducts

## v0.0.5 - [Bugfixes in OrderProducts](https://github.com/rafitajaen/StoreX-WebAPI/tree/1566be581cc9537c3cc2520a100a1dd59a6be320)

**branch :** _storex_

    Edit:
        - src/Core/Application/Store : Small bugfixes OrderProducts

## v0.0.4 - [Make OrderProducts Searchable](https://github.com/rafitajaen/StoreX-WebAPI/tree/c52dda8c77ef7584a8d54af56bcc61c06f2603bc)

**branch :** _storex_

    Edit:
        - src/Core/Application/Store : Small bugfixes to make OrderProducts Searchable

## v0.0.3 - [Independent Store Folder and All in one Seeder](https://github.com/rafitajaen/StoreX-WebAPI/tree/9bfacfcfd48b5ce259db79cabfd7312bd45e9413)

**branch :** _storex_

    Add:
        - Move Models, Requests, Controllers and Configuration to Store Folder to be independent of original project and avoid merge conflicts
        - src/Infrastructure/Persistence/Store : Seeders for Products and OrderProducts
        - src/Core/Store/OrderProduct : Get OrderProduct By Order Id
        - src/Core/Store/OrderProduct : OrderProductDetailsDto
        - src/Infrstructure/Store : All in one Seeder

    Edit:
        - src/Host/Controllers/Store : OrderProduct Controller

## v0.0.2 - [Add Permissions for Warehouse Entities and Update Controllers](https://github.com/rafitajaen/StoreX-WebAPI/tree/da5618d83cec6719a1a638575435a04393cb7575)

**branch :** _storex_

    Add:
        - src/Infraestructure/Common : Suppliers Seeder

    Edit:
        - src/Core/Shared/Authorization/FSHPermissions.cs : Add Permissions for (Suppliers, Orders and OrderProducts)
        - src/Host/Controllers/Catalog/ : Fix Permission attributes in each controller.

## v0.0.1 - [Add Warehouse side entities, requests and controllers](https://github.com/rafitajaen/StoreX-WebAPI/tree/53cbabeb04a22403656c18ef7b04e94b7d0dfd01)

**branch :** _storex_

    Add:
        - /docs/ : Create Readme and Changelog to log my progress.
        - src/Core/Domain/Catalog : Create Entities for Warehouse side. (Provider, Order, Product)
        - src/Application/Catalog/ : Create Requests
        - src/Host/Controllers/Catalog : Create Controllers
        - src/Core/Shared/Authorization : Add New Permissions
        - src/Infrastructure/Persistance/Configuration/ : Add EntityTypeConfigurations
        - src/Infrastructure/Persistance/Context/ : Add DbSets for the new Entities and a Many to Many Relationship

    Edit:
        - src/Host/Configuration/mail.json: Edit configuration for new users emails confirmations (Not commit because i don't know yet to use User Secrets)
        - Edit Initial Migration with new Entities
