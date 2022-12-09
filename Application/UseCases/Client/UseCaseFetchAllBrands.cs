using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchAllBrands:
    IUseCaseQuery<IEnumerable<DtoOutputBrands>>
{
    private readonly IBrandRepository _brandRepository;

    public UseCaseFetchAllBrands(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public IEnumerable<DtoOutputBrands> Execute()
    {
        var brands = _brandRepository.FetchAll();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputBrands>>(brands);
    }
}