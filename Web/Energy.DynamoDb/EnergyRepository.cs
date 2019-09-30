using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Energy.Core.Interfaces;
using Energy.Repository.DynamoDb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Energy.Repository.DynamoDb
{
    public class EnergyRepository : IEnergyRepository
    {
        private readonly DynamoDBContext _context;

        public EnergyRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<IEnergyData>> Get(Guid device, int maxItems, DateTime? from, DateTime? to)
        {
            var filter = new List<ScanCondition>(maxItems);

            if (from.HasValue) filter.Add(new ScanCondition("timestamp", ScanOperator.GreaterThanOrEqual, new[] { from }));
            if (to.HasValue) filter.Add(new ScanCondition("timestamp", ScanOperator.LessThanOrEqual, new[] { to }));
            var result = await _context.ScanAsync<EnergyData>(filter).GetRemainingAsync();
            return result;
        }
    }
}
