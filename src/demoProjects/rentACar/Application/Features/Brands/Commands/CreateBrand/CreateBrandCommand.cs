﻿using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }

    }
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;
        public CreateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

            
            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
            CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);
            
            
            return createdBrandDto;

        }
    }
}
