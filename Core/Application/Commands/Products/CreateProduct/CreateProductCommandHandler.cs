using Application.Interfaces;
using Domain.Model;
using MediatR;

namespace Application.Commands.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileStorageService _fileStorage;

    public CreateProductCommandHandler(
        IApplicationDbContext context,
        IFileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        string? photoPath = null;

        // Сохраняем фото, если оно есть
        if (request.Photo != null) photoPath = await _fileStorage.SaveFileAsync(request.Photo, "products");

        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            UserId = request.CurrentUserId,
            ProductName = request.ProductName,
            YpkId = request.YpkId,
            StatusProductId = request.StatusProductId,
            ProductCost = request.ProductCost,
            ProductInfo = request.ProductInfo,
            IsProduct = request.IsProduct,
            PhotoPath = photoPath,
            Adres = request.Adres
        };

        await _context.Products.AddAsync(newProduct, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newProduct.Id;
    }
}