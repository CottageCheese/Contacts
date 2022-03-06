using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ContactsData.Interfaces;
using Csla.Data;
using Microsoft.Extensions.Configuration;

namespace ContactsData
{
    public class ContactRepositoryDal : IRepository<ContactDto>
    {
        private readonly IConfiguration _configuration;

        public ContactRepositoryDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Delete(int id)
        {
            var connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Delete_Contact_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }

        public bool Exists(int id)
        {
            return Get(id) != null;
        }

        public ContactDto Get(int id)
        {
            ContactDto contactDto = null;

            var connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Get_Contact_By_ID_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@id", id));

                    var dataset = new DataSet();
                    dataset.Load(command.ExecuteReader(), LoadOption.OverwriteChanges, "Contacts");

                    if (dataset.Tables["Contacts"].Rows.Count > 0)
                        foreach (DataRow row in dataset.Tables["Contacts"].Rows)
                        {
                            contactDto = new ContactDto
                            {
                                Id = row.Field<int>("ID"),
                                Firstname = row.Field<string>("Firstname"),
                                Lastname = row.Field<string>("Lastname"),
                                Email = row.Field<string>("Email")
                            };
                            break;
                        }
                    else
                        throw new KeyNotFoundException($"Id {id}");
                }
            }

            return contactDto;
        }

        public List<ContactDto> Get()
        {
            var connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Get_Contacts_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var dataset = new DataSet();
                    dataset.Load(command.ExecuteReader(), LoadOption.OverwriteChanges, "Contacts");

                    var list = new List<ContactDto>();
                    foreach (DataRow row in dataset.Tables["Contacts"].Rows)
                    {
                        var contactEntity = new ContactDto
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

        public ContactDto Insert(ContactDto contact)
        {
            var connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Insert_Contact_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Firstname", contact.Firstname);
                    command.Parameters.AddWithValue("@Lastname", contact.Lastname);
                    command.Parameters.AddWithValue("@Email", contact.Email);

                    var idParameter = command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    idParameter.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    contact.Id = (int)command.Parameters["@Id"].Value;
                }
            }

            return contact;
        }

        public ContactDto Update(ContactDto contact)
        {
            var connString = _configuration.GetConnectionString("MyConn");

            using (var manager = ConnectionManager<SqlConnection>.GetManager(connString, false))
            {
                using (var command = new SqlCommand("Contacts_Update_Contact_SP", manager.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", contact.Id);
                    command.Parameters.AddWithValue("@Firstname", contact.Firstname);
                    command.Parameters.AddWithValue("@Lastname", contact.Lastname);
                    command.Parameters.AddWithValue("@Email", contact.Email);

                    command.ExecuteNonQuery();
                }
            }

            return contact;
        }
    }
}