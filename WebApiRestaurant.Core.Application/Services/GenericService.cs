﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class

    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Add(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.AddAsync(entity);
        }

        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task Delete(int id)
        {
            Entity entity = await _repository.GetById(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entityList = await _repository.GetAll();
            return _mapper.Map<List<ViewModel>>(entityList);

        }

        public virtual async Task<ViewModel> GetById(int id)
        {
            Entity entity = await _repository.GetById(id);
            ViewModel saveVm = _mapper.Map<ViewModel>(entity);
            return saveVm;
        }


    }
}
