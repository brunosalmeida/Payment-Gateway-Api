using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Domain.Models;
using PaymentGateway.Data.EntityExtensions;

namespace PaymentGateway.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        const string _table = "dbo.Payments";
        private const string _database = "PaymentGatewayDatabase";

        private readonly IConfiguration _configuration;
        
        public PaymentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Payment> Get(Guid id)
        {
            var sql = $"SELECT Id, Amount, Name, Number, Month, Year, CVV, Status, CreatedDate " +
                      $"FROM {_table} WITH (NOLOCK) WHERE ID = @id";

            var dictionary = new Dictionary<string, object> {{"@id", id}};
            var parameters = new DynamicParameters(dictionary);

            await using var connection = new SqlConnection(_configuration.GetConnectionString(_database));
            connection.Open();

            var payment = await connection.QueryFirstAsync<PaymentGateway.Data.Entity.Payment>(sql, parameters);
            return payment.ToDomain();
        }

        public async Task Insert(Payment payment)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {_table}");
            sql.Append(" (Id, Amount, Name, Number, Month, Year, CVV, Status, CreatedDate)");
            sql.Append(" VALUES(@id, @amount, @name, @number, @month, @year, @cvv, @status, @createdDate)");

            var dictionary = new Dictionary<string, object>
            {
                {"@id", payment.Id},
                {"@amount", payment.Amount},
                {"@name", payment.CreditCard.Name},
                {"@number", payment.CreditCard.Number},
                {"@month", payment.CreditCard.Month},
                {"@year", payment.CreditCard.Year},
                {"@cvv", payment.CreditCard.CVV},
                {"@status", payment.Status},
                {"@createdDate", payment.CreatedDate}
            };

            var parameters = new DynamicParameters(dictionary);

            await using var connection = new SqlConnection(_configuration.GetConnectionString(_database));
            connection.Open();

            await connection.ExecuteScalarAsync(sql.ToString(), parameters);
            await Task.CompletedTask;
        }
    }
}