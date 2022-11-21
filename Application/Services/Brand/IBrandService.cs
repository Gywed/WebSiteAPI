namespace Application.Services.Brand;

public interface IBrandService
{
    Domain.Brand FetchById(int brandId);
}