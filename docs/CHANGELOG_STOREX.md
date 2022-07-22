# StoreX WebAPI Changelog

## v0.0.2 - [Add Permissions for Warehouse Entities and Update Controllers]()

**branch :** _storex_

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
