using Example.Application.Common;
using Example.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Example.Application.ExampleService.Models.Dtos;
using Example.Application.ExampleService.Service;
using Example.Application.ExampleService.Models.Response;
using Example.Application.ExampleService.Models.Request;
using Example.Infra.Data.Mapping;

namespace Example.Application.CidadeService.Service
{
    public class CidadeService : BaseService<CidadeService>, ICidadeService
    {
        private readonly CidadeContext _db;
         
        public CidadeService(ILogger<CidadeService> logger, Infra.Data.Mapping.CidadeContext db) : base(logger)
        {
            _db = db;
        }

        public async Task<GetAllCidadeResponse> GetAllAsync()
        {
            var entity = await _db.Cidade.ToListAsync();
            return new GetAllCidadeResponse()
            {
                Cidades = entity != null ? entity.Select(a => (CidadeDto)a).ToList() : new List<CidadeDto>()
            };
        }

        public async Task<GetByIdCidadeResponse> GetByIdAsync(int id)
        {

            var response = new GetByIdCidadeResponse();

            var entity = await _db.Cidade.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null) response.Cidade = (CidadeDto)entity;

            return response;
        }

        public async Task<CreateCidadeResponse> CreateAsync(CreateCidadeRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var newCidade = Domain.ExampleAggregate.Cidade.Create(request.Nome, request.UF);

            _db.Cidade.Add(newCidade);

            await _db.SaveChangesAsync();

            return new CreateCidadeResponse() { Id = newCidade.Id };
        }

        public async Task<UpdateCidadeResponse> UpdateAsync(int id, UpdateCidadeRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var entity = await _db.Cidade.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null)
            {
                entity.Update(request.Nome, request.UF);
                await _db.SaveChangesAsync();
            }

            return new UpdateCidadeResponse();
        }

        public async Task<DeleteCidadeResponse> DeleteAsync(int id)
        {

            var entity = await _db.Cidade.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null)
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return new DeleteCidadeResponse();
        }
    }
}
