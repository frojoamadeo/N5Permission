using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Xunit;
using Persistance;
using N5_PermissionManagement.TestDoubles.Infraestructure;
using Shouldly;

namespace Infraestructure.Services
{
    public sealed class ElasticSearchServiceTests
    {
        private readonly PermissionDbContext _permissionDbContext;

        public ElasticSearchServiceTests()
        {
            _permissionDbContext = PermissionDbContextTestFactory.CreateReadWriteDb();
        }
    }
}
