﻿using AutoMapper;
using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
using FlowerApp.Service.Storages;
using Flower = FlowerApp.DTOs.Common.Flowers.Flower;

namespace FlowerApp.Service.Services;

public class FlowersService : IFlowersService
{
    private readonly IFlowersStorage flowersStorage;
    private readonly IMapper mapper;

    public FlowersService(IFlowersStorage flowersStorage, IMapper mapper)
    {
        this.flowersStorage = flowersStorage;
        this.mapper = mapper;
    }

    public async Task<GetFlowerResponse> Get(
        string? searchString,
        Pagination pagination,
        FlowerFilterParams filterParams,
        FlowerSortOptions sortOptions
    )
    {
        var response = await flowersStorage.Get(searchString, pagination, filterParams, sortOptions);
        return response;
    }

    public async Task<List<Flower>> Get(int id)
    {
        var flower = await flowersStorage.Get(id);
        return new List<Flower> { mapper.Map<Flower>(flower) };
    }
}