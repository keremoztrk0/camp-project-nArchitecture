using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Features.Brands.Rules
{
    public class BrandBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        public BrandBusinessRules(IBrandRepository BrandRepository)
        {
            _brandRepository = BrandRepository;
        }

        public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Brand name exists.");
        }
    }
}
