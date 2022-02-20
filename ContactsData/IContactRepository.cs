using System.Collections.Generic;

namespace ContactsData
{
  public interface IContactRepository
  {
    bool Exists(int id);
    ContactEntity Get(int id);
    List<ContactEntity> Get();
    ContactEntity Insert(ContactEntity contact);
    ContactEntity Update(ContactEntity contact);
    bool Delete(int id);
  }
}
