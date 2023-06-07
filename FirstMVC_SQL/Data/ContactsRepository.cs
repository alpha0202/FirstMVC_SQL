using Dapper;
using FirstMVC_SQL.Models;
using System.Data;

namespace FirstMVC_SQL.Data
{
    public class ContactsRepository : IContactsRepository   
    {

        private readonly IDbConnection  _dbconnection;

        public ContactsRepository(IDbConnection dbConnection)
        {
            _dbconnection = dbConnection;
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            var sql = @"select Id, FirstName, LastName, Phone, Address from Contacts";

            return await _dbconnection.QueryAsync<Contact>(sql, new { });
        }
        public  async Task<Contact> GetDetails(int id)
        {
            var sql = @"select Id, FirstName, LastName, Phone, Address 
                        from Contacts
                        where Id = @Id ";

            return await _dbconnection.QueryFirstOrDefaultAsync<Contact>(sql, new { Id = id});
        }
       
        
        public async Task Insert(Contact contact)
        {
            var sql = @" insert into Contacts(FirstName, LastName, Phone, Address)
                            values(@FirstName, @LastName,@Phone,@Address)";

             await _dbconnection.ExecuteAsync(sql, new
             { contact.FirstName,
               contact.LastName,
               contact.Phone,
               contact.Address              
                });
        }

        public async Task Update(Contact contact)
        {
            var sql = @" update Contacts 
                                set FirstName = @FirstName , 
                                    LastName = @LastName, 
                                    Phone = @Phone, 
                                    Address = @Address                          
                                 where Id = @Id ";

            await _dbconnection.ExecuteAsync(sql, new
            {
                contact.Id, 
                contact.FirstName,
                contact.LastName,
                contact.Phone,
                contact.Address
            });
        }

        public async Task Delete(int id)
        {
            var sql = @"delete from Contacts where Id = @Id";

            await _dbconnection.ExecuteAsync(sql, new { Id = id});
        }




    }
}
