using Example.Application.Common;
using Example.Application.ExampleService.Models.Dtos;
using Example.Application.ExampleService.Models.Request;
using Example.Application.ExampleService.Models.Response;
using Example.Infra.Data;
using Example.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.ExampleService.Service
{
    public class PessoaService : BaseService<PessoaService>, IPessoaService
    {
        private readonly PessoaContext _db;

        public PessoaService(ILogger<PessoaService> logger, Infra.Data.Mapping.PessoaContext db) : base(logger)
        {
            _db = db;
        }

        public async Task<GetAllPessoaResponse> GetAllAsync()
        {
            var entity = await _db.Pessoa.ToListAsync();
            return new GetAllPessoaResponse()
            {
                Pessoas = entity != null ? entity.Select(a => (PessoaDto)a).ToList() : new List<PessoaDto>()
            };
        }

        public async Task<GetByIdPessoaResponse> GetByIdAsync(int id)
        {

            var response = new GetByIdPessoaResponse();

            var entity = await _db.Pessoa.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null) response.Pessoa = (PessoaDto)entity;

            return response;
        }

        public async Task<CreatePessoaResponse> CreateAsync(CreatePessoaRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var newPessoa = Domain.ExampleAggregate.Pessoa.Create(request.Nome, request.CPF, request.Id_Cidade, request.Idade );

            _db.Pessoa.Add(newPessoa);

            await _db.SaveChangesAsync();

            return new CreatePessoaResponse() { Id = newPessoa.Id };
        }

        public async Task<UpdatePessoaResponse> UpdateAsync(int id, UpdatePessoaRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request empty!");

            var entity = await _db.Pessoa.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null)
            {
                entity.Update(request.Nome, request.CPF, request.Id_Cidade, request.Idade);
                await _db.SaveChangesAsync();
            }

            return new UpdatePessoaResponse();
        }

        public async Task<DeletePessoaResponse> DeleteAsync(int id)
        {

            var entity = await _db.Pessoa.FirstOrDefaultAsync(item => item.Id == id);

            if (entity != null)
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return new DeletePessoaResponse();
        }
    }
}
