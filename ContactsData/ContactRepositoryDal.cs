using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla.Data;
using Microsoft.Extensions.Configuration;

namespace ContactsData
{
    public class ContactRepositoryDal : IContactRepository
    {
        private readonly IConfiguration _configuration;

        public ContactRepositoryDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Delete(int id)
        {
            return true;
        }

        public bool Exists(int id)
        {
            //TODO DONT NEED THIS IN HERE
            //var person = _contactsTable.FirstOrDefault(p => p.Id == id);
            //return person != null;
            return false;
        }

        public ContactEntity Get(int id)
        {
            ContactEntity contactEntity = null;

            string connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Get_Contact_By_Id", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@id", id));

                    var dataset = new DataSet();
                    dataset.Load(command.ExecuteReader(), LoadOption.OverwriteChanges, "Contacts");

                    if (dataset.Tables["Contacts"].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataset.Tables["Contacts"].Rows)
                        {
                            contactEntity = new ContactEntity
                            {
                                Id = row.Field<int>("ID"),
                                Firstname = row.Field<string>("Firstname"),
                                Lastname = row.Field<string>("Lastname"),
                                Email = row.Field<string>("Email")
                            };
                            break;
                        }
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Id {id}");
                    }
                }
            }
            return contactEntity;
        }

        public List<ContactEntity> Get()
        {
            string connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Get_Contacts_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var dataset = new DataSet();
                    dataset.Load(command.ExecuteReader(), LoadOption.OverwriteChanges, "Contacts");

                    var list = new List<ContactEntity>();
                    foreach (DataRow row in dataset.Tables["Contacts"].Rows)
                    {
                        var contactEntity = new ContactEntity
                        {
                            Id = row.Field<int>("ID"),
                            Firstname = row.Field<string>("Firstname"),
                            Lastname = row.Field<string>("Lastname"),
                            Email = row.Field<string>("Email")
                        };
                        list.Add(contactEntity);
                    }
                    return list;
                }
            }
        }

        public ContactEntity Insert(ContactEntity contact)
        {
            string connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Insert_Contact_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Firstname", contact.Firstname);
                    command.Parameters.AddWithValue("@Lastname", contact.Lastname);
                    command.Parameters.AddWithValue("@Email", contact.Email);

                    SqlParameter idParameter = command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    idParameter.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    contact.Id = (int)command.Parameters["@Id"].Value;
                }
            }
            return contact;
        }

        public ContactEntity Update(ContactEntity contact)
        {
            return null;
        }
    }
}
