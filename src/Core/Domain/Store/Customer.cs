namespace FSH.WebApi.Domain.Store;

public class Customer : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? WebsiteUrl { get; private set; }
    public string? ImagePath { get; private set; }

    public Customer(string name, string? description, string? phone, string? email, string? websiteUrl, string? imagePath)
    {
        Name = name;
        Description = description;
        Phone = phone;
        Email = email;
        WebsiteUrl = websiteUrl;
        ImagePath = imagePath;
    }

    public Customer Update(string name, string? description, string? phone, string? email, string? websiteUrl, string? imagePath)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (phone is not null && Phone?.Equals(phone) is not true) Phone = phone;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (websiteUrl is not null && WebsiteUrl?.Equals(websiteUrl) is not true) WebsiteUrl = websiteUrl;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        return this;
    }

    public Customer ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}