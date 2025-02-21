using AppServices.Interfaces.IServices;
using Domain.Entities;
using Domain.Settings;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public sealed class ElasticSearchService : IElasticSearchService
    {
        public readonly ElasticsearchClient _elasticsearchClient;
        public readonly ElasticSettings _elasticSettings;

        public ElasticSearchService(IOptions<ElasticSettings> options)
        {
            _elasticSettings = options.Value;

            var settings = new ElasticsearchClientSettings(new Uri(_elasticSettings.Url))
                .DefaultIndex(_elasticSettings.DefaultIndex);

            _elasticsearchClient = new ElasticsearchClient(settings);
        }

        public async Task<bool> AddOrUpdateEmployeePermission(EmployeePermission EmployeePermission)
        {
            var response = await _elasticsearchClient.IndexAsync(EmployeePermission, x =>
                x.Index(_elasticSettings.DefaultIndex)
                .OpType(OpType.Index));

            return response.IsValidResponse;
        }

        public async Task<bool> AddOrUpdateEmployeePermissionBulk(IEnumerable<EmployeePermission> EmployeePermissions, string indexName)
        {
            var response = await _elasticsearchClient.BulkAsync(b => b.Index(_elasticSettings.DefaultIndex)
                .UpdateMany(EmployeePermissions,
                    (ud, u) => ud.Doc(u).DocAsUpsert(true)));

            return response.IsValidResponse;
        }

        public async Task CreateIndexIfNotExistAsync(string indexName)
        {
            if (!_elasticsearchClient.Indices.Exists(indexName).Exists)
            {
                await _elasticsearchClient.Indices.CreateAsync(indexName);
            }
        }

        public async Task<IEnumerable<EmployeePermission>?> GetAllEmployeePermissions(string key)
        {
            var response = await _elasticsearchClient.SearchAsync<EmployeePermission>(key, g =>
                g.Index(_elasticSettings.DefaultIndex));

            return response.IsValidResponse ? response.Documents.ToList() : default;
        }

        public async Task<EmployeePermission> GetEmployeePermission(string key)
        {
            var response = await _elasticsearchClient.GetAsync<EmployeePermission>(key, g =>
                g.Index(_elasticSettings.DefaultIndex));

            return response.Source;
        }

        public Task<long?> RemoveAllEmployeePermission()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveEmployeePermission(string key)
        {
            throw new NotImplementedException();
        }
    }
}
