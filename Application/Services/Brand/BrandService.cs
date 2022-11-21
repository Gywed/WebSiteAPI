using Infrastructure.Ef;

namespace Application.Services.Brand;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public Domain.Brand FetchById(int brandId)
    {
        var dbBrand = _brandRepository.FetchById(brandId);

        return Mapper.GetInstance().Map<Domain.Brand>(dbBrand);
    }
}